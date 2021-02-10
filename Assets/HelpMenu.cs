using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class HelpMenu : MonoBehaviour
{
    public bool newPlayer;
    int highScore = 0;
    string highScoreKey = "HighScore";
    public Animator anim;
    public GameObject HelpMenuOBJ;
    public GameObject BacktoMenu;
    public GameObject Continue;

    public static HelpMenu helpMenu {get; set;}
    public bool helpMenuDone = false;
    // Start is called before the first frame update
    void Start()
    {
        helpMenu = this;
        highScore = PlayerPrefs.GetInt(highScoreKey, 0);
        
            if(helpMenu != null){
                HelpMenuOBJ = GameObject.Find("NewPlayerMenu");
                BacktoMenu = GameObject.Find("BacktoMenu");
                Continue = GameObject.Find("ContinueButton");
                CheckNewPlayer();

                if(HelpMenuOBJ == null){
                    Debug.LogError("Help Menu object is null");
                }
                
                if(anim == null){
                    Debug.LogError("FadeOut Animator is null");
                }

                CreateHelpMenu();
            }
        
    }
    void CheckNewPlayer(){
        if(highScore == 0){
            
            newPlayer = true;
            
        } else {
            newPlayer = false;
            helpMenuDone = true;
            Time.timeScale = 1;
        }
    }
    void CreateHelpMenu(){
        
        if(newPlayer){
            HelpMenuOBJ.SetActive(true);
            Continue.SetActive(true);
            BacktoMenu.SetActive(true);
            Time.timeScale = 0;
        } else {
            HelpMenuOBJ.SetActive(false);
            Continue.SetActive(false);
            BacktoMenu.SetActive(false);
            Time.timeScale = 1;
        }
    }
    public void ContinueButton(){
        anim.SetTrigger("Fade");
        StartCoroutine(WaitForAnimation());
        helpMenuDone = true;
        
    }


    IEnumerator WaitForAnimation(){
        
        yield return new WaitForSecondsRealtime(1.1f);
        Time.timeScale = 1;
        HelpMenuOBJ.SetActive(false);
        Continue.SetActive(false);
        BacktoMenu.SetActive(false);
    }
    public void BackToMenu(){
        SceneManager.LoadScene("MainMenu");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
