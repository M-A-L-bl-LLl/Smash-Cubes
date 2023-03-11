using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FxSpawner : MonoBehaviour
{
    public static FxSpawner Instance;
    Queue<FX> fxQueue = new Queue<FX>();
    [SerializeField] private GameObject fxPrefab;
    [SerializeField] private int fxQueueCapacity = 20;
    [SerializeField] private bool autoQueueGrow = true;
    private Vector3 spawnPosition;
    private void Awake()
    {
        Instance = this;
        spawnPosition = transform.position;
        
        InitializeFxQueue();
    }
    private void InitializeFxQueue()
    {
        for (int i = 0; i < fxQueueCapacity; i++)
        {
            AddFxToQueue();
        }
    }

    public FX Spawn(Vector3 position, Color fxColor)
    {
        if (fxQueue.Count == 0)
        {
            if (autoQueueGrow)
            {
                fxQueueCapacity++;
                AddFxToQueue();
            }
            else
            {
                Debug.LogError("[FX Queue] : no more fx available in the pool");
                return null;
            }
        }

        FX fx = fxQueue.Dequeue();
        fx.transform.position = position;
        fx.SetColor(fxColor);
        fx.gameObject.SetActive(true);
        fx.cubeExplosionFX.Play();
        
        return fx;
    }
    
    private void AddFxToQueue()
    {
        FX fx = Instantiate(fxPrefab, spawnPosition, Quaternion.identity, transform)
            .GetComponent<FX>();

        fx.gameObject.SetActive(false);
        
        fxQueue.Enqueue(fx);
    }
    
    public void DestroyFX(FX fx)
    {
        
        //fx.gameObject.SetActive(false);
        Destroy(fx.gameObject);
        fxQueue.Enqueue(fx);
    }
}
