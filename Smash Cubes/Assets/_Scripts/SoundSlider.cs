using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SoundSlider : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    private Slider _volumeSlider;
    private int value;
    private void Awake()
    {
       _volumeSlider = gameObject.GetComponent<Slider>();

       
    }

    private void Start()
    {
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            Load();
        }
        else
        {
            Load();
        }
    }

    public void ChangeVolume()
    {
        AudioListener.volume = _volumeSlider.value;
        _text.text = Mathf.FloorToInt(_volumeSlider.value*100).ToString();
        Save();
    }

    private void Load()
    {
        _volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
        _text.text = Mathf.FloorToInt(_volumeSlider.value*100).ToString();
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", _volumeSlider.value);
    }
}
