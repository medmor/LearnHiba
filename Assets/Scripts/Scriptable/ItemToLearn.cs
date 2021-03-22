using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="ItemToLearn",menuName ="ItemToLearn")]
public class ItemToLearn : ScriptableObject
{
    public string EnName;
    public string ArName;
    public string FrName;
    public AudioClip EnAudio;
    public AudioClip FrAudio;
    public AudioClip ArAudio;
    public GameObject prefabObject;
}
