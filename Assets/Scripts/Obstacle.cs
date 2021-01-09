using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public static Obstacle obstacle {   get; set;   }
    // Start is called before the first frame update
    private void Awake() {
        
        if(obstacle == null){

            obstacle = this;
            StartingBlock.ObstacleBlock = GameObject.FindWithTag("Obstacle").GetComponent<StartingBlock>();
            Debug.Log("Obstacle name is "+obstacle.name);
        
        }
    }
    private void Start() {

        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(transform.localScale.x <3) {
        
        transform.localScale +=  new Vector3(1f,0,0)* (Time.deltaTime * 2);
        transform.position +=  new Vector3(0.5f,0,0)* (Time.deltaTime * 2);
        }  else if(transform.localScale.x >= 3){
            
            transform.position +=  new Vector3(1f,0,0) * (Time.deltaTime * 2);
            
        }
    }
    
    
}
