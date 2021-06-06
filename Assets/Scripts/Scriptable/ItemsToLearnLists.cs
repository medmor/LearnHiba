
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemsToLearnLists", menuName = "ItemsToLearnLists")]
public class ItemsToLearnLists : ScriptableObject
{

    public List<ItemToLearn> Animals;
    public List<ItemToLearn> Vegetables;

    private List<ItemToLearn> randomCombinedList;
    [HideInInspector]
    public List<ItemToLearn> RandomCombinedList {
        get {
            if (randomCombinedList == null || randomCombinedList.Count == 0)
            {
                var r = new System.Random();
                randomCombinedList = Animals.Concat(Vegetables).OrderBy(i => r.Next()).ToList();
            }
            return randomCombinedList;
        }

    }

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
