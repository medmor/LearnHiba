using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleText : MonoBehaviour
{

    public TextMesh[] texts;
    void Start()
    {

        LeanTween.rotateY(gameObject, 45, 5)
            .setEase(LeanTweenType.easeShake)
            .setLoopClamp()
            .setRepeat(-1);
    }

    public void SetTexts(string str)
    {
        foreach (var t in texts)
        {
            t.text = str;
        }
    }

}
