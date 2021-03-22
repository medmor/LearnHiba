using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnTextClick : MonoBehaviour
{
    private void OnMouseDown()
    {
        SoundManager.Instance.Play(GameManager.Instance.currentItemToLearn.EnAudio);
    }
}
