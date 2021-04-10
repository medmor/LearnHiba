using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(SphereCollider))]
public class ObjectClick : MonoBehaviour
{
    private void OnMouseDown()
    {
        var r = Random.Range(0f, 1f);
        if(r < 0.33)
            SoundManager.Instance.Play(GameManager.Instance.currentItemToLearn.EnAudio);
        else if(r < 0.66)
            SoundManager.Instance.Play(GameManager.Instance.currentItemToLearn.FrAudio);
        else
            SoundManager.Instance.Play(GameManager.Instance.currentItemToLearn.ArAudio);
    }
}
