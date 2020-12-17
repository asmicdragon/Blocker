using System;
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
    private void OnEnable() {
        
        if (LastBlock == null){
            LastBlock = GameObject.Find("Stack").GetComponent<StartingBlock>();
        }
        CurrentBlock = this;
    }

    internal void Stop()
    {
        //turns the speed to zero when the method is called
        _speed = 0;
        //hangover is the part that hangsout and gets trimmed
        float hangover = transform.position.x - LastBlock.transform.position.x;
        Debug.Log(hangover);
    }
  
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
