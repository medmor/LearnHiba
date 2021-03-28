using ArabicSupport;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Manager<GameManager>
{
    public GameObject[] SystemPrefabs;
    private int CurrentItemToLearnIndex;
    internal ItemToLearn currentItemToLearn { get { return ItemsToLearn[CurrentItemToLearnIndex]; } private set { } }
    public List<ItemToLearn> ItemsToLearn;

    public TextMesh EnName;
    public TextMesh FrName;
    public TextMesh ArName;
    private GameObject objectToLearn;

    public Transform SpointPoint;

    void Start()
    {
        InstantiateSystemPrefabs();
        RandomItemToLearn();
        SetupScene();
        InputManager.Instance.OnSwipe += OnSwip;
    }
    void InstantiateSystemPrefabs()
    {
        GameObject prefabInstance;
        for (int i = 0; i < SystemPrefabs.Length; ++i)
        {
            prefabInstance = Instantiate(SystemPrefabs[i]);
        }
    }

    void SetupScene()
    {
        var currentItem = ItemsToLearn[CurrentItemToLearnIndex];
        Destroy(objectToLearn);
        objectToLearn = Instantiate(currentItem.prefabObject);
        Resize(objectToLearn);
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
        currentItemToLearn = ItemsToLearn[CurrentItemToLearnIndex];
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
        currentItemToLearn = ItemsToLearn[CurrentItemToLearnIndex];
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
        currentItemToLearn = ItemsToLearn[CurrentItemToLearnIndex];
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

    void Resize(GameObject obj)
    {
        var emptySize = .5;

        var mf = obj.GetComponent<MeshRenderer>();

        if (mf != null)
        {
            var bounds = mf.bounds;

            var max = bounds.extents.x;
            if (max < bounds.extents.y)
                max = bounds.extents.y;
            if (max < bounds.extents.z)
                max = bounds.extents.z;
            print(max);

            var scale = (float)(emptySize * 0.5) / max;
            transform.localScale = new Vector3(scale, scale, scale);
        }
    }
}
