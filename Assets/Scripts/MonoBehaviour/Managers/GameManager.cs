using ArabicSupport;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

}
