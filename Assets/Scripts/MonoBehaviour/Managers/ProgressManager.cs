using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ProgressManager : Manager<ProgressManager>
{
    public readonly string MazeProgressKey = "MazesProgress";
    public readonly string MazeItemsToLearnKey = "MazeItemsToLearn";

    private List<string> MazeProgress = new List<string>();
    private readonly List<string> MazeItemsToLearn = new List<string>();

    private void Start()
    {
        if (PlayerPrefs.HasKey(MazeProgressKey))
            MazeProgress = GetMazeProgress().Split(',').ToList();
        else
            ResetMazeProgress();
        MazeProgress = MazeProgress.Distinct().ToList();
    }

    public void SetMazeProgress()
    {
        PlayerPrefs.SetString(MazeProgressKey, string.Join(",", MazeProgress));
        PlayerPrefs.Save();
    }

    public void AddCompletedMaze(string completedMaze)
    {
        if (MazeProgress.Contains(completedMaze))
            return;
        MazeProgress.Add(completedMaze);
        SetMazeProgress();
    }

    public string GetMazeProgress()
    {
        return PlayerPrefs.GetString(MazeProgressKey);
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

    public void AddMazeItemToLearn(string item)
    {
        this.MazeItemsToLearn.Add(item);
    }

    public List<string> GetIMazeItemsToLearn()
    {
        return this.MazeItemsToLearn;
    }

    public void ResetMazeItmsToLearn()
    {
        this.MazeItemsToLearn.Clear();
    }


}
