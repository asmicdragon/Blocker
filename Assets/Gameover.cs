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
        if(GameManager.gameManager.gameOver == true){
            if(anim != null){
            GameManager.gameManager.PauseGame();
            anim.enabled = true;
            
            }   
        }
    }
}
