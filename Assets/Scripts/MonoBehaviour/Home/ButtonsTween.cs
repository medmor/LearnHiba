using UnityEngine;

public class ButtonsTween : MonoBehaviour
{
    public float ToY;
    public float Duration;
    void Start()
    {
        if (!GameManager.Instance.HommeIntro)
        {
            GameManager.Instance.HommeIntro = true;
            transform.position = new Vector3(transform.position.x, transform.position.y + 2000, 0);
            LeanTween.moveLocalY(gameObject, ToY, Duration)
               .setEase(LeanTweenType.easeOutBounce)
               .setLoopClamp()
               .setRepeat(1);

        }

    }


}
