using UnityEngine;

public class LockButtonImage : MonoBehaviour
{

    public int mazeNumber;
    void Start()
    {
        if (ProgressManager.Instance.LockedMaze(mazeNumber))
            gameObject.SetActive(true);
        else
            gameObject.SetActive(false);

    }

}
