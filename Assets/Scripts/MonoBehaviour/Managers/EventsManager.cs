using UnityEngine;
using UnityEngine.Events;

public class EventsManager : Manager<EventsManager>
{
    internal PlayerCollideWithChar PlayerCollideWithChar;
    internal ItemToLearnClicked ItemToLearnClicked;
    public override void Awake()
    {
        base.Awake();
        PlayerCollideWithChar = new PlayerCollideWithChar();
        ItemToLearnClicked = new ItemToLearnClicked();
    }

}

[System.Serializable] public class PlayerCollideWithChar : UnityEvent<GameObject> { }
[System.Serializable] public class ItemToLearnClicked : UnityEvent<string> { }

