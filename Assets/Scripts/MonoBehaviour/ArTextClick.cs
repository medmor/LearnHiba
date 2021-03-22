using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArTextClick : MonoBehaviour
{
    private void OnMouseDown()
    {
        SoundManager.Instance.Play(GameManager.Instance.currentItemToLearn.ArAudio);
    }
}
