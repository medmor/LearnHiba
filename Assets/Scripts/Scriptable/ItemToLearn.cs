using UnityEngine;

[CreateAssetMenu(fileName = "ItemToLearn", menuName = "ItemToLearn")]
public class ItemToLearn : ScriptableObject
{
    public string EnName;
    public string ArName;
    public string FrName;
    [HideInInspector]
    public AudioClip EnAudio = default;
    [HideInInspector]
    public AudioClip FrAudio = default;
    [HideInInspector]
    public AudioClip ArAudio = default;
    //private GameObject prefabObject = default;

}
