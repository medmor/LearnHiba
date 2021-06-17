using UnityEngine;

public class NamesLearnChoiceClick : MonoBehaviour
{
    public ItemsToLearnLists Lists;
    public void LoadObjectToLearnList(string list)
    {
        SoundManager.Instance.PlayEffects("Click");
        Lists.SetList(list);
        GameManager.Instance.SwitchScene("NamesLearn");
    }
}
