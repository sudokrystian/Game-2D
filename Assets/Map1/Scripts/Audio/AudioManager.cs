using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [SerializeField] public Sound[] sounds;

    void Awake()
    {
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
        Array.Find(sounds, sound => sound.name == "Theme").audioSource.volume = musicVolume;
    }
}
