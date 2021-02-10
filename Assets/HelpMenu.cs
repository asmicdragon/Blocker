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
    // Start is called before the first frame update
    void Start()
    {
        
        highScore = PlayerPrefs.GetInt(highScoreKey, 0);

        HelpMenuOBJ = this.gameObject;

        CheckNewPlayer();

        if(HelpMenuOBJ == null){
            Debug.LogError("Help Menu object is null");
        }
        
        if(anim == null){
            Debug.LogError("FadeOut Animator is null");
        }

        
    }
    void CheckNewPlayer(){
        if(highScore == 0){
            
            newPlayer = true;
            
        } else {
            newPlayer = false;
            
            Time.timeScale = 1;
        }
    }
    void CreateHelpMenu(){
        if(newPlayer){
            HelpMenuOBJ.SetActive(true);
            Time.timeScale = 0;
        } else {
            HelpMenuOBJ.SetActive(false);
            Time.timeScale = 1;
        }
    }
    public void ContinueButton(){
        anim.SetTrigger("Start");
        StartCoroutine(WaitForAnimation());
    }
    IEnumerator WaitForAnimation(){
        
        yield return new WaitForSecondsRealtime(1.1f);
        newPlayer = false;
        HelpMenuOBJ.SetActive(false);
    }
    public void BackToMenu(){
        SceneManager.LoadScene("MainMenu");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.T)){
            anim.SetTrigger("Start");
        }
        CreateHelpMenu();
    }
}
