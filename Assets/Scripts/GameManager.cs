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

    public GameObject StaminaBar;

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
    public int lifeCombo = 0;
    public int treasureCombo = 0;

    public float growthPercent;
    public int growthCombo = 0;
    public int growthComboMax = 4;
    public int lifeComboMax = 8;//default value on level 1 life combo
    public int treasureCallPoint = 3;

    int treasureMax;
    int treasureMin;
    public int lifeUpgrade;
    public int treasureUpgrade;
    public int growthUpgrade;
    public int coins = 0;
    float coinsF = 0;

    public int levelUPCoins;
    public int globalCoins = 0;
    public int currentXP, targetXP, xpThisRound;
    public int currentLevel = 1;

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
    public bool isXPAdded;
    bool levelUp = false;
    bool checkout = false;
    bool checkProgressDone;
    public bool stopProgress;

    public bool slowDescentActivated = false;
    public bool fastDescentActivated = false;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        stackBlock = GameObject.FindWithTag("Stack").GetComponent<StartingBlock>();
        currentXP = PlayerPrefs.GetInt("currentxp", 0);


        isXPAdded = false;
    }
    private void Start() {

        PauseGameOnLoad();

        IEnumerator WaitForFade(){FadeScreen.instance.playFade = true; yield return new WaitForSecondsRealtime(2f); FadeScreen.instance.playFade = false;}
        
        StartCoroutine(WaitForFade());
        
        //getting the highscore from the player prefs, if it is not there, it will be zero
        highScore = PlayerPrefs.GetInt(highScoreKey, 0);

        lifeUpgrade = PlayerPrefs.GetInt("lifeupgrade", 0);
        growthUpgrade = PlayerPrefs.GetInt("growthupgrade", 0);
        treasureUpgrade = PlayerPrefs.GetInt("treasureupgrade", 0);
        treasureMax = PlayerPrefs.GetInt("treasuremax", 0);
        treasureMin = PlayerPrefs.GetInt("treasuremin", 0);
        growthPercent = PlayerPrefs.GetFloat("growthpercent", 0);
        

        Debug.Log("life upgrade is: "+lifeUpgrade.ToString());
        colorType = PlayerPrefs.GetInt(colorTypeKey, 0);
        masterVolume = PlayerPrefs.GetFloat(volumeKey, 1.0f);
        globalCoins = PlayerPrefs.GetInt("globalCoins", 0);
        difficultySeconds = seconds;
        ResetLifeCombo();
        ResetGrowthCombo();
        
        currentLevel = PlayerPrefs.GetInt("currentlevel", 1);
        targetXP =  Mathf.FloorToInt(((currentLevel*(currentLevel))/15) * 100) + 2800;
        levelUPCoins = Mathf.FloorToInt(((currentLevel*(currentLevel))/30) * 100) + 1000;
        
        checkProgressDone = false;
        stopProgress = false;

        FindObjectOfType<BlockSpawner>().SpawnBlock();  
        StartCoroutine(CreateObstacleRoutine());

        CheckForLifeUpgrades();

        LoadColorMode();
        LoadVolume();
    }
    void PauseGameOnLoad()
    {
        Time.timeScale = 0;
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
    void CheckForLifeUpgrades()
    {
        switch (lifeUpgrade)
        {
            case 1: lifeComboMax = 8;
            Debug.Log("Life Combo Max is: "+ lifeComboMax.ToString());
                break;
            case 2: lifeComboMax = 6;
            Debug.Log("Life Combo Max is: "+ lifeComboMax.ToString());
                break;
            case 3: lifeComboMax = 5;
            Debug.Log("Life Combo Max is: "+ lifeComboMax.ToString());
                break;
            default: Debug.Log("Life Combo is locked");
                break;
        }
    }
    public void Growth()
    {
        if(growthCombo >= growthComboMax && StartingBlock.LastBlock.transform.localScale.x < 2.3f)
        {
            StartingBlock.LastBlock.transform.localScale = new Vector3(StartingBlock.LastBlock.transform.localScale.x * growthPercent,StartingBlock.LastBlock.transform.localScale.y, StartingBlock.LastBlock.transform.localScale.z);
            Debug.Log("Growth");
            
        }
    }
    public void FindTreasure()
    {
        
        
        
        //Match current combo with the random number
        if(combo >= treasureCallPoint)//Strike point at which the player enters the chance to find treasure
        {
            int comboRange = Random.Range(3,202);// 1/200 odds
            
            int randomCoins = Random.Range(treasureMin, treasureMax);
            
            if(combo >= comboRange){

                //The higher your combo, the higher the chance 
                coins += randomCoins;
                Debug.Log("You won "+randomCoins + " coins!");
                SaveCoins();
                ResetCombo();
            }
        }
    }
    public void CheckForSkills()
    {
        if(ShopManager.instance.haveSlowDescent == 1)
        {
            slowDescentActivated = true;
        
            if(slowDescentActivated && !GameManager.gameManager.gameOver)
            {
                StaminaBar.SetActive(true);
                
            } else StaminaBar.SetActive(false);
            

        } else slowDescentActivated = false;

        if(ShopManager.instance.haveFastDescent == 1)
        {
            fastDescentActivated = true;
        
            if(fastDescentActivated && !GameManager.gameManager.gameOver)
            {
                StaminaBar.SetActive(true);
                
            } else StaminaBar.SetActive(false);
            

        } else fastDescentActivated = false;
    }
    public void SaveAll()
    {
        PlayerPrefs.Save();
    }
    
    public void LevelUp()
    {
        SaveTargetXP();
        if(!checkProgressDone)
        {
            XPBarSlider.instance.progress = Mathf.FloorToInt(XPBarSlider.instance.xpBarSlider.value) + xpThisRound;
            checkProgressDone = true;
            //this is done so the progress is calculated based on the starting bar value and the xp this round.
            //it only calculates the progress once per round so that it doesn't loop on itself.
        }
        

        if(currentXP >= targetXP)
        {
            currentXP -= targetXP;
            XPBarSlider.instance.newProgress = currentXP;
            
            levelUp = true;

            
            SaveCurrentXP();
            
        }

    }
    public void SaveLevelUpReward()
    {
        levelUPCoins = Mathf.FloorToInt(((currentLevel*(currentLevel))/30) * 100) + 1000;
        PlayerPrefs.SetInt("levelupcoins", levelUPCoins);
        PlayerPrefs.Save();
        LevelUpReward.instance.rewardCoins = levelUPCoins;
    }
    public void SaveCoins()
    {
        PlayerPrefs.SetInt("globalCoins", coins);
        PlayerPrefs.Save();
    }

    public void SaveTargetXP()
    {
        targetXP =  Mathf.FloorToInt(((currentLevel*(currentLevel))/15) * 100) + 2800;
        XPBarSlider.instance.xpBarSlider.maxValue = targetXP;
        PlayerPrefs.SetInt("targetxp", targetXP);
        PlayerPrefs.Save();
    }
    public void SaveCurrentXP()
    {
        PlayerPrefs.SetInt("currentxp", currentXP);
        PlayerPrefs.Save();
    }
    public void SaveLevel()
    {
        PlayerPrefs.SetInt("currentlevel", currentLevel);
        PlayerPrefs.Save();
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
        coinsF *= 0.90f; //-10%
        coins = Mathf.FloorToInt(coinsF);
    }
    private void Update()
    {
        OnSpacePressed();
        
        coinsF = coins; //This makes coins into coinsF so that we can convert float into int
        PlayDestroySound();
        PlayStackSound();
        CheckForObstacleCollision();
        //Saving the highscore
        SaveHighScore();
        
        CheckForSkills();

        IncrementGlobalCoins();
        ResetHighScore();
        //pressing escape takes you to the menu
        if(Input.GetKeyDown(KeyCode.Escape)){
            SceneManager.LoadScene(0);
        }


        if(StartingBlock.CurrentBlock._verticalMovement >= 0){
            //This will turn the current block into the last block and spawn a new one
            StartingBlock.CurrentBlock = StartingBlock.LastBlock;
            FindObjectOfType<BlockSpawner>().SpawnBlock();

            //sets the moveCamera to true everytime the block is spawned
            moveCamera = true;

            //everytime the block is spawned, the score increments by 1 
            score++;
            xpThisRound = score * 100;

        } 
    }    

    void FixedUpdate()
    {
        //Starts the coroutine of the moving camera
        // StartCoroutine(moveCameraRoutine());

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
    public void WaitAndResume()
    {
        IEnumerator WaitForFade(){yield return new WaitForSecondsRealtime(1f); Time.timeScale = 1;FadeScreen.instance.playFade = false;}
        StartCoroutine(WaitForFade());
    }
    public void Fade()
    {
        FadeScreen.instance.playFade = true;
        FadeScreen.instance.fadeOut = true;
    }
    void OnSpacePressed()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Fade();
            if(FadeScreen.instance.fadeOut)
            {
                Time.timeScale = 1; 
            }
            

        }
    }
    public void ResetLifeCombo(){
        lifeCombo = 0;
        
    }
    public void ResetGrowthCombo(){
        growthCombo = 0;
        
    }
    public void ResetCombo(){
        combo = 0;
        
    }
    public void ComboLifeSystem(){
        
        if(lifeUpgrade != 0) {//checks if life combo is bought

            if(lifeCombo == lifeComboMax && StartingBlock.CurrentBlock.colliding == 0 && lives < 3)  {
                lives++;
                Debug.Log("Lives are now: "+lives);
                ResetLifeCombo();
                
            }
        }
        //Gain a life every 8 perfect stacks in a row
    }

    public void ComboIncrementation(){
        coins += 10 + (combo * 10);
        combo++;
        lifeCombo++;
        growthCombo++;
        
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
        if(score >= 30 && canIncrease && StartingBlock.CurrentBlock.transform.localScale.x < 2.5f && obstacleMovement < 2){
            obstacleMovement += 0.25f * Time.deltaTime;
            canIncrease = false;
            //finally setting the spawning speed to 2 seconds
            seconds = 1.8f;
        }
        if(StartingBlock.CurrentBlock.transform.localScale.x < 2.0f){
            canIncrease = true;
            if(score >= 50 && canIncrease && obstacleMovement < 2){
                obstacleMovement += 0.25f * Time.deltaTime;
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

}

