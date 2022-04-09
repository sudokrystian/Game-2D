using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource theme;
    [SerializeField] public Sound[] sounds;
    private List<AudioSource> internalAudioSources;
    private List<AudioSource> gameObjectsAudioSources;
    [SerializeField] private float spatialBlend = 1f;
    [SerializeField] private float maxDistance = 15;
    private string themeTitle = "Theme";

    void Awake()
    {
        internalAudioSources = new List<AudioSource>(sounds.Length);
        gameObjectsAudioSources = new List<AudioSource>(sounds.Length);
        var soundEffectsVolume = PlayerPrefs.GetFloat("SoundEffects");
        var musicVolume = PlayerPrefs.GetFloat("Music");

        foreach (Sound sound in sounds)
        {
            sound.audioSource = gameObject.AddComponent<AudioSource>();
            sound.audioSource.clip = sound.clip;

            sound.audioSource.volume = soundEffectsVolume;
            sound.audioSource.pitch = sound.pitch;
            if (!sound.name.Equals(themeTitle))
            {
                internalAudioSources.Add(sound.audioSource);
            }
        }
        this.theme = Array.Find(sounds, sound => sound.name == themeTitle).audioSource;
        theme.volume = musicVolume;
    }

    private void Start()
    {
        Play(themeTitle);
    }

    public void Play(string name)
    {
        Sound sound = Array.Find(sounds, sound => sound.name == name);
        sound.audioSource.Play();
    }

    public void UpdateVolume()
    {
        var soundEffectsVolume = PlayerPrefs.GetFloat("SoundEffects");
        var musicVolume = PlayerPrefs.GetFloat("Music");

        internalAudioSources.ForEach(source => source.volume = soundEffectsVolume);
        gameObjectsAudioSources.ForEach(source => source.volume = soundEffectsVolume);
        
        theme.volume = musicVolume;
    }

    public AudioSource AttachAudioSourceToGameObject(GameObject gameObject, string name)
    {
        Sound sound = Array.Find(sounds, sound => sound.name == name);
        AudioSource audioSourceComponent = gameObject.AddComponent<AudioSource>();
        audioSourceComponent.clip = sound.clip;
        audioSourceComponent.spatialBlend = spatialBlend;
        audioSourceComponent.maxDistance = maxDistance;
        audioSourceComponent.rolloffMode = AudioRolloffMode.Linear;
        audioSourceComponent.volume = sound.audioSource.volume;
        gameObjectsAudioSources.Add(audioSourceComponent);
        return audioSourceComponent;
    }
}
