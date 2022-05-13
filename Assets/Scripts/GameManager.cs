using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    /*This class file contains most global variables that need to stick, Sound, Combos, and certain collision-
    variables are all in this class file*/
    public static GameManager gameManager {get; set;}
    public static StartingBlock stackBlock {get; set;}

    public AudioSource audioSource;
    public AudioClip audio_Stack;
    public AudioClip audio_OnDestroy;

    //we use these 2 variable for the color modes to move from one scene to another
    public int colorType;
    string colorTypeKey = "ColorType";
    public int score;
    public int highScore = 0;
    public string highScoreKey = "HighScore";

    public float masterVolume = 0.0f;
    string volumeKey = "Volume";
    public int lives = 3;
    public int combo = 0;
    public int maxCombo = 8;

    public int coins = 0;
    float coinsF = 0;
    public int globalCoins = 0;

    //int seconds is used for the obstacle spawning routine, so that we can adjust the progression of the game through this variable
    public float seconds = 5; //Original spawning speed is set to 5.
    public float difficultySeconds;
    public float obstacleMovement = 1.5f;
    public const float comboGrowth = 0.1f;
    public float comboMaxGrowth;
    public bool moveCamera;
    public bool collidedWithObstacle = false;
    public bool gameOver = false;
    public bool canDecrease = true;
    public bool canIncrease = true;
    public bool playDestroySound = false;
    public bool playStackSound = false;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        stackBlock = GameObject.FindWithTag("Stack").GetComponent<StartingBlock>();
    }
    private void Start() {
        //getting the highscore from the player prefs, if it is not there, it will be zero
        highScore = PlayerPrefs.GetInt(highScoreKey, 0);
        colorType = PlayerPrefs.GetInt(colorTypeKey, 0);
        masterVolume = PlayerPrefs.GetFloat(volumeKey, 1.0f);
        globalCoins = PlayerPrefs.GetInt("globalCoins", 0);
        difficultySeconds = seconds;
        

        FindObjectOfType<BlockSpawner>().SpawnBlock();  
        StartCoroutine(CreateObstacleRoutine());

        LoadColorMode();
        LoadVolume();
    }
    //loads the color mode type from the color blind menu
    void LoadColorMode(){
        //Inputting the colorType to the Colorblind script to change the colormode
        Wilberforce.Colorblind.colorBlind.Type = colorType;
    }
    //this loads the volume from playerprefs
    void LoadVolume(){
        AudioListener.volume = masterVolume;
    }
    //Saves the highscore
    void SaveHighScore() {
        if(gameOver){
            if(score > highScore){
                //if the score is greater than the highscore, we input the score into the highscore.
                PlayerPrefs.SetInt(highScoreKey, score);
                PlayerPrefs.Save();
            }
        }
    }
        void IncrementGlobalCoins() {
        if(gameOver){
                //if the score is greater than the highscore, we input the score into the highscore.
                PlayerPrefs.SetInt("globalCoins", globalCoins + coins);
                PlayerPrefs.Save();
            
        }
    }
    //This reset is to test out the help menu which works on the highscore being zero
    void ResetHighScore(){
        if(Input.GetKey(KeyCode.P)){
            PlayerPrefs.DeleteAll();
        }
    }
    public void ReduceCoins() {
        coinsF *= 0.85f; //-20%
        coins = Mathf.FloorToInt(coinsF);
    }
    private void Update()
    {
        coinsF = coins; //This makes coins into coinsF so that we can convert float into int
        PlayDestroySound();
        PlayStackSound();
        CheckForObstacleCollision();
        //Saving the highscore
        SaveHighScore();
        IncrementGlobalCoins();
        ResetHighScore();
        //pressing escape takes you to the menu
        if(Input.GetKeyDown(KeyCode.Escape)){
            SceneManager.LoadScene(0);
        }

        //Starts the coroutine of the moving camera
        StartCoroutine(moveCameraRoutine());

        if(StartingBlock.CurrentBlock._verticalMovement >= 0){
            //This will turn the current block into the last block and spawn a new one
            StartingBlock.CurrentBlock = StartingBlock.LastBlock;
            FindObjectOfType<BlockSpawner>().SpawnBlock();

            //sets the moveCamera to true everytime the block is spawned
            moveCamera = true;

            //everytime the block is spawned, the score increments by 1 
            score++;

        } 
    }    
    public void PlayDestroySound(){
        if(playDestroySound) {
            audioSource.pitch = 1f;
            audioSource.clip = audio_OnDestroy;
            audioSource.Play();
            playDestroySound = false;
        }
    }
    //Playing the stacking sound
    public void PlayStackSound(){
        if(playStackSound){
            audioSource.pitch = 1.5f;
            audioSource.clip = audio_Stack;
            audioSource.Play();
            playStackSound = false;
        }
    }
    //Playing the destroyed sound

    public void PauseGame(){
        // pauses the game
        Time.timeScale = 0;
    }
    public void ResumeGame(){
        // resumes the game
        Time.timeScale = 1;
    }
    public void ResetCombo(){
        combo = 0;
        Debug.Log("Combo has been reset");
    }
    public void ComboLifeSystem(){
        
        if(combo == maxCombo && StartingBlock.CurrentBlock.colliding == 0 && lives < 3)  {
            lives++;
            Debug.Log("Lives are now: "+lives);
            

        }
        //Gain a life every 8 perfect stacks in a row
        if(combo >= maxCombo){
            //resetting combo every 8 perfect stacks
            ResetCombo();
        }
    }

    public void ComboIncrementation(){
        coins += 10 + (combo * 10);
        combo++;
        
    }

    public void SpawnWallsRoutine(){
        //Sadly this part of the code would have been implemented into the game, but since for some odd reason the script doesn't stick into the build
        //We decided to leave this out
        if(Wall.wallLeft.transform.position.y < Camera.main.transform.position.y - 5){
            BlockSpawner.blockSpawner.SpawnWalls();
        }
    }
    void CheckForObstacleCollision(){
        //This is to check for the obstacle collision from the obstacle script, and implement the collision into the gamemanager class file so that it sticks even when the obstacle is destroyed.
        if(collidedWithObstacle){

            collidedWithObstacle = false;
            StartCoroutine(LoseALifeRoutine());
            
            
        }

    }
    public IEnumerator LoseALifeRoutine(){
        lives--;
        Debug.Log("Lives: "+lives);
        Destroy(StartingBlock.CurrentBlock.gameObject);
        
        yield return new WaitForSeconds(2);
        BlockSpawner.blockSpawner.SpawnBlock();
        
    }
    public void DifficultyProgression(){
        //This method will be used for the games progression
        
        if(score >= 15 && canDecrease && difficultySeconds != 3){
            difficultySeconds--;
            seconds = difficultySeconds;
            canDecrease = false;

        }
        if(StartingBlock.CurrentBlock.transform.localScale.x < 2.5f){
            canDecrease = true;
        }
        //This part will be the obstacle speed increase
        if(score >= 30 && canIncrease && StartingBlock.CurrentBlock.transform.localScale.x < 1.7f && obstacleMovement < 2){
            obstacleMovement += 0.25f;
            canIncrease = false;
            //finally setting the spawning speed to 2 seconds
            seconds = 2f;
        }
        if(StartingBlock.CurrentBlock.transform.localScale.x < 0.9f){
            canIncrease = true;
            if(score >= 50 && canIncrease && obstacleMovement < 2){
                obstacleMovement += 0.25f;
                canIncrease = false;
            }
        }

    }
    IEnumerator CreateObstacleRoutine() { 

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
            yield return new WaitForSeconds(Mathf.Abs(0.5156f));
            moveCamera = false;
            }
        }
    }

