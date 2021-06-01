using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Manager<SoundManager>
{
    public AudioSource MusicAudioSource;
    public AudioSource NamesAudioSource;
    public AudioSource EffectsAudioSource;
    public List<AudioClip> SoundsEffects;

    void Start()
    {
        NamesAudioSource = GetComponent<AudioSource>();
    }

    public void PlayNames(AudioClip clip)
    {
        StopNamesAudioSource();
        NamesAudioSource.PlayOneShot(clip);
    }
    public void PlayEffects(string sound)
    {
        StopEffectsAudioSource();
        var clip = this.SoundsEffects.Find(s => s.name == sound);
        EffectsAudioSource.PlayOneShot(clip);
    }
    public void StopAll()
    {
        StopNamesAudioSource();
        StopMusicAudioSource();
        StopEffectsAudioSource();
    }
    public void StopNamesAudioSource()
    {
        if (NamesAudioSource.isPlaying)
            NamesAudioSource.Stop();
    }
    public void StopMusicAudioSource()
    {
        if (MusicAudioSource.isPlaying)
            MusicAudioSource.Stop();
    }
    public void StopEffectsAudioSource()
    {
        if (EffectsAudioSource.isPlaying)
            EffectsAudioSource.Stop();
    }
    public bool IsNamesAudioSource()
    {
        return NamesAudioSource.isPlaying;
    }
}
