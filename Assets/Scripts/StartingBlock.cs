using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
public class StartingBlock : MonoBehaviour
{
    //currentblock variable to declare the block so that we can have 2 different blocks, the one current and the one placed.
    
    public static StartingBlock CurrentBlock {   get; set;  }
    public static StartingBlock LastBlock {   get; set;  }

    private GameManager gameManager;
    public int colliding = 0;
    [SerializeField]
    private float _speed = 3.5f;
    public float slowDown = 2;
    public float hangover;
    public float _verticalMovement = -1.5f;
    public float LastBlockXSize;
    public bool hasStacked = false;
    bool canTrim = true;
    public bool perfectStack = false;
    bool dropCube = false;
    private bool pressingW;
    private bool upArrow;
    private bool pressingS;
    private bool downArrow;

    public float staminaUsage = 1.2f;


    //Enabling the game to set the variable currentBlock to this gameobject

    private void OnEnable() 
    {
        if (LastBlock == null)
        {
            //this makes it so the lastBlock becomes the gameobject 'Stack'
            LastBlock = GameObject.Find("Stack").GetComponent<StartingBlock>();
        } 
            CurrentBlock = this;

            //OnEnable starts the local scale of the current cube to these set of parameters
            transform.localScale = new Vector3(LastBlock.transform.localScale.x, transform.localScale.y, LastBlock.transform.localScale.z);
            
            
        
    }
    private void Awake() {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        
        
    }

  
    internal void Stop()
    {
            //turns the speed to zero when the method is called
            
            _speed = 0;
            //hangover is the part that hangsout and gets trimmed
            hangover = transform.position.x - LastBlock.transform.position.x;
            

                if(Mathf.Abs(hangover) >= LastBlock.transform.localScale.x || CurrentBlock.transform.localScale.x < Mathf.Abs(0.1f)){
                    //Switches to the next scene in order on build settings, which would be the Gameover scene
                    
                    GameManager.gameManager.gameOver = true;
                }
            if(Mathf.Abs(hangover) > 0.1f && colliding == 0)
            {
                GameManager.gameManager.coins++;
            }
            
            float direction = hangover > 0 ? 1f : -1f; //if hangover is greater than 0, we get a value of 1f, else we get a value of -1f
            //calculates the trimming on the currentblock only along with the direction it is at

            //the colliding check if it is 0 means it is double filtering the method from being read twice, without this, it reads twice for some reason
            if(Mathf.Abs(hangover) < 0.1f && colliding == 0){ 

                CurrentBlock.transform.position = new Vector3(LastBlock.transform.position.x, transform.position.y, transform.position.z);

                
                gameManager.ComboIncrementation();
                

                GameManager.gameManager.playStackSound = true;
                Debug.Log("Combo: "+GameManager.gameManager.combo);
                
            }
            if(Mathf.Abs(hangover) > 0.1f && colliding == 0) {
                
                SplitBlockOnX(hangover, direction);
                
                gameManager.ResetCombo();

                GameManager.gameManager.playStackSound = true;
                Debug.Log("Combo: "+GameManager.gameManager.combo);
                
                
            }

            
    }


    private void SplitBlockOnX(float hangover, float direction)
    {
        
        //with this method we can get the size of the block so that we can make it look like it is being trimmed
        //calculating the size of the falling block with the new block
        //calculating the position to position it perfectly on the stack.
        //by dividing the hangover by 2, this gives half the hangover but turns that into a transform position which switches the block to half the hangover position

        float newXSize = LastBlock.transform.localScale.x - Mathf.Abs(hangover);
        float newXPosition = LastBlock.transform.position.x + (hangover / 2f);
        float fallingBlockSize = transform.localScale.x - newXSize;
        //inputting the new variables in the game newXSize and newXPosition
        transform.localScale = new Vector3(newXSize, transform.localScale.y, transform.localScale.z);
        transform.position = new Vector3(newXPosition, transform.position.y, transform.position.z);
        
        LastBlockXSize = Mathf.Abs(newXSize);

        float blockEdge = transform.position.x + (newXSize /2f * direction); //multiplying by the direction calculates if its on the left or the right side
        float fallingBlockXPosition = blockEdge + fallingBlockSize / 2f * direction;
        
        SpawnDropBlock(fallingBlockXPosition, fallingBlockSize);
        
        
    }

