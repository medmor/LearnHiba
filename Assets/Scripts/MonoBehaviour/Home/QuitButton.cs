using UnityEngine;

public class QuitButton : MonoBehaviour
{
    public void Quit()
    {
        SoundManager.Instance.PlayEffects("Click");
        Application.Quit();
    }
}
