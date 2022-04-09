using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [SerializeField] public Sound[] sounds;
    private List<AudioSource> gameObjectsAudioSources;
    [SerializeField] private float spatialBlend = 1f;
    [SerializeField] private float maxDistance = 15;

    void Awake()
    {
        gameObjectsAudioSources = new List<AudioSource>(sounds.Length);
        var soundEffectsVolume = PlayerPrefs.GetFloat("SoundEffects");
        var musicVolume = PlayerPrefs.GetFloat("Music");

        foreach (Sound sound in sounds)
        {
            sound.audioSource = gameObject.AddComponent<AudioSource>();
            sound.audioSource.clip = sound.clip;

            sound.audioSource.volume = soundEffectsVolume;
            sound.audioSource.pitch = sound.pitch;
        }
        Array.Find(sounds, sound => sound.name == "Theme").audioSource.volume = musicVolume;
    }

    private void Start()
    {
        Play("Theme");
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

        foreach (Sound sound in sounds)
        {
            sound.audioSource.volume = soundEffectsVolume;
        }
        gameObjectsAudioSources.ForEach(source => source.volume = soundEffectsVolume);
        
        Array.Find(sounds, sound => sound.name == "Theme").audioSource.volume = musicVolume;
    }

    public Sound GetSound(string name)
    {
        return Array.Find(sounds, sound => sound.name == name);
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
