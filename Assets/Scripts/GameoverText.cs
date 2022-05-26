using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Added the using UnityEngine.UI; to use the UI scripts
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameoverText : MonoBehaviour
{

    public GameObject Pause;
    public Animator anim;
    bool animationDone = false;
    bool gameplayTimeAdded;
    int timeAdded = 0;
    private void Start() {
        gameplayTimeAdded = false;
        Pause = GameObject.Find("PauseButton");
    }
    private void Update() {
        FadeIn();
    }

    void FadeIn(){
        if(GameManager.gameManager.gameOver == true){

            StartCoroutine(WaitForText(1.2f));
            Destroy(Pause);
            
            if(!gameplayTimeAdded && timeAdded == 0)
            {
                
                GameManager.gameManager.SaveTime();
                gameplayTimeAdded = true;
                timeAdded++;
                StartCoroutine(Reset());
            }
            

                if(animationDone == true){
                    anim.SetTrigger("Start");
                    
                }
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
    IEnumerator Reset()
    {
        yield return new WaitForSecondsRealtime(0.01f);
        timeAdded = 0;
    }
}