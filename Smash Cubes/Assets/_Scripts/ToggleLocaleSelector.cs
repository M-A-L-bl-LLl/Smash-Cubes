using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class ToggleLocaleSelector : MonoBehaviour
{
    [SerializeField] private Locale _locale;
    [SerializeField] private int _localeId;
    private Toggle _toggle;

    private void Awake()
    {
        _toggle = gameObject.GetComponent<Toggle>();
        

        if (!PlayerPrefs.HasKey("localeId"))
        {
            PlayerPrefs.SetInt("localeId", 0);
        }
        else if (PlayerPrefs.GetInt("localeId") == _localeId)
        {
            LocalizationSettings.SelectedLocale = _locale;
        }

    }

    private void Update()
    {
        if (LocalizationSettings.SelectedLocale != _locale)
        {
            _toggle.interactable = true;
            _toggle.isOn = true;
        }
        else
        {
            _toggle.interactable = false;
            _toggle.isOn = false;
        }
    }

    public void SelectLocale()
    {
        LocalizationSettings.SelectedLocale = _locale;
        PlayerPrefs.SetInt("localeId", _localeId);
    }
}
