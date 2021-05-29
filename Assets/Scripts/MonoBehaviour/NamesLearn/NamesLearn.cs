using ArabicSupport;
using UnityEngine;

public class NamesLearn : MonoBehaviour
{
    public ItemsToLearnLists Lists;

    private int CurrentItemToLearnIndex;
    private AudioClip enAudioClip;
    private AudioClip frAudioClip;
    private AudioClip arAudioClip;

    public TextMesh EnName;
    public TextMesh FrName;
    public TextMesh ArName;
    private GameObject objectToLearn;

    private Transform ClickableArea;
    public void Start()
    {

        ClickableArea = GameObject.Find("/ClickableArea").transform;

        RandomItemToLearn();
        SetupScene();

        InputManager.Instance.ClearOnSwipListners();
        InputManager.Instance.OnSwipe += OnSwip;
        EventsManager.Instance.ItemToLearnClicked.AddListener(OnItemToLearnClicked);
    }

    void SetupScene()
    {
        var currentItem = Lists.CurrentList[CurrentItemToLearnIndex];

        Destroy(objectToLearn);

        objectToLearn = Instantiate(Resources.Load<GameObject>(Lists.ObjectUrl + currentItem.EnName));
        enAudioClip = Resources.Load<AudioClip>(Lists.AudioUrl + currentItem.EnName + "/En" + currentItem.EnName);
        frAudioClip = Resources.Load<AudioClip>(Lists.AudioUrl + currentItem.EnName + "/Fr" + currentItem.EnName);
        arAudioClip = Resources.Load<AudioClip>(Lists.AudioUrl + currentItem.EnName + "/Ar" + currentItem.EnName);

        PivotTo(objectToLearn.transform, ClickableArea.position);

        if (!objectToLearn.GetComponent<Tween>())
            objectToLearn.AddComponent<Tween>();

        EnName.text = currentItem.EnName;
        FrName.text = currentItem.FrName;
        ArName.text = ArabicFixer.Fix(currentItem.ArName);
    }

    void RandomItemToLearn()
    {
        CurrentItemToLearnIndex = Random.Range(0, Lists.CurrentList.Count - 1);
    }

    void NexItemToLearn()
    {
        if (CurrentItemToLearnIndex < Lists.CurrentList.Count - 1)
        {
            CurrentItemToLearnIndex++;
        }
        else
        {
            CurrentItemToLearnIndex = 0;
        }
    }

    void PreviousItemToLearn()
    {
        if (CurrentItemToLearnIndex > 0)
        {
            CurrentItemToLearnIndex--;
        }
        else
        {
            CurrentItemToLearnIndex = Lists.CurrentList.Count - 1;
        }
    }

    void OnSwip(SwipeData data)
    {
        if (SoundManager.Instance.IsSoundPlying())
            SoundManager.Instance.Stop();
        var dir = data.Direction;
        switch (dir)
        {
            case SwipeDirection.Up | SwipeDirection.Left:
                PreviousItemToLearn();
                break;
            case SwipeDirection.Down | SwipeDirection.Right:
                NexItemToLearn();
                break;
            default:
                break;
        }
        SetupScene();
    }

    void OnItemToLearnClicked(string lang)
    {
        if (lang == "en")
            SoundManager.Instance.Play(enAudioClip);
        else if (lang == "fr")
            SoundManager.Instance.Play(frAudioClip);
        else if (lang == "ar")
            SoundManager.Instance.Play(arAudioClip);
        else
        {
            var r = Random.Range(0f, 1f);
            if (r < 0.33)
                SoundManager.Instance.Play(enAudioClip);
            else if (r < 0.66)
                SoundManager.Instance.Play(frAudioClip);
            else
                SoundManager.Instance.Play(arAudioClip);
        }
    }

    private float GetScaleCoefitient()
    {
        var clickableBox = ClickableArea.GetComponent<BoxCollider>();
        var objectBox = objectToLearn.GetComponent<BoxCollider>();
        var xScale = clickableBox.size.x / objectBox.size.x;
        var yScale = clickableBox.size.y / objectBox.size.y;
        var zScale = clickableBox.size.z / objectBox.size.z;
        return Mathf.Max(xScale, Mathf.Max(yScale, zScale));
    }

    private void PivotTo(Transform tr, Vector3 position)
    {
        Vector3 offset = tr.position - tr.gameObject.GetComponent<BoxCollider>().center;

        foreach (Transform child in tr)
            child.transform.position += offset;
        tr.position = position;
    }

}
