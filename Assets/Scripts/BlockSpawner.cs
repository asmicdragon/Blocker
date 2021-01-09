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
        block.transform.position = new Vector3(Random.Range(-4f, 4f), Camera.main.transform.position.y + placeIncrementation * 2, transform.position.z);
        cloneIncrement++;
        
    }
    public void SpawnObstacle(){
        topOfCameraY = CameraController.FindObjectOfType<CameraController>().topOfCameraY;
        var obstacle = Instantiate(obstaclePreFab);
        obstacle.gameObject.SetActive(true);
        obstacle.transform.position = new Vector3(Random.Range(Wall.wall1.transform.position.x - 2, Wall.wall2.transform.position.x + 2), topOfCameraY + 0.25f, transform.position.z);
    }
}
