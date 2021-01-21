using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public static Obstacle obstacle {   get; set;   }
    public GameObject obstacleLeft;
    public GameObject obstacleRight;
    public float SpawnDirection;
    public float obstacleEdge;
    public float hangoverOnObstacle;

    // Start is called before the first frame update
    private void Awake() {
        obstacleLeft.SetActive(true);
        obstacleRight.SetActive(true);
        obstacle = this;
        
    }
    private void Update() {

        if(StartingBlock.CurrentBlock != null){
            if(StartingBlock.CurrentBlock.transform.position.x > obstacle.transform.position.x){
                
                obstacleEdge = Obstacle.obstacle.transform.position.x + (Obstacle.obstacle.transform.localScale.x / 2);
            } else {
                obstacleEdge = Obstacle.obstacle.transform.position.x - (Obstacle.obstacle.transform.localScale.x / 2);
            }
        }
        
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.collider.tag == "StartingBlock"){
            
            GameManager.gameManager.collidedWithObstacle = true;
            
            
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        
        if(other.gameObject == Wall.wallLeft.gameObject || other.gameObject == Wall.wallRight.gameObject){

            Destroy(this.gameObject);
        }
    }
}   
    