    //this method is to generate the falling block when trimming to make it look as if it actually cut
    private void SpawnDropBlock(float fallingBlockXPosition, float fallingBlockSize){
        var block = GameObject.CreatePrimitive(PrimitiveType.Cube);
        //creates the block variable and give it a gameobject, which is set to createprimitive type cube
        if(Mathf.Abs(hangover) < LastBlock.transform.localScale.x || CurrentBlock.transform.localScale.x > Mathf.Abs(0.1f)){
            
            block.transform.localScale = new Vector3(fallingBlockSize, transform.localScale.y, transform.localScale.z);
            block.transform.position = new Vector3(fallingBlockXPosition, transform.position.y, transform.localPosition.z);
            block.tag = "FallingBlock"; //This is done so that we have better access to primitivetype.cube
            block.AddComponent<Rigidbody>(); // we add the rigid component to the falling block
            dropCube = true;
            Destroy(block.gameObject, 1f); //the float number gives it a running time of a second
        } 
    }

    void FixedUpdate()
    {
        CalculateMovement();
    }
    void CalculateMovement()
    {

        float _horizontalInput = Input.GetAxis("Horizontal");
        //Movement method, using Vector3 as it is a 3D Game based as 2D
        // only making horizontal input so that the player cant move up and down aswell

        Vector3 direction = new Vector3(_horizontalInput,0, 0);
        Vector3 goingDown = new Vector3(0,_verticalMovement, 0);
        transform.Translate(direction * _speed * Time.deltaTime);
        transform.Translate(goingDown * Time.deltaTime);

        //checks for W pressed the verticalmovement is still going down and that you have enough stamina, which has to be max value to use

            if(GameManager.gameManager.slowDescentActivated)
            {
                if(Input.GetKey(KeyCode.W)  && _verticalMovement < 0 && StaminaBar.instance.enoughStamina == true && !upArrow){
                    
                    //When the stamina bar is above 30 u can use the W slowing down
                    StaminaBar.instance.UseStamina(staminaUsage);
                    _speed = 1.5f;
                    StaminaBar.instance.usingStamina = true;
                    transform.Translate(Vector3.up * slowDown * Time.deltaTime);
                    pressingW = true;
                    upArrow = false;

                } else if(_verticalMovement < 0 && !upArrow){
                    pressingW = false;
                    //when running out of stamina it will stop the slowing down
                    _speed = 5f;
                    StartCoroutine(RechargingStamina());
                }
                if(Input.GetKey(KeyCode.UpArrow)  && _verticalMovement < 0 && StaminaBar.instance.enoughStamina == true && !downArrow){
                    
                    //When the stamina bar is above 30 u can use the W slowing down
                    StaminaBar.instance.UseStamina(staminaUsage);
                    _speed = 1.2f;
                    StaminaBar.instance.usingStamina = true;
                    transform.Translate(Vector3.up * slowDown * Time.deltaTime);
                    upArrow = true;
                    pressingW = false;

                } else if(_verticalMovement < 0 && !pressingW){
                    upArrow = false;
                    //when running out of stamina it will stop the slowing down
                    _speed = 5f;
                    StartCoroutine(RechargingStamina());
                }
            
            if(GameManager.gameManager.fastDescentActivated)
            {
                if(Input.GetKey(KeyCode.S) && _verticalMovement < 0 && StaminaBar.instance.enoughStamina == true && !pressingW){
                    
                    //When the stamina bar is above 30 u can use the W slowing down
                    StaminaBar.instance.UseStamina(staminaUsage * 0.8f); //stamina usage is decreased by 20% compared to slow descent
                    StaminaBar.instance.usingStamina = true;
                    transform.Translate(Vector3.down * 2 * Time.deltaTime);
                    pressingS = true;
                    downArrow = false;

                } else if(_verticalMovement < 0 && !downArrow){
                    pressingS = false;
                    StartCoroutine(RechargingStamina());
                }
                if(Input.GetKey(KeyCode.DownArrow) && _verticalMovement < 0 && StaminaBar.instance.enoughStamina == true && !upArrow){
                    
                    //When the stamina bar is above 30 u can use the W slowing down
                    StaminaBar.instance.UseStamina(staminaUsage * 0.8f);
                    StaminaBar.instance.usingStamina = true;
                    transform.Translate(Vector3.down * 2 * Time.deltaTime);
                    pressingS = true;
                    downArrow = false;

                } else if(_verticalMovement < 0 && !pressingS){
                    downArrow = false;
                    StartCoroutine(RechargingStamina());
                }
        }
            } 
            

    }
    void CheckForOutOfBounds(){
        if(CurrentBlock != null){
            if(CurrentBlock.transform.position.y < (Camera.main.transform.position.y - 5f)){
                Destroy(CurrentBlock.gameObject);
                GameManager.gameManager.playDestroySound = true;
                GameManager.gameManager.gameOver = true;
                
            }
        }
    }
            IEnumerator RechargingStamina(){

            yield return new WaitForSeconds(1);
            StaminaBar.instance.usingStamina = false;
        }
    void Start() {
        GameManager.gameManager.comboMaxGrowth += transform.localScale.x;
    }
    // Update is called once per frame
    void Update()
    {
        CheckForOutOfBounds();
        
        IsGameOver();
    }
    void IsGameOver(){
        if(GameManager.gameManager.gameOver && CurrentBlock != null){
            GameManager.gameManager.playDestroySound = true;
            Destroy(CurrentBlock.gameObject);
        }
    }

