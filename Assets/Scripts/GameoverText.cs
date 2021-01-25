using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Added the using UnityEngine.UI; to use the UI scripts
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameoverText : MonoBehaviour
{
    public GameObject MenuButton;
    public GameObject RestartButton;
    public Animator anim;
    bool animationDone = false;
    
    private void Start() {
        

        if(MenuButton != null && RestartButton != null){
            StartCoroutine(SetActiveGameObjects(false));
        }
    }
    private void Update() {
        FadeIn();
    }

    void FadeIn(){
        if(GameManager.gameManager.gameOver == true){
            StartCoroutine(WaitForText(1.5f));
            RestartButton.SetActive(true);
            MenuButton.SetActive(true);
            if(animationDone == true){
                anim.SetTrigger("Start");
            }
        }
        
    }
    IEnumerator WaitForText(float seconds){

        yield return new WaitForSecondsRealtime(seconds);
        animationDone = true;
    }
    IEnumerator SetActiveGameObjects(bool isActive){
        yield return new WaitForEndOfFrame();
        MenuButton.SetActive(isActive);
        RestartButton.SetActive(isActive);
    }
    public void RestartGame(){
        Time.timeScale = 1;
        SceneManager.LoadScene("Game");
        
    }
    public void BacktoMenu(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}