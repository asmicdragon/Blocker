using UnityEngine;

public class CameraController : MonoBehaviour
{
  // Start is called before the first frame update
    public float cameraMoveSpeed = 0.9f;
    [SerializeField]
    //this variable is done to make a canMove check for the camera
    bool canMove;
 
    void Start() {
        
    }
    // Update is called once per frame
    void Update()
    {
        //importing the moveCamera variable from the GameManager and make it equal to canMove
        canMove = GameManager.FindObjectOfType<GameManager>().moveCamera;

        if(canMove == true){ //if canMove is true, the camera can move upwards to the movement speed * deltaTime which is per second

            transform.Translate(Vector3.up * Time.deltaTime * cameraMoveSpeed);
        } 
    }
}
