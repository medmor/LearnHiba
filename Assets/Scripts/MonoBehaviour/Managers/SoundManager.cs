using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Manager<SoundManager>
{
    AudioSource Source;

    void Start()
    {
        Source = GetComponent<AudioSource>();
    }

    public void Play(AudioClip clip)
    {
        if (Source.isPlaying)
            Stop();
        Source.PlayOneShot(clip);
    }
    public void Stop()
    {
        Source.Stop();
    }
    public bool IsSoundPlying()
    {
        return Source.isPlaying;
    }
}
