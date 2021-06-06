using UnityEngine;
using UnityEngine.UI;

public class StarsContainer : MonoBehaviour
{
    public GameObject StarPref;
    public GridLayoutGroup Grid;
    public void Start()
    {

    }

    public void SpawnStars(int number)
    {
        for (var i = 0; i < number; i++)
        {
            Instantiate(StarPref)
                .transform
                .SetParent(Grid.transform);
        }

    }
    public void Tween()
    {
        LeanTween.moveLocalY(gameObject, 40, 1)
        .setEase(LeanTweenType.easeOutBounce)
        .setLoopClamp()
        .setRepeat(1);
    }
}
