using UnityEngine;
using UnityEngine.UI;

public class MusicToggle : MonoBehaviour
{
    private Toggle _toggle;
    private AudioSource _music;

    private void Awake()
    {
        _toggle = gameObject.GetComponent<Toggle>();
        _music = Camera.main.GetComponent<AudioSource>();

        if (!PlayerPrefs.HasKey("muted"))
        {
            PlayerPrefs.SetInt("muted", 0);
            Load();
        }
        else
        {
            Load();
        }
    }

    public void OnBtnPress()
    {
        if (_toggle.isOn)
            _music.enabled = true;
        else
            _music.enabled = false;
        
        Save();
    }

    private void Save()
    {
        PlayerPrefs.SetInt("muted", _toggle.isOn ? 1 : 0);
    }

    private void Load()
    {
        _toggle.isOn = PlayerPrefs.GetInt("muted") == 1;
    }
}
