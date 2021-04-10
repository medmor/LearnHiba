using UnityEngine;
using UnityEngine.Events;

public class EventsManager : Manager<EventsManager>
{
    internal PlayerCollideWithChar PlayerCollideWithChar;
    public override void Awake()
    {
        base.Awake();
        PlayerCollideWithChar = new PlayerCollideWithChar();
    }

}

[System.Serializable] public class PlayerCollideWithChar : UnityEvent<GameObject> { }
