using System.Collections;
using UnityEngine;

public class SkyboxChanger : MonoBehaviour
{
    private readonly string baseUrl = "Skyboxes/";

    private void Awake()
    {
        StartCoroutine(NextSkybox());
    }

    public void ChangeSkybox(int index)
    {
        RenderSettings.skybox = Resources.Load<Material>(baseUrl + index);
    }

    public IEnumerator NextSkybox()
    {
        while (true)
        {
            ChangeSkybox(Random.Range(1, 5));
            yield return new WaitForSeconds(180);
        }
    }
}