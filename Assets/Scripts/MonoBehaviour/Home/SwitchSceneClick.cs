using UnityEngine;

public class SwitchSceneClick : MonoBehaviour
{
    public string SceneName;

    public void SwitchScene(string SceneName)
    {
        GameManager.Instance.SwitchScene(SceneName);
    }
}
