using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class MazeLogic : MonoBehaviour
{

    public ItemsToLearnLists Items;
    [HideInInspector]
    public ItemToLearn Item;

    public NavMeshAgent agent;

    public GameObject textPerph;
    public GameObject wrongTextPerph;

    public GameObject ArTextSlotPref;
    public GameObject EnTextSlotPref;
    public UITextContainer ArUITextContainer;
    public UITextContainer FrUITextContainer;
    public UITextContainer EnUITextContainer;
    public GameObject Hearts;

    public StarsContainer StarsContainer;

    public Material FloorMaterial;

    private Transform StartPos;
    private Transform GoalPos;

    private readonly List<char> chars = new List<char>();
    private readonly List<char> foundChars = new List<char>();
    private readonly List<char> wrongChars = new List<char>();
    private readonly List<GameObject> UIChars = new List<GameObject>();
    private readonly List<char> allChars = new List<char>() { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'ا', 'ب', 'ج', 'د', 'ذ', 'ت', 'ث', 'س', 'ش', 'ز', 'ر', 'ض', 'ص', 'ق', 'ف', 'ع', 'غ', 'ه', 'خ', 'ح', 'ن', 'ل', 'م', 'ك', 'ط', 'و' };

    private readonly List<Vector3> charsPositions = new List<Vector3>();
    private readonly List<Vector3> wrongCharsPositions = new List<Vector3>();
    private bool[] foundNames = new bool[] { false, false, false };

    private NavMeshPath shortPath = default;
    private float totalDistance = 0;

    public void Start()
    {
        Instantiate(Resources.Load<GameObject>("Maze/Levels/" + GameManager.Instance.CurrentMaze));
        GoalPos = GameObject.Find(GameManager.Instance.CurrentMaze + "(Clone)/Goal").transform;
        StartPos = GameObject.Find(GameManager.Instance.CurrentMaze + "(Clone)/Start").transform;
        agent.gameObject.transform.position = StartPos.position;
        agent.transform.position = StartPos.position;

        ChooseItemToLearn();

        SetUpScene();

        EventsManager.Instance.PlayerCollideWithChar.AddListener(OnPlayerCollideWhitheChar);
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            MazeCompleted();
        }
    }

    void ChooseItemToLearn()
    {
        var usedItems = ProgressManager.Instance.GetIMazeItemsToLearn();

        if (usedItems.Count == Items.RandomCombinedList.Count)
            Item = Items.RandomCombinedList[new System.Random().Next(usedItems.Count)];
        else
            Item = Items.RandomCombinedList[usedItems.Count];
        var material = GameObject.Find(GameManager.Instance.CurrentMaze + "(Clone)/Map/Floor").GetComponent<Renderer>().material;// FloorMeshMap.material;
        material.SetTexture("_MainTex", Resources.Load<Texture2D>("Maze/Floors/" + Item.EnName));
    }
    float GetPathRemainingDistance()
    {
        float distance = 0.0f;
        for (int i = 0; i < shortPath.corners.Length - 1; ++i)
        {
            distance += Vector3.Distance(shortPath.corners[i], shortPath.corners[i + 1]);
        }
        return distance;
    }
    void SetChars()
    {
        foreach (var c in Item.ArName)
        {
            var wrongChar = allChars[Random.Range(0, allChars.Count)];
            while (Item.ArName.Contains(wrongChar.ToString()) || chars.Contains(wrongChar))
            {
                wrongChar = allChars[Random.Range(0, allChars.Count)];
            }
            wrongChars.Add(wrongChar);
            chars.Add(c);
            foundChars.Add('0');
        }
        foreach (var c in Item.FrName)
        {
            var wrongChar = allChars[Random.Range(0, allChars.Count)];
            while (Item.EnName.Contains(wrongChar.ToString()) || Item.FrName.Contains(wrongChar.ToString()) || chars.Contains(wrongChar))
            {
                wrongChar = allChars[Random.Range(0, allChars.Count)];
            }
            wrongChars.Add(wrongChar);
            chars.Add(c);
            foundChars.Add('0');
        }
        foreach (var c in Item.EnName)
        {
            var wrongChar = allChars[Random.Range(0, allChars.Count)];
            while (Item.EnName.Contains(wrongChar.ToString()) || Item.FrName.Contains(wrongChar.ToString()) || chars.Contains(wrongChar))
            {
                wrongChar = allChars[Random.Range(0, allChars.Count)];
            }
            wrongChars.Add(wrongChar);
            chars.Add(c);
            foundChars.Add('0');
        }
    }
    void CalculateShortPath()
    {
        shortPath = new NavMeshPath();
        agent.CalculatePath(GoalPos.position, shortPath);
    }
    void SetCharsPositions()
    {
        var initialPos = StartPos.position;
        var averageDist = totalDistance / chars.Count;
        var cornerIndex = 0;
        var remainingDistToCorner = Vector3.Distance(initialPos, shortPath.corners[cornerIndex]);

        for (int i = 0; i < chars.Count; i++)
        {
            var dist = averageDist;
            while (remainingDistToCorner < dist && cornerIndex < shortPath.corners.Length - 1)
            {
                dist -= remainingDistToCorner;
                initialPos = shortPath.corners[cornerIndex];
                cornerIndex++;
                remainingDistToCorner = Vector3.Distance(initialPos, shortPath.corners[cornerIndex]);
            }
            var dir = (shortPath.corners[cornerIndex] - initialPos).normalized;
            charsPositions.Add(initialPos + dir * dist);
            var wrongPos = new Vector3(charsPositions[i].x + Random.Range(-1f, 1f),
                charsPositions[i].y, charsPositions[i].z + Random.Range(-1f, 1f));
            if (NavMesh.SamplePosition(wrongPos, out NavMeshHit hit, 1, NavMesh.GetAreaFromName("Not Walkable"))
                && Vector3.Distance(hit.position, charsPositions[i]) > .5f)
                wrongCharsPositions.Add(hit.position);
            initialPos = charsPositions[i];
            remainingDistToCorner -= dist;
        }
    }
    void SpawnSharsInScene()
    {
        for (var i = 0; i < chars.Count; i++)
        {
            var txt = Instantiate(textPerph);
            txt.transform.position = charsPositions[i] + Vector3.up * .05f;
            txt.GetComponent<MazeText>().SetTexts(chars[i].ToString());
            if (i < wrongCharsPositions.Count)
            {
                txt = Instantiate(wrongTextPerph);
                txt.transform.position = wrongCharsPositions[i] + Vector3.up * .05f;
                txt.GetComponent<MazeText>().SetTexts(wrongChars[i].ToString());
            }
        }
        ArUITextContainer.SpawnUIChars(Item.ArName, UIChars);
        FrUITextContainer.SpawnUIChars(Item.FrName, UIChars);
        EnUITextContainer.SpawnUIChars(Item.EnName, UIChars);
    }
    void SetUpScene()
    {
        SetChars();
        CalculateShortPath();
        totalDistance = GetPathRemainingDistance();
        CalculateNeededTime();
        SetCharsPositions();
        SpawnSharsInScene();
    }
    void OnPlayerCollideWhitheChar(GameObject obj)
    {
        var index = GetFoundCharIndex(obj.GetComponent<MazeText>().GetChar());
        if (index > -1)
        {
            SoundManager.Instance.PlayEffects("Collect2");
            CheckFoundNames();
            UIChars[index].GetComponent<Image>().color = new Color(0, 1, 0, .5f);
        }
        else
        {
            DecrementHeart();
        }
    }
    void CheckFoundNames()
    {
        for (var i = 0; i < foundNames.Length; i++)
        {
            if (!foundNames[i])
                if (!string.IsNullOrWhiteSpace(
                    AreAllCharsOfNameFound(i == 0 ? Item.ArName : i == 1 ? Item.FrName : Item.EnName)
                    ))
                {
                    foundNames[i] = true;
                    SoundManager.Instance.PlayEffects("Tada");
                    var sound = Resources.Load<AudioClip>("Audios/" + Item.Type + "/" + Item.EnName +
                        (i == 0 ? "/Ar" : i == 1 ? "/Fr" : "/En")
                        + Item.EnName);
                    StartCoroutine(Wait(SoundManager.Instance.SoundLength("Tada"), () =>
                    {
                        SoundManager.Instance.PlayNames(sound);
                        StartCoroutine(Wait(sound.length + .5f,
                            () =>
                            {
                                SoundManager.Instance.PlayNames(sound);
                                if (!StillCharsToFind())
                                {
                                    StartCoroutine(Wait(sound.length + 1f, () => MazeCompleted()));
                                }
                            }
                            ));
                    }));
                    return;
                }
        }

    }
    void MazeCompleted()
    {
        SoundManager.Instance.PlayEffects("Win");
        ProgressManager.Instance.AddMazeItemToLearn(Item.EnName);
        var starsNumber = 3;
        if (GameObject.Find("/MazeUI/Timer").GetComponent<MazeTimer>().IsTimeDone())
            starsNumber--;
        if (Hearts.transform.childCount < 3)
            starsNumber--;
        StarsContainer.SpawnStars(starsNumber);
        StarsContainer.Tween();
        ProgressManager.Instance.AddCompletedMaze(
           int.Parse(GameManager.Instance.CurrentMaze.Substring(4)) + ":" + starsNumber
           );
        StartCoroutine(Wait(3, () => { GameManager.Instance.SwitchScene("MazeChoice"); }));
    }
    int GetFoundCharIndex(char c)
    {
        if (chars.Contains(c))
        {
            var index = chars.IndexOf(c);
            while (foundChars[index] != '0')
                index = chars.IndexOf(c, index + 1);
            foundChars[index] = c;
            return index;
        }
        return -1;
    }
    bool StillCharsToFind()
    {
        return foundChars.Contains('0');
    }
    void DecrementHeart()
    {
        SoundManager.Instance.PlayEffects("BadCollect");
        var heartsNumber = Hearts.transform.childCount - 1;
        Destroy(Hearts.transform.GetChild(heartsNumber).gameObject);
        if (heartsNumber == 0)
            GameManager.Instance.SwitchScene("gameover");

    }
    float CalculateNeededTime()
    {
        return totalDistance * agent.speed * 10;
    }

    IEnumerator Wait(float seconds, LambdaArgument lambda)
    {
        yield return new WaitForSeconds(seconds);
        lambda();
    }
    delegate void LambdaArgument();

    string AreAllCharsOfNameFound(string name)
    {
        for (var i = 0; i < foundChars.Count; i++)
        {
            if (name[0] == foundChars[i])
            {
                var j = 1;
                while (j < name.Length && name[j] == foundChars[i + j])
                {
                    j++;
                }
                if (j == name.Length)
                    return name;
            }
        }

        return "";
    }

}

