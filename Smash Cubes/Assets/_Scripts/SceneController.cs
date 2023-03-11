using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Toggle = UnityEngine.UI.Toggle;


public class SceneController : MonoBehaviour
{
    #region Dll

    [DllImport("__Internal")]
        private static extern bool IsMobile();
     
        public bool isMobile()
        {
    #if !UNITY_EDITOR && UNITY_WEBGL
                 return IsMobile();
    #endif
            return false;
        }

    #endregion
    
    [SerializeField] private TMP_Text tapToStart;
    [SerializeField] private TMP_Text score;
    [SerializeField] private TouchSlider touchSlider;
    [SerializeField] private GameObject help;
    [SerializeField] private GameObject startPanel;
    [Header("Pause")]
    [SerializeField] private GameObject pauseBtn;
    [SerializeField] private GameObject holder;
    [SerializeField] private GameObject pauseBtnMobile;
    [SerializeField] private GameObject pauseBg;
    [SerializeField] private GameObject pauseBgMobile;
    [SerializeField] private GameObject holderMobile;
    //[SerializeField] private Toggle vibration;
    public ScoreController _ScoreController;
    public static bool vibrate;
    public static bool sound;
    private bool isGameStarted;

   
    public void StartGame()
    {
        Application.targetFrameRate = Screen.currentResolution.refreshRate;
        
        tapToStart.gameObject.SetActive(false);
        touchSlider.gameObject.SetActive(true);
        if (isMobile() == false)
        {
            pauseBtn.gameObject.SetActive(true);
            holder.gameObject.SetActive(true);
        }
        else
        {
            pauseBtnMobile.gameObject.SetActive(true);
            holderMobile.gameObject.SetActive(true);
        }
        
        score.gameObject.SetActive(true);
        help.gameObject.SetActive(true);
        startPanel.gameObject.SetActive(false);
        isGameStarted = true;
        
    }

    private void Awake()
    {
       
        
        vibrate = (PlayerPrefs.GetInt("Vibration") != 0);

        if (vibrate)
        {
            //vibration.isOn = true;
        }
        else
        {
            //vibration.isOn = false;
        }
    }

    private void Start()
    {
        tapToStart.gameObject.SetActive(true);
        touchSlider.gameObject.SetActive(false);
        score.gameObject.SetActive(false);
        isGameStarted = false;
        
    }

    public void OnBackBtnPause()
    {
        score.gameObject.SetActive(true);
        touchSlider.gameObject.SetActive(true);
        help.gameObject.SetActive(false);
        if (isMobile() == false)
        {
            pauseBtn.gameObject.SetActive(true);
            pauseBg.gameObject.SetActive(false);
        }
        else
        {
            pauseBtnMobile.gameObject.SetActive(true);
            pauseBgMobile.gameObject.SetActive(false);
        }
        
        
    }
    public void OnPauseBtn()
    {
        score.gameObject.SetActive(false);
        touchSlider.gameObject.SetActive(false);
        help.gameObject.SetActive(false);
        if (isMobile() == false)
        {
            pauseBtn.gameObject.SetActive(false);
            pauseBg.gameObject.SetActive(true);
        }
        else
        {
            pauseBtnMobile.gameObject.SetActive(false);
            pauseBgMobile.gameObject.SetActive(true);
        }
       
        
    }
    public void ReloadGame()
    {
        _ScoreController.sdk.ShowRewarded("Play");
        SceneManager.LoadScene("Game");
    }

    public void OnVibrationBtn()
    {
        //vibrate = (vibration.isOn) ? true : false;
        PlayerPrefs.SetInt("Vibration", (vibrate ? 1 : 0));
    }
}
