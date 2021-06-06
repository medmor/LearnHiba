using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UITextContainer : MonoBehaviour
{
    public string Direction;
    public GridLayoutGroup Grid;
    public GameObject TextSlotPref;

    public void Start()
    {

        if (Direction == "RTL")
            Grid.startCorner = GridLayoutGroup.Corner.UpperRight;
        else
            Grid.startCorner = GridLayoutGroup.Corner.UpperLeft;
    }
    public void SpawnUIChars(string name, List<GameObject> uIChars)
    {
        for (var i = 0; i < name.Length; i++)
        {
            var txt = Instantiate(TextSlotPref);
            txt.transform.SetParent(Grid.transform);
            txt.GetComponentInChildren<TextMeshProUGUI>().text = name[i].ToString();
            uIChars.Add(txt);
        }
    }
}
