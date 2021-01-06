using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    [SerializeField]
    private StartingBlock blockPreFab;
    
    public int cloneIncrement;
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
}
