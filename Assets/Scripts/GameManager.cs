using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Start() {

        FindObjectOfType<BlockSpawner>().SpawnBlock();  
    }
    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if(StartingBlock.CurrentBlock != null) {

            StartingBlock.CurrentBlock.Stop(); 
            Debug.Log(StartingBlock.CurrentBlock);
        
            FindObjectOfType<BlockSpawner>().SpawnBlock();   
            }
        }
    }
}
