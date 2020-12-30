using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Start() {
<<<<<<< HEAD

=======
        
>>>>>>> Kieran
        FindObjectOfType<BlockSpawner>().SpawnBlock();  
    }
    private void Update()
    {
        
        if (Input.GetButtonDown("Jump"))
        {
            if(StartingBlock.CurrentBlock != null) {

            StartingBlock.CurrentBlock.Stop(); 
            Debug.Log(StartingBlock.CurrentBlock);  
            }
        }
        if(StartingBlock.CurrentBlock._verticalMovement >= 0){
            //This will turn the current block into the last block and spawn a new one
            //by making it the lastblock, the game has to automatically wait for the hasStacked variable to turn true before spawning
            StartingBlock.CurrentBlock = StartingBlock.LastBlock;
            FindObjectOfType<BlockSpawner>().SpawnBlock(); 
        }
    }
}
