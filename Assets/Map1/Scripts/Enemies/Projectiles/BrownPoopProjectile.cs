using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrownPoopProjectile : BirdProjectile
{
    // Audio manager
    private AudioManager audioManager;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();

    }

    public override void OnPlayerHitActions()
    {
        audioManager.PlaySoundEffect("BirdPoop");
    }
}
