using System;
using System.Collections;
using System.Collections.Generic;
using Audio;
using UnityEngine;
using Random = UnityEngine.Random;

public class CharacterLocomotionEvents : AudioEvents
{
    #region Locomotion Audioclips

    public SoundSet leftFootSoundSet;
    public SoundSet rightFootSoundSet;

    #endregion

    
    public void OnCharEvent(String e)
    {
        switch (e)
        {
            case "FootDown":
                
                break;
            case "LeftFootDown":
                InitSettings(leftFootSoundSet);
                break;
            case "RightFootDown":
                InitSettings(rightFootSoundSet);
                break;
            case "Land":
                
                break;
        }
        
        audioSource.Play();
    }
}

public class AudioEvents : MonoBehaviour
{
    #region References

    private protected AudioSource audioSource;

    #endregion
    
    private void Start()
    {
        GetComponents();
    }

    void GetComponents()
    {
        audioSource = GetComponent<AudioSource>();
    }
    
    protected void InitSettings(SoundSet set)
    {
        audioSource.clip = set.clips[Random.Range(0, set.clips.Count)];
        audioSource.volume = 1 - set.volume/100;
        audioSource.minDistance = set.distMin;
        audioSource.maxDistance = set.distMax;
        audioSource.loop = set.loopCount != 1;
        audioSource.spatialBlend = set.spatialBlend;
        audioSource.panStereo = Random.Range(set.panMin, set.panMax);
        audioSource.rolloffMode = set.rolloffMode;
    }
}
