using System;
using System.Collections;
using UnityEngine;
using System.Runtime.InteropServices;

[RequireComponent(typeof(Camera))]
public class CameraCrop : MonoBehaviour {
    
    [DllImport("__Internal")]
    private static extern bool IsMobile();
 
    public bool isMobile()
    {
#if !UNITY_EDITOR && UNITY_WEBGL
             return IsMobile();
#endif
        return false;
    }
    
    [SerializeField, Tooltip("Формат экрана / Screen format")] private Vector2 targetAspect = new Vector2(9, 16);
    [SerializeField, Tooltip("Горизонтальное заполнение / Horizontal crop")] private bool useHorizontalCrop;
    
    ////////////////////////////////
    
    private Camera _camera;
    
    private void Awake () {
        _camera = GetComponent<Camera>();
    }

    private void Update()
    {
        if (isMobile() == false)
        {
            float screenRatio = Screen.width / (float)Screen.height;
            float targetRatio = targetAspect.x / targetAspect.y;

            if(Mathf.Approximately(screenRatio, targetRatio)) {
                _camera.rect = new Rect(0, 0, 1, 1);
            }
            else if(screenRatio > targetRatio) {
                float normalizedWidth = targetRatio / screenRatio;
                float barThickness = (1f - normalizedWidth)/2f;
                _camera.rect = new Rect(barThickness, 0, normalizedWidth, 1);
            }
            else {
                float normalizedHeight = screenRatio / targetRatio;
                float barThickness = (1f - normalizedHeight) / 2f;
                _camera.rect = new Rect(0, barThickness, 1, normalizedHeight);
            }
        }
    }

}