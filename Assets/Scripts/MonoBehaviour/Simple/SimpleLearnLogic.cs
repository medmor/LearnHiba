using ArabicSupport;
using System.Collections.Generic;
using UnityEngine;

public class SimpleLearnLogic : MonoBehaviour
{
    private int CurrentItemToLearnIndex;
    internal ItemToLearn CurrentItemToLearn { get { return ItemsToLearn[CurrentItemToLearnIndex]; } private set { } }
    public List<ItemToLearn> ItemsToLearn;

    public TextMesh EnName;
    public TextMesh FrName;
    public TextMesh ArName;
    private GameObject objectToLearn;

    public Transform SpointPoint;
    void Start()
    {
        RandomItemToLearn();
        SetupScene();
        InputManager.Instance.OnSwipe += OnSwip;
        EventsManager.Instance.ItemToLearnClicked.AddListener(OnItemToLearnClicked);
    }

    void SetupScene()
    {
        var currentItem = ItemsToLearn[CurrentItemToLearnIndex];
        Destroy(objectToLearn);
        objectToLearn = Instantiate(Resources.Load("ObjectsToLearn/Animals/" + currentItem.EnName)) as GameObject;
        currentItem.EnAudio = Resources.Load<AudioClip>("Audios/Animals/" + currentItem.EnName + "/En" + currentItem.EnName);
        currentItem.FrAudio = Resources.Load<AudioClip>("Audios/Animals/" + currentItem.EnName + "/Fr" + currentItem.EnName);
        currentItem.ArAudio = Resources.Load<AudioClip>("Audios/Animals/" + currentItem.EnName + "/Ar" + currentItem.EnName);
        objectToLearn.transform.position = SpointPoint.position;
        if (!objectToLearn.GetComponent<Tween>())
            objectToLearn.AddComponent<Tween>();
        EnName.text = currentItem.EnName;
        FrName.text = currentItem.FrName;
        ArName.text = currentItem.ArName;
        ArName.text = ArabicFixer.Fix(currentItem.ArName);
    }

    void RandomItemToLearn()
    {
        CurrentItemToLearnIndex = Random.Range(0, ItemsToLearn.Count - 1);
        CurrentItemToLearn = ItemsToLearn[CurrentItemToLearnIndex];
    }

    void NexItemToLearn()
    {
        if (CurrentItemToLearnIndex < ItemsToLearn.Count - 1)
        {
            CurrentItemToLearnIndex++;
        }
        else
        {
            CurrentItemToLearnIndex = 0;
        }
        CurrentItemToLearn = ItemsToLearn[CurrentItemToLearnIndex];
    }

    void PreviousItemToLearn()
    {
        if (CurrentItemToLearnIndex > 0)
        {
            CurrentItemToLearnIndex--;
        }
        else
        {
            CurrentItemToLearnIndex = ItemsToLearn.Count - 1;
        }
        CurrentItemToLearn = ItemsToLearn[CurrentItemToLearnIndex];
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
            SoundManager.Instance.Play(CurrentItemToLearn.EnAudio);
        else if (lang == "fr")
            SoundManager.Instance.Play(CurrentItemToLearn.FrAudio);
        else if (lang == "ar")
            SoundManager.Instance.Play(CurrentItemToLearn.ArAudio);
        else
        {
            var r = Random.Range(0f, 1f);
            if (r < 0.33)
                SoundManager.Instance.Play(CurrentItemToLearn.EnAudio);
            else if (r < 0.66)
                SoundManager.Instance.Play(CurrentItemToLearn.FrAudio);
            else
                SoundManager.Instance.Play(CurrentItemToLearn.ArAudio);
        }
    }

}
