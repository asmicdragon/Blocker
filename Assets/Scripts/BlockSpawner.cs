using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    [SerializeField]
    private StartingBlock blockPreFab;

    public void SpawnBlock()
    {
        //This class file is done so that the block spawns after being placed
        var block = Instantiate(blockPreFab);
        // placing the position of the new block at this flat position
        block.transform.position = new Vector2(transform.position.x, 1.47f);
    }
}
