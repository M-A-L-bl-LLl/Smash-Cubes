using UnityEngine;
using TMPro;

public class Cube : MonoBehaviour
{
    static int staticID = 0;
    [SerializeField] private TMP_Text[] numbersText;

    [HideInInspector] public int CubeID;
    [HideInInspector] public Color CubeColor;
    [HideInInspector] public int CubeNumber;
    [HideInInspector] public Rigidbody CubeRigidbody;
    [HideInInspector] public bool IsMainCube;
    [HideInInspector] public TrailRenderer trailColor;

    private MeshRenderer cubeMeshRebderer;

    private void Awake()
    {
        CubeID = staticID++;
        cubeMeshRebderer = GetComponent<MeshRenderer>();
        CubeRigidbody = GetComponent<Rigidbody>();
        trailColor = GetComponent<TrailRenderer>();
    }

    public void SetColor(Color color)
    {
        CubeColor = color;
        cubeMeshRebderer.material.color = color;
        trailColor.material.color = color;
    }

    public void SetNumber(int number)
    {
        CubeNumber = number;
        for (int i = 0; i < 6; i++)
        {
            numbersText[i].text = number.ToString();
        }
    }
}
