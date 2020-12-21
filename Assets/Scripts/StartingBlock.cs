﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingBlock : MonoBehaviour
{
    //currentblock variable to declare the block so that we can have 2 different blocks, the one current and the one placed.
    
    public static StartingBlock CurrentBlock {   get; private set;  }
    public static StartingBlock LastBlock {   get; private set;  }

    [SerializeField]
    private float _speed = 3.5f;

    [SerializeField]
    float _verticalMovement = -1.5f;
    //Enabling the game to set the variable currentBlock to this gameobject
    private void OnEnable() 
    {
        if (LastBlock == null)
            //this makes it so the lastBlock becomes the gameobject 'Stack'
            LastBlock = GameObject.Find("Stack").GetComponent<StartingBlock>();

        CurrentBlock = this;
        
    }

    internal void Stop()
    {
        //turns the speed to zero when the method is called
        _speed = 0;
        //hangover is the part that hangsout and gets trimmed
        float hangover = transform.position.x - LastBlock.transform.position.x;
        SplitBlockOnX(hangover);
        
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

        if(_speed >= 0) {
        //inputting the new variables in the game newXSize and newXPosition
        transform.localScale = new Vector2(newXSize, transform.localScale.y);
        transform.position = new Vector2(newXPosition, transform.position.y);
        Debug.Log(hangover);
        }
    }
    [SerializeField]
    Vector2 currentBlockPos = new Vector2(0.05f,4.4f);
    void SetPosition(){
        //gives the position only to this GameObject so that when we use it on other gameobjects, it doesnt transfer the position.
        currentBlockPos = this.transform.position;
    }
    // Start is called before the first frame update
    void Start()
    {
        SetPosition();
    }
    void CalculateMovement()
    {
        //Checks if canMove is true, which is always true, to be able to move.
        //When canMove is switched to false, the player will stop moving
        

        float _horizontalInput = Input.GetAxis("Horizontal");
        //Movement method, using Vector2 as it is a 2D Game
        // only making horizontal input so that the player cant move up and down aswell

        Vector2 direction = new Vector2(_horizontalInput,0);
        Vector2 goingDown = new Vector2(0,_verticalMovement);
        transform.Translate(direction * _speed * Time.deltaTime);
        transform.Translate(goingDown * Time.deltaTime);
        transform.position = new Vector2(Mathf.Clamp(transform.position.x,-7.8f, 7.8f),transform.position.y);
    }
    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
    }
    
    void OnTriggerEnter2D(Collider2D other) {

        if(other.tag == "Floor"){

        Destroy(this.gameObject);
        
        }
        
    }
}