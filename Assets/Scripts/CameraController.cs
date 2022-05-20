using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance {get; set;}
  // Start is called before the first frame update
    public float cameraMoveSpeed = 0.9f;

    public static CameraController MainCamera {get; set;}
    public float topOfCameraY;
    public float totalCameraMove = 0.9f;

    public Vector3 positionY;
    
    [SerializeField]
    //this variable is done to make a canMove check for the camera
    public bool canMove;
 
    void Start() {
        instance = this;
        MainCamera = this;
        canMove = false;
        
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        positionY = new Vector3(transform.position.x,StartingBlock.LastBlock.transform.position.y+3.65f,transform.position.z);
        transform.position = Vector3.Lerp(MainCamera.transform.position, positionY, 0.08f);

    }
}
