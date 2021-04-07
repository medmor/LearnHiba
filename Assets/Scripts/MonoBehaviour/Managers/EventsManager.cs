using UnityEngine;
using UnityEngine.Events;

public class EventsManager : Manager<EventsManager>
{
    internal PlayerCollideWithChar PlayerCollideWithChar;
    private void Start()
    {
        PlayerCollideWithChar = new PlayerCollideWithChar();
    }

}

[System.Serializable] public class PlayerCollideWithChar : UnityEvent<GameObject> { }
