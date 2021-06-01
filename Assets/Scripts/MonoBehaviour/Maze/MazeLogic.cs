﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MazeLogic : MonoBehaviour
{
    public Transform StartPos;
    public Transform GoalPos;

    public ItemsToLearnLists Items;
    [HideInInspector]
    public ItemToLearn Item;

    public NavMeshAgent agent;

    public GameObject textPerph;
    public GameObject wrongTextPerph;

    public TMPro.TextMeshProUGUI TimerText;

    public GameObject ArTextSlotPref;
    public GameObject EnTextSlotPref;
    public Transform ArUITextContainer;
    public Transform FrUITextContainer;
    public Transform EnUITextContainer;
    public GameObject Hearts;
    public Renderer FloorMeshMap;

    private readonly List<char> chars = new List<char>();
    private readonly List<char> foundChars = new List<char>();
    private readonly List<char> wrongChars = new List<char>();
    private readonly List<GameObject> UITexts = new List<GameObject>();
    private readonly List<char> allChars = new List<char>() { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'ا', 'ب', 'ج', 'د', 'ذ', 'ت', 'ث', 'س', 'ش', 'ز', 'ر', 'ض', 'ص', 'ق', 'ف', 'ع', 'غ', 'ه', 'خ', 'ح', 'ن', 'ل', 'م', 'ك', 'ط', 'و' };

    private readonly List<Vector3> charsPositions = new List<Vector3>();
    private readonly List<Vector3> wrongCharsPositions = new List<Vector3>();

    private NavMeshPath shortPath = default;
    private float totalDistance = 0;
    private float neededTime = 0;

    public void Start()
    {
        ChooseItemToLearn();
        SetUpScene();
        StartCoroutine(Timer());
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
        print(usedItems.Count);
        print(Items.RandomCombinedList.Count);

        if (usedItems.Count == Items.RandomCombinedList.Count)
            Item = Items.RandomCombinedList[new System.Random().Next(usedItems.Count)];
        else
            Item = Items.RandomCombinedList[usedItems.Count];
        var material = FloorMeshMap.material;
        material.SetTexture("_MainTex", Resources.Load<Texture2D>("FloorMazes/" + Item.EnName));
    }

    float GetPathRemainingDistance()
    {
        if (agent.pathPending ||
            agent.pathStatus == NavMeshPathStatus.PathInvalid ||
            agent.path.corners.Length == 0)
            return -1f;

        float distance = 0.0f;
        for (int i = 0; i < agent.path.corners.Length - 1; ++i)
        {
            distance += Vector3.Distance(agent.path.corners[i], agent.path.corners[i + 1]);
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
        agent.SetPath(shortPath);
        agent.isStopped = true;
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

            if (i < Item.ArName.Length)
            {
                txt = Instantiate(ArTextSlotPref);
                txt.transform.SetParent(ArUITextContainer);
            }
            else if (i < Item.ArName.Length + Item.FrName.Length)
            {
                txt = Instantiate(EnTextSlotPref);
                txt.transform.SetParent(FrUITextContainer);
            }
            else
            {
                txt = Instantiate(EnTextSlotPref);
                txt.transform.SetParent(EnUITextContainer);
            }
            txt.GetComponentInChildren<TextMeshProUGUI>().text = chars[i].ToString();
            UITexts.Add(txt);

            if (i < wrongCharsPositions.Count)
            {
                txt = Instantiate(wrongTextPerph);
                txt.transform.position = wrongCharsPositions[i] + Vector3.up * .05f;
                txt.GetComponent<MazeText>().SetTexts(wrongChars[i].ToString());
            }
        }
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
            UITexts[index].GetComponent<Image>().color = new Color(0, 1, 0, .5f);
            if (!StillCharsToFind())
            {
                MazeCompleted();
            }
        }
        else
        {
            DecrementHeart();
        }
    }
    void MazeCompleted()
    {
        SoundManager.Instance.PlayEffects("Tada");
        ProgressManager.Instance.AddMazeItemToLearn(Item.EnName);
        ProgressManager.Instance.AddCompletedMaze(
           int.Parse(SceneManager.GetActiveScene().name.Substring(4)) + 1 + ":0");
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
    void CalculateNeededTime()
    {
        neededTime = totalDistance * agent.speed * 10;
    }
    IEnumerator Timer()
    {
        while (neededTime > 0)
        {
            System.TimeSpan t = System.TimeSpan.FromSeconds(neededTime);

            string answer = string.Format("{0:D2}.{1:D2}",
                            t.Minutes,
                            t.Seconds);
            neededTime -= 1f;
            TimerText.text = answer;
            yield return new WaitForSeconds(1f);
        }
    }
    IEnumerator Wait(float seconds, LambdaArgument lambda)
    {
        yield return new WaitForSeconds(seconds);
        lambda();
    }
    delegate void LambdaArgument();
    string AreAllCharsOfNameFound(string name)
    {

        return "";
    }
}

class CharsManagement
{

}
