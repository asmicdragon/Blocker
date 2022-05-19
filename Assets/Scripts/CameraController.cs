using UnityEngine;

public class CameraController : MonoBehaviour
{
  // Start is called before the first frame update
    public float cameraMoveSpeed = 0.9f;

    public static CameraController MainCamera {get; set;}
    public float topOfCameraY;
    [SerializeField]
    //this variable is done to make a canMove check for the camera
    bool canMove;
 
    void Start() {
        
        MainCamera = this;
        
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        //importing the moveCamera variable from the GameManager and make it equal to canMove
        canMove = GameManager.FindObjectOfType<GameManager>().moveCamera;

        if(canMove == true){ //if canMove is true, the camera can move upwards to the movement speed * deltaTime which is per second

            transform.Translate(Vector3.up * Time.deltaTime * cameraMoveSpeed);
            topOfCameraY = MainCamera.transform.position.y + 5.5f;

        } 
    }
}
