using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class RedZone : MonoBehaviour
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
    
    public GameObject Canvas;
    public GameObject CanvasMobile;
    public GameObject Slider;
    public GameObject pauseBtn;
    public GameObject pauseBtnMobile;

    private void OnTriggerStay(Collider other)
    {
        Cube cube = other.GetComponent<Cube>();

        if (cube != null)
        {
            if (!cube.IsMainCube && cube.CubeRigidbody.velocity.magnitude < .1f)
            {
                Debug.Log("Gameover");
                ScoreController.instance.ShowFinalResault();
                if (isMobile()==false)
                {
                    Canvas.gameObject.SetActive(true);
                    pauseBtn.gameObject.SetActive(false);
                }
                else
                {
                    CanvasMobile.gameObject.SetActive(true);
                    pauseBtnMobile.gameObject.SetActive(false);
                }
                
                Slider.gameObject.SetActive(false);
            }
        }
    }
}
