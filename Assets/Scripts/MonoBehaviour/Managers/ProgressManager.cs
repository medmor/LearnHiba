using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ProgressManager : Manager<ProgressManager>
{
    public readonly string Maze = "maze";
    private List<string> MazeProgress = new List<string>();

    private void Start()
    {
        if (PlayerPrefs.HasKey(Maze))
            MazeProgress = StringToList(GetMazeProgress());
        else
            ResetMazeProgress();
    }

    public void SetMazeProgress()
    {
        PlayerPrefs.SetString(Maze, ListToString(MazeProgress));
        PlayerPrefs.Save();
    }

    public string GetMazeProgress()
    {
        return PlayerPrefs.GetString(Maze);
    }


    public void ResetMazeProgress()
    {
        MazeProgress = new List<string>() { "1:0" };
        SetMazeProgress();
    }

    public bool LockedMaze(int mazeNumber)
    {
        return mazeNumber > int.Parse(MazeProgress[MazeProgress.Count - 1].Split(':')[0]);
    }

    private string ListToString(List<string> list)
    {
        return string.Join(",", list);
    }

    private List<string> StringToList(string strList)
    {
        return strList.Split(',').ToList();
    }
}
