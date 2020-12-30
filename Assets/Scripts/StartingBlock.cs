using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingBlock : MonoBehaviour
{
    //currentblock variable to declare the block so that we can have 2 different blocks, the one current and the one placed.
    
    public static StartingBlock CurrentBlock {   get; set;  }
    public static StartingBlock LastBlock {   get; set;  }

    public static Rigidbody rigidBlock;
    public static Rigidbody2D rigidBlock2D;
    [SerializeField]
    public float _speed = 3.5f;

    public bool hasStacked = false;
    public bool canTrim = false;

    [SerializeField]
    public float _verticalMovement = -1.5f;
    //Enabling the game to set the variable currentBlock to this gameobject

    private void OnEnable() 
    {
        if (LastBlock == null)
        {
            //this makes it so the lastBlock becomes the gameobject 'Stack'
            LastBlock = GameObject.Find("Stack").GetComponent<StartingBlock>();
        }
        CurrentBlock = this;
        Debug.Log("Current Block: " + CurrentBlock.name);
        Debug.Log("Last block name: " + LastBlock.name);

        
    }
    internal void Stop()
    {
        //turns the speed to zero when the method is called
        if(canTrim == false){

            return;
            
        } else {

            _speed = 0;
            //hangover is the part that hangsout and gets trimmed
            float hangover = transform.position.x - LastBlock.transform.position.x;
            SplitBlockOnX(hangover);
            Debug.Log("hangover: " + hangover);
            hasStacked = true;
        }
        
    }
    private void SplitBlockOnX(float hangover)
    {
        /*
        //with this method we can get the size of the block so that we can make it look like it is being trimmed
        float newXSize = LastBlock.transform.localScale.x - Mathf.Abs(hangover);
        //calculating the size of the falling block with the new block
        float fallingBlockSize = transform.localScale.x - newXSize;
        //calculating the position to position it perfectly on the stack.
        //by dividing the hangover by 2, this gives half the hangover but turns that into a transform position which switches the block to half the hangover position
        float newXposition = LastBlock.transform.position.x + (hangover / 2f);
        transform.localScale = new Vector2(newXSize, transform.localScale.y);
        transform.position = new Vector2(newXposition, transform.position.y);
        Debug.Log(newXposition);
        */
        float newXSize = LastBlock.transform.localScale.x - Mathf.Abs(hangover);
        float newXPosition = LastBlock.transform.position.x + (hangover / 2f);

        //inputting the new variables in the game newXSize and newXPosition
        transform.localScale = new Vector3(newXSize, transform.localScale.y, transform.localScale.z);
        transform.position = new Vector3(newXPosition, transform.position.y, transform.position.z);
        Debug.Log(hangover);
        
    }

    // Start is called before the first frame update

    void CalculateMovement()
    {
        //Checks if canMove is true, which is always true, to be able to move.
        //When canMove is switched to false, the player will stop moving
        

        float _horizontalInput = Input.GetAxis("Horizontal");
        //Movement method, using Vector2 as it is a 2D Game
        // only making horizontal input so that the player cant move up and down aswell

        Vector3 direction = new Vector3(_horizontalInput,0, 0);
        Vector3 goingDown = new Vector3(0,_verticalMovement, 0);
        transform.Translate(direction * _speed * Time.deltaTime);
        transform.Translate(goingDown * Time.deltaTime);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x,-2.41f, 2.41f),transform.position.y, transform.position.z);
        
        
    }
    void Start() {
    
    }
    // Update is called once per frame
    void Update()
    {
        if(hasStacked == true){
            canTrim = false;
        }
        CalculateMovement();
    }
    //This method is calling the Stop() method when the startingblock collides with the stack
    //onTrigger the Stop() method will be called and the block will be trimmed, aswell as the verticalmovement will be set to 0
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.name == "Stack"){
            _verticalMovement = 0;
            CurrentBlock = LastBlock;
            canTrim = true;
            Stop();
            Debug.Log("Collided with " + LastBlock.name);
        } else {
            canTrim = true;
            _verticalMovement = 0;
            CurrentBlock = LastBlock;
            Stop();
            Debug.Log("Collided with Last Block");
        }
    }
}

