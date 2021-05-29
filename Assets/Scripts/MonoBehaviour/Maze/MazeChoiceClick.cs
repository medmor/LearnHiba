using UnityEngine;

public class MazeChoiceClick : MonoBehaviour
{
    public void GoToMaze(string mazeNumber)
    {
        if (ProgressManager.Instance.LockedMaze(int.Parse(mazeNumber)))
            return;
        GameManager.Instance.SwitchScene("Maze" + mazeNumber);
    }
}
