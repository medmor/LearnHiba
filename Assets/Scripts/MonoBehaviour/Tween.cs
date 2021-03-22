using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tween : MonoBehaviour
{
    void Start()
    {
        LeanTween.moveX(gameObject, transform.position.x - Random.Range(-.5f, .5f), Random.Range(1, 2f))
            .setEase(LeanTweenType.easeShake)
            .setLoopClamp()
            .setRepeat(-1);

        LeanTween.moveY(gameObject, transform.position.y - Random.Range(-.2f, .2f), Random.Range(1, 2f))
            .setEase(LeanTweenType.easeShake)
            .setLoopClamp()
            .setRepeat(-1);

        LeanTween.rotateZ(gameObject, transform.rotation.z - Random.Range(-10f, 10f), Random.Range(1, 2f))
            .setEase(LeanTweenType.easeShake)
            .setLoopClamp()
            .setRepeat(-1);

        LeanTween.scale(gameObject, transform.localScale * Random.Range(.8f, 1.2f), Random.Range(1, 2f))
            .setEase(LeanTweenType.easeShake)
            .setLoopClamp()
            .setRepeat(-1);
    }

}
