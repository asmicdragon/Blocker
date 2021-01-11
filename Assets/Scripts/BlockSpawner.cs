using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    [SerializeField]
    private StartingBlock blockPreFab;

    [SerializeField]
    private Obstacle obstaclePreFab;

    public int cloneIncrement;
    public float topOfCameraY;
    private float placeIncrementation;
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
        int spawnDirection = 0;
        spawnDirection += Random.Range(0,99);

        topOfCameraY = CameraController.FindObjectOfType<CameraController>().topOfCameraY;
        var obstacle = Instantiate(obstaclePreFab);
        obstacle.gameObject.SetActive(true);
        
        if(spawnDirection <= 49){
            obstacle.transform.position = new Vector3(Wall.wall2.transform.position.x + 0.6f, Camera.main.transform.position.y + 0.6f, transform.position.z);
        } else {

        if(spawnDirection >= 50){
            obstacle.transform.position = new Vector3(Wall.wall1.transform.position.x - 0.6f, Camera.main.transform.position.y + 0.6f, transform.position.z);

        }
    }
}

}
