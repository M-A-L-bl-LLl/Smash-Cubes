using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LocaleLoader : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(nameof(Load));
        
    }

    IEnumerator Load()
    {
        yield return new WaitForSeconds(0.1f);
        if (!PlayerPrefs.HasKey("localeId"))
        {
            PlayerPrefs.SetInt("localeId", 0);
        }
        else if (PlayerPrefs.GetInt("localeId") == 1)
        {
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[1];
        }
    
        
    }
}
