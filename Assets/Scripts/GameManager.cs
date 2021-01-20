using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager {get; set;}
    public int score;
    public int lives = 3;
    public int combo = 0;
    public bool didTrim;
    public bool moveCamera;
    int obstacleCount;
    public bool collidedWithObstacle = false;

    private void Awake() {

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    private void Start() {
        
        FindObjectOfType<BlockSpawner>().SpawnBlock();  
        StartCoroutine(CreateObstacleRoutine(5));
    }
    
    private void Update()
    {
        // ComboSystem();
        CheckForObstacleCollision();
            
        //pressing escape takes you to the menu
        if(Input.GetKeyDown(KeyCode.Escape)){
            SceneManager.LoadScene(0);
        }

    
 

        //Starts the coroutine of the moving camera
        StartCoroutine(moveCameraRoutine());


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
        

    // }
    // void ComboSystem(){ // Revise combo system
    //     if(StartingBlock.CurrentBlock.hangover > 0.1f){
    //         didTrim = true;
    //     } else {
    //         didTrim = false;
    //     }

    //     if(!didTrim && StartingBlock.CurrentBlock._verticalMovement == 0){
    //         combo++;
    //         Debug.Log("Combo: "+combo);
            
    //     } 
    //     if(didTrim && StartingBlock.CurrentBlock._verticalMovement == 0){
    //         combo = 0;
    //         Debug.Log("Combo: "+combo);
            

    //     }
    }
    void CheckForObstacleCollision(){
        if(collidedWithObstacle){

            collidedWithObstacle = false;
            StartCoroutine(LoseALifeRoutine());
            
            
        }
        if(lives == 0){
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    IEnumerator LoseALifeRoutine(){
        lives--;
        Debug.Log("Lives: "+lives);
        Destroy(StartingBlock.CurrentBlock.gameObject);
        
        yield return new WaitForSeconds(2);
        BlockSpawner.blockSpawner.SpawnBlock();
        
    }
    IEnumerator CreateObstacleRoutine(int seconds) { 

        while(true){

            yield return new WaitForSeconds(seconds);

            //Calls the SpawnObstacle() Method which instantiates a new obstacle
            FindObjectOfType<BlockSpawner>().SpawnObstacle();
            Debug.Log("Spawning every " +seconds+ " seconds");
            

        }
            
    }
    //This will make the moveCamera bool turn to false after 0.5 seconds
    IEnumerator moveCameraRoutine() {
        //checks if moveCamera is true, while its true it will wait 0.5 seconds to turn the bool to false
        while(moveCamera == true){
            yield return new WaitForSeconds(Mathf.Abs(0.525f));
            moveCamera = false;
            }
        }
    }

