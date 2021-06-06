using ArabicSupport;
using UnityEngine;

public class NamesLearnLogic : MonoBehaviour
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
        SoundManager.Instance.StopNamesAudioSource();
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
            SoundManager.Instance.PlayNames(enAudioClip);
        else if (lang == "fr")
            SoundManager.Instance.PlayNames(frAudioClip);
        else if (lang == "ar")
            SoundManager.Instance.PlayNames(arAudioClip);
        else
        {
            var r = Random.Range(0f, 1f);
            if (r < 0.33)
                SoundManager.Instance.PlayNames(enAudioClip);
            else if (r < 0.66)
                SoundManager.Instance.PlayNames(frAudioClip);
            else
                SoundManager.Instance.PlayNames(arAudioClip);
        }
    }

    private void PivotTo(Transform tr, Vector3 position)
    {
        Vector3 offset = tr.position - tr.gameObject.GetComponent<BoxCollider>().center;

        foreach (Transform child in tr)
            child.transform.position += offset;
        tr.position = position;
    }

}
