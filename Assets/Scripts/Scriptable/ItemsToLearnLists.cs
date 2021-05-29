using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemsToLearnLists", menuName = "ItemsToLearnLists")]
public class ItemsToLearnLists : ScriptableObject
{

    public List<ItemToLearn> Animals;
    public List<ItemToLearn> Vegetables;

    [HideInInspector]
    public List<ItemToLearn> CurrentList;
    [HideInInspector]
    public string ObjectUrl;
    [HideInInspector]
    public string AudioUrl;

    public void SetList(string listEnum)
    {
        switch (listEnum)
        {
            case "ANIMALS":
                CurrentList = Animals;
                SetUrls("Animals/");
                break;
            case "VEGETABLES":
                CurrentList = Vegetables;
                SetUrls("Vegetables/");
                break;
            default:
                break;
        }
    }

    private void SetUrls(string url)
    {
        ObjectUrl = "ObjectsToLearn/" + url;
        AudioUrl = "Audios/" + url;
    }
}
