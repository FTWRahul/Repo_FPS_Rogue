using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEvents : MonoBehaviour
{
    #region References

    private AudioSource _characterAudioSource;

    #endregion


    private void Start()
    {
        GetComponents();
    }

    void GetComponents()
    {
        _characterAudioSource = GetComponent<AudioSource>();
    }
    
    public void OnCharEvent(AnimationEvent e)
    {
        switch (e.stringParameter)
        {
            case "FootDown":
                
                break;
            case "LeftFootDown":
               
                break;
            case "RightFootDown":
                
                break;
            case "Land":
                
                break;
        }  
    }
}
