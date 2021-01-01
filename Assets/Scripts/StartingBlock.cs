﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartingBlock : MonoBehaviour
{
    //currentblock variable to declare the block so that we can have 2 different blocks, the one current and the one placed.
    
    public static StartingBlock CurrentBlock {   get; set;  }
    public static StartingBlock LastBlock {   get; set;  }

    public static Rigidbody rigidBlock;
    public static Rigidbody2D rigidBlock2D;
    [SerializeField]
    public float _speed = 3.5f;
    public bool spaceKeyPressed;
    public bool hasStacked = false;
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

            //OnEnable starts the local scale of the current cube to these set of parameters
            transform.localScale = new Vector3(LastBlock.transform.localScale.x, transform.localScale.y, LastBlock.transform.localScale.z);

        
        
    }

    //the below comments are done to add a tag to the method Stop()

    /// <summary>
    /// If this method is called, the block is trimmed based on their hangover
    /// </summary>
    /// <param name="Stop">Parameter value to pass.</param>
    /// <returns>Returns the hangover value and trims the block it is called upon</returns>
    internal void Stop()
    {
            //turns the speed to zero when the method is called
            _speed = 0;
            //hangover is the part that hangsout and gets trimmed
            float hangover = transform.position.x - LastBlock.transform.position.x;
            
            if(Mathf.Abs(hangover) >= LastBlock.transform.localScale.x){

                LastBlock = null;
                CurrentBlock = null;
                SceneManager.LoadScene(0);
            }
            
            float direction = hangover > 0 ? 1f : -1f; //if hangover is greater than 0, we get a value of 1f, else we get a value of -1f
            //calculates the trimming on the currentblock only along with the direction it is at
            CurrentBlock.SplitBlockOnX(hangover, direction);
            Debug.Log("hangover: " + hangover);
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
        Debug.Log(hangover);
        
        float blockEdge = transform.position.x + (newXSize /2f * direction); //multiplying by the direction calculates if its on the left or the right side
        float fallingBlockXPosition = blockEdge + fallingBlockSize / 2f * direction;
        
        SpawnDropBlock(fallingBlockXPosition, fallingBlockSize);
    }
    //this method is to generate the falling block when trimming to make it look as if it actually cut
    private void SpawnDropBlock(float fallingBlockXPosition, float fallingBlockSize){

        //creates the block variable and give it a gameobject, which is set to createprimitive type cube
        var block = GameObject.CreatePrimitive(PrimitiveType.Cube);

        block.transform.localScale = new Vector3(fallingBlockSize, transform.localScale.y, transform.localScale.z);
        block.transform.position = new Vector3(fallingBlockXPosition, transform.position.y, transform.localPosition.z);

        block.AddComponent<Rigidbody>(); // we add the rigid component to the falling block
        Destroy(block.gameObject, 1f); //the float number gives it a running time of a second
    }

    // Start is called before the first frame update

    void CalculateMovement()
    {

        float _horizontalInput = Input.GetAxis("Horizontal");
        //Movement method, using Vector2 as it is a 2D Game
        // only making horizontal input so that the player cant move up and down aswell

        Vector3 direction = new Vector3(_horizontalInput,0, 0);
        Vector3 goingDown = new Vector3(0,_verticalMovement, 0);
        transform.Translate(direction * _speed * Time.deltaTime);
        transform.Translate(goingDown * Time.deltaTime);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x,-4f, 4f),transform.position.y, transform.position.z);
        
        
    }
    void Start() {
    
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Jump")){
            
            spaceKeyPressed = true;
            
        } /*else {

            spaceKeyPressed = false;
        }*/
        CalculateMovement();
    }
    //This method is calling the Stop() method when the startingblock collides with the stack
    //onTrigger the Stop() method will be called and the block will be trimmed, aswell as the verticalmovement will be set to 0
    private void OnTriggerEnter2D(Collider2D other) {
        //calls upon the currentBlock and calls the stop method onto it
        if(spaceKeyPressed == false){

            CurrentBlock.Stop();
            _verticalMovement = 0;
            //makes the currentblock into the lastblock after it is placed so that we can switch between the blocks
            CurrentBlock = LastBlock;
            Debug.Log("Collided with " + LastBlock.name);
            //sets the hasStacked boolean to true
            hasStacked = true;
        } else {

            _verticalMovement = 0;
            CurrentBlock = LastBlock;
            hasStacked = true;
        }

    }
}


