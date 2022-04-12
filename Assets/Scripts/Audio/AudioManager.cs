using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Lists of sounds
    [SerializeField] public Sound[] music;
    [SerializeField] public Sound[] soundEffects;
    // Audio sources that are attached to the game objects
    private HashSet<AudioSource> gameObjectsAudioSources;
    // Values for sounds attached to the game objects
    [SerializeField] private float defaultSpatialBlend = 1f;
    [SerializeField] private float defaultMaxDistance = 15;
    
    void Awake()
    {
        gameObjectsAudioSources = new HashSet<AudioSource>();
        
        var soundEffectsVolume = PlayerPrefs.GetFloat("FroggersSoundEffects");
        var musicVolume = PlayerPrefs.GetFloat("FroggersMusic");

        foreach (Sound sound in music)
        {
            sound.audioSource = gameObject.AddComponent<AudioSource>();
            sound.audioSource.clip = sound.clip;
            sound.audioSource.loop = true;

            sound.audioSource.volume = musicVolume;
            sound.audioSource.pitch = sound.pitch;
        }
        
        foreach (Sound sound in soundEffects)
        {
            sound.audioSource = gameObject.AddComponent<AudioSource>();
            sound.audioSource.clip = sound.clip;

            sound.audioSource.volume = soundEffectsVolume;
            sound.audioSource.pitch = sound.pitch;
        }
    }
    
    public Sound PlaySoundEffect(string name)
    {
        Sound sound = Array.Find(soundEffects, sound => sound.name == name);
        sound.audioSource.Play();
        return sound;
    }
    
    public Sound PlayMusic(string name)
    {
        Sound sound = Array.Find(music, sound => sound.name == name);
        sound.audioSource.Play();
        return sound;
    }
    
    public void StopMusic(string name)
    {
        Sound sound = Array.Find(music, sound => sound.name == name);
        sound.audioSource.Stop();
    }
    
    public void UpdateSoundEffectsVolume()
    {
        var soundEffectsVolume = PlayerPrefs.GetFloat("FroggersSoundEffects");
        foreach (Sound sound in soundEffects)
        {
            sound.volume = soundEffectsVolume;
            sound.audioSource.volume = soundEffectsVolume;
        }

        UpdateGameObjectsSoundEffectsVolume();
    }

    private void UpdateGameObjectsSoundEffectsVolume()
    {
        var soundEffectsVolume = PlayerPrefs.GetFloat("FroggersMusic");
        foreach (AudioSource audioSource in gameObjectsAudioSources)
        {
            audioSource.volume = soundEffectsVolume;
        }
    }

    public void UpdateMusicVolume()
    {
        var musicVolume = PlayerPrefs.GetFloat("FroggersMusic");
        foreach (Sound sound in music)
        {
            sound.volume = musicVolume;
            sound.audioSource.volume = musicVolume;
        }
    }

    public AudioSource AttachAudioSourceToGameObject(GameObject gameObject, string name)
    {
        Sound sound = Array.Find(soundEffects, sound => sound.name == name);
        AudioSource audioSourceComponent = gameObject.AddComponent<AudioSource>();
        audioSourceComponent.clip = sound.clip;
        audioSourceComponent.spatialBlend = defaultSpatialBlend;
        audioSourceComponent.maxDistance = defaultMaxDistance;
        audioSourceComponent.rolloffMode = AudioRolloffMode.Linear;
        audioSourceComponent.volume = sound.audioSource.volume;
        gameObjectsAudioSources.Add(audioSourceComponent);
        return audioSourceComponent;
    }

    public void DetachAudioSource(AudioSource audioSource)
    {
        gameObjectsAudioSources.Remove(audioSource);
    }
}
