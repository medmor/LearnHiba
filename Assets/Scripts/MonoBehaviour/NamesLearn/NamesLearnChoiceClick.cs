using UnityEngine;

public class NamesLearnChoiceClick : MonoBehaviour
{
    public ItemsToLearnLists Lists;
    public void LoadObjectToLearnList(string list)
    {
        Lists.SetList(list);
        GameManager.Instance.SwitchScene("NamesLearn");
    }
}
