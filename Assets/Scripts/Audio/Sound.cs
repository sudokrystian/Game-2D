using System;
using UnityEngine.Audio;
using UnityEngine;

[Serializable]
public class Sound
{
    public string name;
    
    public AudioClip clip;
    [Range(0f, 1f)]
    public float volume;
    [Range(0f, 1f)]
    public float pitch;

    [HideInInspector] public AudioSource audioSource;


}
