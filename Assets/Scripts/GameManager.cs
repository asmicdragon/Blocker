using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int score;
    public bool moveCamera;
    
    private void Start() {
        
        FindObjectOfType<BlockSpawner>().SpawnBlock();  
        
    }
    
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            SceneManager.LoadScene(0);
        }
        
        StartCoroutine(moveCameraRoutine());

        if (Input.GetButtonDown("Jump") && StartingBlock.CurrentBlock.canPressAgain == true)
        {
            if(StartingBlock.CurrentBlock != null) {

            StartingBlock.CurrentBlock.Stop(); 
            Debug.Log(StartingBlock.CurrentBlock);  
            //This sets the verticalmovement to fall faster when pressing spacebar
            StartingBlock.CurrentBlock._verticalMovement = -3f;

            }
        }

        if(StartingBlock.CurrentBlock._verticalMovement >= 0){
            //This will turn the current block into the last block and spawn a new one
            //by making it the lastblock, the game has to automatically wait for the hasStacked variable to turn true before spawning
            StartingBlock.CurrentBlock = StartingBlock.LastBlock;
            FindObjectOfType<BlockSpawner>().SpawnBlock();

            //sets the moveCamera to true everytime the block is spawned
            moveCamera = true;

            //everytime the block is spawned, the score increments by 1 
            score++;
            Debug.Log("score: " + score);
        } 
        
    }
    //This will make the moveCamera bool turn to false after 0.5 seconds
    IEnumerator moveCameraRoutine() {
        //checks if moveCamera is true, while its true it will wait 0.5 seconds to turn the bool to false
        while(moveCamera == true){
            yield return new WaitForSeconds(Mathf.Abs(0.51f));
            moveCamera = false;
        }
    }
}
