using System;
using UnityEngine;

public class FX : MonoBehaviour
{
    [SerializeField] public ParticleSystem cubeExplosionFX;

    [HideInInspector] public Color FxColor;
    [SerializeField] private MeshRenderer cube;
    ParticleSystemRenderer cubeExplosionFXMainModule;
    private Renderer render;
    private FX _fx;
    private ParticleSystem _particle;
    

    public static FX Instance;

    private void Awake()
    {
        // _fx = GetComponent<FX>();
        // _particle = GetComponent<ParticleSystem>();
        Instance = this;
        render = cubeExplosionFX.GetComponent<Renderer>();
    }

    private void Update()
    {
        // if (_particle.particleCount == 0)
        // {
        //     FxSpawner.Instance.DestroyFX(_fx);
        // }
    }

    public void PlayCubeExplosionFX(Vector3 position, Color color)
    {
        FxColor = color;
        render.material.color = new ParticleSystem.MinMaxGradient(color).color;
        
        
        

        cubeExplosionFX.transform.position = position;
        
        cubeExplosionFX.Play();
    }
    
    public void SetColor(Color color)
    {
        FxColor = color;
        render.material.color = color;
        
    }
    
}
