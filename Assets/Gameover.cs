using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameover : MonoBehaviour
{

    public Animator anim;
private void Start() {
    anim.enabled = false;
}
    // Update is called once per frame
    void Update()
    {
        //if the gameover screen is set to true, finds all cubes with the fallingblock tag and destroys them
        if(GameManager.gameManager.gameOver == true){
            
            if(GameObject.FindWithTag("FallingBlock") != null){
                Destroy(GameObject.FindWithTag("FallingBlock"));
            }
            //if the animator is not null pauses the game and plays the animation
            if(anim != null){
            
            GameManager.gameManager.PauseGame();
            anim.enabled = true;

            //If somehow the cube from the trimming still spawns, this make sure its deleted in the gameover screen
            if(GameObject.Find("Cube") != null){
                var block = GameObject.Find("Cube");
                Destroy(block.gameObject);
            }
            
            }   
        }
    }
}
