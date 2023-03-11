using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioChange : MonoBehaviour
{
    private void Awake()
    {
        
        
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            AudioListener.volume = PlayerPrefs.GetFloat("musicVolume");
        }
        else
        {
            AudioListener.volume = PlayerPrefs.GetFloat("musicVolume");
        }
           
        
        if (!PlayerPrefs.HasKey("muted"))
        {
            PlayerPrefs.SetInt("muted", 1);
            gameObject.GetComponent<AudioSource>().enabled = true;
        }
        else
        {
            gameObject.GetComponent<AudioSource>().enabled = PlayerPrefs.GetInt("muted") == 1; 
        }
    }
}
