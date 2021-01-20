using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    [SerializeField]
    private StartingBlock blockPreFab;
    public static BlockSpawner blockSpawner {get; set;}

    [SerializeField]
    private Obstacle obstacleLeftPreFab;
    [SerializeField]
    private Obstacle obstacleRightPreFab;
    public int spawnDirection = 0;
    public int cloneIncrement;
    public float topOfCameraY;
    private float placeIncrementation;
    [SerializeField]
    private Wall wallRightPreFab;
    [SerializeField]
    private Wall wallLeftPreFab;

    private void Start() {
        blockSpawner = this;
        SpawnWalls();
    }
    public void SpawnBlock()
    {
        placeIncrementation = CameraController.FindObjectOfType<CameraController>().cameraMoveSpeed;
        //This class file is done so that the block spawns after being placed
        var block = Instantiate(blockPreFab);
        block.name = "StartingBlock " + cloneIncrement;
        // placing the position of the new block at this flat position
        block.transform.position = new Vector3(Random.Range(-2f, 2f), Camera.main.transform.position.y + placeIncrementation * 2, transform.position.z);
        cloneIncrement++;
        
    }
    public void SpawnObstacle(){
        
        spawnDirection = Random.Range(1,100);

        topOfCameraY = CameraController.FindObjectOfType<CameraController>().topOfCameraY;

        if(spawnDirection <= 49){
            var obstacleLeft = Instantiate(obstacleLeftPreFab);
            obstacleLeft.gameObject.SetActive(true);
            obstacleLeft.transform.position = new Vector3(Wall.wallLeft.transform.position.x + 0.6f, Camera.main.transform.position.y + 0.6f, transform.position.z);
            
        } else {

            if(spawnDirection >= 50){
                var obstacleRight = Instantiate(obstacleRightPreFab);
                obstacleRight.gameObject.SetActive(true);
                obstacleRight.transform.position = new Vector3(Wall.wallRight.transform.position.x - 0.6f, Camera.main.transform.position.y + 0.6f, transform.position.z);

                
            }
        }
    
    }
    public void SpawnWalls(){
        var wallLeft = Instantiate(wallLeftPreFab);
        var wallRight = Instantiate(wallRightPreFab);
        wallLeft.transform.position = new Vector3(Wall.wallLeft.transform.position.x, Wall.wallLeft.transform.position.y + 10, transform.position.z);
        wallRight.transform.position = new Vector3(Wall.wallRight.transform.position.x, Wall.wallRight.transform.position.y + 10, transform.position.z);

    }
}
