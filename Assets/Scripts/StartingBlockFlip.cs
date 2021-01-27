using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingBlockFlip : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public GameManager gameManager;
    public int score = 0;

    private void Awake() {
        //setting the gameobject to GameManager
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
 
    }
    // Start is called before the first frame update
    void Start()
    {
        //Setting the local variable to the game manager's score
        score  = gameManager.score;
        //Checking if the score is an even number, this will make it so that being an even number, flips the image
        if(spriteRenderer != null && score%2==0){

            spriteRenderer.flipY = true;
            

        } else {
            //flips it back
            spriteRenderer.flipY = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
