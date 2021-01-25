using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Added the using UnityEngine.UI; to use the UI scripts
using UnityEngine.UI;

public class GameoverText : MonoBehaviour
{
    public Animator anim;
    bool GameOver;

    private void Start() {
        anim.enabled = false;
        GameOver = GameManager.gameManager.gameOver;
    }
    private void Update() {
        FadeIn();
    }

    void FadeIn(){
        if(GameOver){
            anim.enabled = true;
        }
    }
}