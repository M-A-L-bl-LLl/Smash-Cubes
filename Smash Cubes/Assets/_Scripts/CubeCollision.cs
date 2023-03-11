using UnityEngine;


public class CubeCollision : MonoBehaviour
{
    Cube cube;
    public static int cubeNumberToScore;
    private AudioSource _sound;
    private void Awake()
    {
        cube = GetComponent<Cube>();
       // Vibration.Vibration.Init();
       _sound = GameObject.Find("CubeSound").GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Cube otherCube = collision.gameObject.GetComponent<Cube>();

        //check if contacted with other cube
        if (otherCube != null && cube.CubeID > otherCube.CubeID)
        {
            //chek if both cubes have same number
            if (cube.CubeNumber == otherCube.CubeNumber)
            {
                Debug.Log("HIT : " + cube.CubeNumber);
                Vector3 contactPoint = collision.contacts[0].point;

                //chek if cube number less than max number in CubeSpawner
                if (otherCube.CubeNumber < CubeSpawner.Instance.maxCubeNumber)
                {
                    //Spawn a new cube as a resault
                    Cube newCube = CubeSpawner.Instance.Spawn(cube.CubeNumber*2, contactPoint+Vector3.up*1.3f); //1.3f
                    //push the new cube up and forward:
                    float pushForce = 2.5f; // 0.5f
                    newCube.CubeRigidbody.AddForce(new Vector3(0, .3f, 1f) * pushForce, ForceMode.Impulse); //0, .3f, 0.3f

                    cubeNumberToScore = cube.CubeNumber * 2;
                    //add score:
                    ScoreController.instance.AddScore();


                    //add same  torque:
                    float randomValue = Random.Range(-20f, 20f);
                    Vector3 randomDirection = Vector3.one * randomValue;
                    newCube.CubeRigidbody.AddTorque(randomDirection);
                }
                else
                {
                    return;
                }

                //the explosion should affect surrounded cubes too:
                Collider[] surroundedCubes = Physics.OverlapSphere(contactPoint, 2f);
                float explosionForce = 130f; //150f
                float explosianRadius = 1.5f; //1f

                foreach (Collider coll in surroundedCubes)
                {
                    if (coll.attachedRigidbody != null)
                    {
                        coll.attachedRigidbody.AddExplosionForce(explosionForce, contactPoint, explosianRadius);
                    }
                }

                
                //FX.Instance.PlayCubeExplosionFX(contactPoint, cube.CubeColor);
                FxSpawner.Instance.Spawn(contactPoint, cube.CubeColor);
                //Destroy the two cubes:
                CubeSpawner.Instance.DestroyCube(cube);
                CubeSpawner.Instance.DestroyCube(otherCube);
                _sound.Play();
                if (SceneController.vibrate == true)
                {
                    // Vibration.Vibration.VibratePop();
                    // Debug.Log("Vibration on");
                }
                else
                {
                    //Debug.Log("Vibration off");
                    //return;
                }
                
            }
        }
    }
}
