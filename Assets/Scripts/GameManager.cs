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
        
            FindObjectOfType<BlockSpawner>().SpawnBlock();   
            }
        }
    }
}
