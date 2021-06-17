using UnityEngine;
[RequireComponent(typeof(BoxCollider))]
public class NamesLearnObjectClick : MonoBehaviour
{
    public string Lang;
    private void OnMouseDown()
    {
        EventsManager.Instance.ItemToLearnClicked.Invoke(Lang);
    }
}
