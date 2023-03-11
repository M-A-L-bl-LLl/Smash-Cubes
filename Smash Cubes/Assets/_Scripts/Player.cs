using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float pushForce;
    [SerializeField] private float cubeMaxPosX;
    [Space]
    [SerializeField] private TouchSlider touchSlider;
    private Cube mainCube;
    


    private bool isPoinerDown;
    private bool canMove;
    private bool trailOn;
    
    private Vector3 cubePos;


    private void Start()
    {
        SpawnCube();
        canMove = true;
       

        touchSlider.OnPointerDownEvent += OnPointerDown;
        touchSlider.OnPointerDragEvent += OnPointerDrag;
        touchSlider.OnPointerUpEvent += OnPointerUp;
    }

    private void Update()
    {
        if (isPoinerDown & canMove)
        {
            mainCube.transform.position = Vector3.Lerp(
                mainCube.transform.position,
                cubePos,
                moveSpeed * Time.deltaTime);
        }
    }

    private void OnPointerDown()
    {
        isPoinerDown = true;
        
    }

    private void OnPointerDrag(float xMovement)
    {
        if (isPoinerDown )
        {
            trailOn = false;
            if (trailOn == false)
            {
                mainCube.GetComponent<TrailRenderer>().enabled = false;
            }

            
            
            cubePos = mainCube.transform.position;
            cubePos.x = xMovement * cubeMaxPosX;
            
        }
    }

    private void OnPointerUp()
    {
        if (isPoinerDown && canMove)
        {
            isPoinerDown = false;
            canMove = false;
            trailOn = true;
            if (trailOn)
            {
                mainCube.GetComponent<TrailRenderer>().enabled = true;
            }

            //push the cube
            
            mainCube.CubeRigidbody.AddForce(Vector3.forward * pushForce, ForceMode.Impulse);
            
            Invoke("SpawnNewCube", 0.3f);
        }
    }

    private void SpawnNewCube()
    {
        mainCube.IsMainCube = false;
        mainCube.GetComponent<TrailRenderer>().enabled = false;
        mainCube.GetComponent<Animator>().enabled = false;
        SpawnCube();
    }

    private void SpawnCube()
    {
        mainCube = CubeSpawner.Instance.SpawnRandom();
        mainCube.IsMainCube = true;

        Invoke("CanMove", 0.5f);
        mainCube.GetComponent<Animator>().enabled = true;
        mainCube.GetComponent<TrailRenderer>().enabled = false;

        //reset cubePos variable
        cubePos = mainCube.transform.position;
    }

    private void OnDestroy()
    {
        touchSlider.OnPointerDownEvent -= OnPointerDown;
        touchSlider.OnPointerDragEvent -= OnPointerDrag;
        touchSlider.OnPointerUpEvent -= OnPointerUp;
    }

    private void CanMove()
    {
        canMove = true;
    }
}
