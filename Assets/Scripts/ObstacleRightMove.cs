using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleRightMove : MonoBehaviour
{
    public static ObstacleRightMove obstacleRight {get; set;}
    // Start is called before the first frame update
    void Start()
    {
        obstacleRight = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(obstacleRight != null){
            if(transform.localScale.x < 3){
                
                transform.localScale += new Vector3(1f,0,0) * (Time.deltaTime * 2);
                transform.position -= new Vector3(0.5f,0,0) * (Time.deltaTime * 2);

            } else if(transform.localScale.x >= 3){

                transform.position -= new Vector3(1f, 0, 0) * (Time.deltaTime * 2);
            }
        } 
    }
}
