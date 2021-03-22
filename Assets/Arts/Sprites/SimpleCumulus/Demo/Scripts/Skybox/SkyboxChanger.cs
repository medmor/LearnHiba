using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SkyboxChanger : MonoBehaviour
{
    private int index;
    public Material[] Skyboxes;

    private void Start()
    {
        index = Random.Range(0, Skyboxes.Length);
        StartCoroutine(NextSkybox());
    }

    public void ChangeSkybox(int index)
    {
        RenderSettings.skybox = Skyboxes[index];
        RenderSettings.skybox.SetFloat("_Rotation", 0);
    }

    public IEnumerator NextSkybox()
    {
        while (true)
        {
            yield return new WaitForSeconds(20);
            ChangeSkybox(index++);
        }
    }
}