using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Manager<SoundManager>
{
    public AudioSource MusicAudioSource;
    public AudioSource NamesAudioSource;
    public AudioSource EffectsAudioSource;
    public List<AudioClip> SoundsEffects;
    public List<AudioClip> Musics;

    void Start()
    {
        MusicAudioSource.volume = .1f;
    }

    public void PlayNames(AudioClip clip)
    {
        StopNamesAudioSource();
        NamesAudioSource.PlayOneShot(clip);
    }
    public void PlayEffects(string sound)
    {
        StopEffectsAudioSource();
        var clip = SoundsEffects.Find(s => s.name == sound);
        EffectsAudioSource.PlayOneShot(clip);
    }

    public void PlayMusic(string clip)
    {
        MusicAudioSource.clip = Musics.Find(c => c.name == clip);
        MusicAudioSource.loop = true;
        MusicAudioSource.Play();
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
    public float SoundLength(string sound)
    {
        return SoundsEffects.Find(s => s.name == sound).length;
    }
}