    //This method is calling the Stop() method when the startingblock collides with the stack
    //onTrigger the Stop() method will be called and the block will be trimmed, aswell as the verticalmovement will be set to 0

    private void OnTriggerEnter2D(Collider2D other) {
        
        //calls upon the currentBlock and calls the stop method onto it
        if(other.gameObject.tag == "WallRight" || other.gameObject.tag == "WallLeft"){

            return; //Do nothing
            
        }
        if(other.gameObject.tag == "Obstacle"){
            
            return; //Do nothing

        }
        if(other.gameObject.tag == "Floor"){
            GameManager.gameManager.gameOver = true;
        }
        if(other.gameObject.tag == "Spike"){
            GameManager.gameManager.playDestroySound = true;
            GameManager.gameManager.collidedWithObstacle = true;
            GameManager.gameManager.ResetCombo();
            if(colliding == 0)
            {
                gameManager.ReduceCoins(); // Without the if statement, reads the code twice
            }
            colliding++;
            StartCoroutine(Reset());
            
            

        }
        if(other.gameObject.tag == "Stack"){
            //this is done so that it checks for a single collision, and doesnt let more happen
            if(colliding == 0) {
            

            CurrentBlock.Stop();
            _verticalMovement = 0;
            
            
            //makes the currentblock into the lastblock after it is placed so that we can switch between the blocks
            //gives the block the tag 'Stack' after it is placed
            gameObject.tag = "Stack";
            LastBlock = CurrentBlock;
            gameManager.ComboLifeSystem();
            gameManager.DifficultyProgression();
            //sets the hasStacked boolean to true
            hasStacked = true;
            colliding++;
            StartCoroutine(Reset());
            //calls the coroutine to reset the colliding integer to zero
            }
        }
    }
    //Resets the colliding integer to zero everytime it is done from the code, this is done so that every time there is a collision-
    //-it waits for the end of frame.
    IEnumerator Reset() {
    
    yield return new WaitForEndOfFrame();
    colliding = 0;
    }
}