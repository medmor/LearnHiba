using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Manager<GameManager>
{
    public GameObject[] SystemPrefabs;
    public string CurrentMaze { get; set; } = "Maze6";
    public bool HommeIntro = false;


    void Start()
    {
        InstantiateSystemPrefabs();
    }
    void InstantiateSystemPrefabs()
    {
        for (int i = 0; i < SystemPrefabs.Length; ++i)
        {
            Instantiate(SystemPrefabs[i]);
        }
        SoundManager.Instance.PlayEffects("Intro");
    }

    public void SwitchScene(string name)
    {
        if (name == "Maze" || name == "NamesLearn")
            SoundManager.Instance.PlayMusic(SoundManager.Instance.Musics[Random.Range(0, SoundManager.Instance.Musics.Count)].name);
        else
            SoundManager.Instance.StopMusicAudioSource();
        SceneManager.LoadScene(name);
    }


}
