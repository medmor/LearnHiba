using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoxCollider))]
public class SimpleLearnObjectLearnClick : MonoBehaviour
{
    public string Lang;
    private void OnMouseDown()
    {
        EventsManager.Instance.ItemToLearnClicked.Invoke(Lang);
    }
}
