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
    public GameObject Pause;
    public Animator anim;
    bool animationDone = false;
    
    private void Start() {
        
        Pause = GameObject.Find("PauseButton");
        if(MenuButton != null || RestartButton != null){
            StartCoroutine(SetActiveGameObjects(false, 0));
        }
    }
    private void Update() {
        FadeIn();
    }

    void FadeIn(){
        if(GameManager.gameManager.gameOver == true){

            StartCoroutine(WaitForText(1.2f));
            Destroy(Pause);

                if(animationDone == true){
                    anim.SetTrigger("Start");
                    
                }
                StartCoroutine(SetActiveGameObjects(true, 1));
                FindObjectOfType<BonusXPText>().BonusXP();
        }
        
    }
    public void PauseButton(){
        if(Time.timeScale == 1){
            PauseGame();
        } else {
            if(Time.timeScale == 0){
                ResumeGame();
            }
        }
    }
    IEnumerator WaitForText(float seconds){
        //using realtime for unscaled time because the game is paused on gameover
        yield return new WaitForSecondsRealtime(seconds);
        animationDone = true;
    }
    IEnumerator SetActiveGameObjects(bool isActive, float seconds){
        yield return new WaitForSecondsRealtime(seconds);
        MenuButton.SetActive(isActive);
        RestartButton.SetActive(isActive);
    }
    public void RestartGame(){
        
        SceneManager.LoadScene("Game");
        
    }
    public void ResumeGame(){
        StartCoroutine(WaitForRestart());
    }
    public void PauseGame(){
        Time.timeScale = 0;
    }
    public void BacktoMenu(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    IEnumerator WaitForRestart(){
        yield return new WaitForEndOfFrame();
        Time.timeScale = 1;

    }
}