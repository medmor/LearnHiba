using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class SwitchSceneClick : MonoBehaviour
{
    public string SceneName;
    private void OnMouseDown()
    {
        GameManager.Instance.SwitchScene(SceneName);
    }
}
