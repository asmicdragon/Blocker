using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall2 : MonoBehaviour
{
    private static Wall2 Wall02 {get; set;}
    // Start is called before the first frame update
    bool moveCamera;
    float sizeIncrementation;
    void Start()
    {
        Wall02 = this;
        moveCamera = FindObjectOfType<GameManager>().moveCamera;
        sizeIncrementation = FindObjectOfType<CameraController>().cameraMoveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if(StartingBlock.CurrentBlock._verticalMovement == 0 && transform.localScale.y < 30){
            Debug.Log("Wall has scaled");
            Vector3 wallScale = new Vector3(transform.localScale.x, transform.localScale.y + sizeIncrementation, transform.localScale.z);
            transform.localScale = new Vector3(wallScale.x, wallScale.y, wallScale.z);
            transform.position = new Vector3(transform.localPosition.x, transform.position.y + (sizeIncrementation / 2), transform.localPosition.z);
        
        } else if(StartingBlock.CurrentBlock._verticalMovement == 0){

            transform.position = new Vector3(transform.localPosition.x, transform.position.y + (sizeIncrementation / 2) , transform.localPosition.z);

        }
    }
}
