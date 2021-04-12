using ArabicSupport;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Manager<GameManager>
{
    public GameObject[] SystemPrefabs;
    

    void Start()
    {
        InstantiateSystemPrefabs();
    }
    void InstantiateSystemPrefabs()
    {
        GameObject prefabInstance;
        for (int i = 0; i < SystemPrefabs.Length; ++i)
        {
            prefabInstance = Instantiate(SystemPrefabs[i]);
        }
    }

    public void SwitchScene(string name)
    {
        SceneManager.LoadScene(name);
    }


}
