using System.Collections;
using UnityEngine;

public class MazeTimer : MonoBehaviour
{
    public TMPro.TextMeshProUGUI TimerText;
    private float neededTime = 180;
    private bool isRed = false;

    void Start()
    {
        StartCoroutine(Timer());

    }
    public bool IsTimeDone()
    {
        return neededTime <= 0;
    }
    IEnumerator Timer()
    {
        while (neededTime >= 0)
        {
            System.TimeSpan t = System.TimeSpan.FromSeconds(neededTime);

            string answer = string.Format("{0:D2}.{1:D2}",
                            t.Minutes,
                            t.Seconds);
            neededTime -= 1f;
            TimerText.text = answer;
            if (neededTime < 20 && !isRed)
            {
                isRed = true;
                TimerText.color = Color.red;
            }
            yield return new WaitForSeconds(1f);
        }
    }

}
