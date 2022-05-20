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
    public float lastBlockSize;

    
    private void Start() {
        lastBlockSize = StartingBlock.LastBlock.transform.localScale.x;
    }
    // Start is called before the first frame update
    private void Awake() {
        obstacleLeft.SetActive(true);
        obstacleRight.SetActive(true);
        obstacle = this;
        
    }
    public void CheckLastBlockSize(){
        if(lastBlockSize <= 2f){
            lastBlockSize = 2f;
        }
    }
    private void Update() {
        if(GameManager.gameManager.gameOver){
            this.gameObject.SetActive(false);
        }
        CheckLastBlockSize();
        if(StartingBlock.CurrentBlock != null){
            if(StartingBlock.CurrentBlock.transform.position.x > obstacle.transform.position.x){
                
                obstacleEdge = Obstacle.obstacle.transform.position.x + (Obstacle.obstacle.transform.localScale.x / 2);
            } else {
                obstacleEdge = Obstacle.obstacle.transform.position.x - (Obstacle.obstacle.transform.localScale.x / 2);
            }
        }
        
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "WallLeft" || other.gameObject.tag == "WallRight"){

            Destroy(this.gameObject);
        }
    }
}   
    
