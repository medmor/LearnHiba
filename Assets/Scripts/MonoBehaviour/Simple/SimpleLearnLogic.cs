﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArabicSupport;

public class SimpleLearnLogic : MonoBehaviour
{
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
        RandomItemToLearn();
        SetupScene();
        InputManager.Instance.OnSwipe += OnSwip;
    }

    void SetupScene()
    {
        var currentItem = ItemsToLearn[CurrentItemToLearnIndex];
        Destroy(objectToLearn);
        objectToLearn = Instantiate(currentItem.prefabObject);
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

}