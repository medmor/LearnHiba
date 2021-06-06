using UnityEngine;

public class SwitchSceneClick : MonoBehaviour
{
    public string SceneName;

    public void SwitchScene(string SceneName)
    {
        SoundManager.Instance.PlayEffects("Click");
        GameManager.Instance.SwitchScene(SceneName);
    }
}
