using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverTextFade : MonoBehaviour
{
    public TMP_Text restart, backToMenu;
    public Button resButton, menuButton;
    public GameObject restartTextOBJ, backToMenuTextOBJ, restartButtonOBJ, backToMenuButtonOBJ;
    private bool savedCoins;

    // Start is called before the first frame update
    void Start()
    {
        
        if(this.gameObject != null){
        restartTextOBJ = GameObject.Find("RestartText");
        backToMenuTextOBJ = GameObject.Find("BackToMenuText");
        restartButtonOBJ = GameObject.Find("RestartButton");
        backToMenuButtonOBJ = GameObject.Find("BackToMenuButton");
        restart = restartTextOBJ.GetComponent<TMP_Text>();
        backToMenu = backToMenuTextOBJ.GetComponent<TMP_Text>();
        resButton = restartButtonOBJ.GetComponent<Button>();
        menuButton = backToMenuButtonOBJ.GetComponent<Button>();

        restart.CrossFadeAlpha(0,0,true);
        backToMenu.CrossFadeAlpha(0,0,true);
        savedCoins = true;

        restartButtonOBJ.GetComponent<Button>().enabled = false;
        backToMenuButtonOBJ.GetComponent<Button>().enabled = false;
        restartButtonOBJ.GetComponent<Image>().enabled = false;
        backToMenuButtonOBJ.GetComponent<Image>().enabled = false;
        backToMenuTextOBJ.GetComponent<TMP_Text>().enabled = false;
        restartTextOBJ.GetComponent<TMP_Text>().enabled = false;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        FadeIn();
    }
    void FadeIn()
    {
        if(GameManager.gameManager.gameOver == true){

            StartCoroutine(WaitAndFadeIn());

        }
    }
    IEnumerator WaitAndFadeIn()
    {
        if(GameManager.gameManager.xpThisRound >= GameManager.gameManager.targetXP)
        {
            yield return new WaitForSecondsRealtime(6f);
            restartButtonOBJ.GetComponent<Button>().enabled = true;
            backToMenuButtonOBJ.GetComponent<Button>().enabled = true;
            backToMenuTextOBJ.GetComponent<TMP_Text>().enabled = true;
            restartTextOBJ.GetComponent<TMP_Text>().enabled = true;
            restartButtonOBJ.GetComponent<Image>().enabled = true;
            backToMenuButtonOBJ.GetComponent<Image>().enabled = true;
            if(savedCoins){
            GameManager.gameManager.SaveCoins();
            savedCoins = false;
            }
            restart.CrossFadeAlpha(1,2,true);
            backToMenu.CrossFadeAlpha(1,2,true);
            
        } else {
            yield return new WaitForSecondsRealtime(5f);
            restartButtonOBJ.GetComponent<Button>().enabled = true;
            backToMenuButtonOBJ.GetComponent<Button>().enabled = true;
            backToMenuTextOBJ.GetComponent<TMP_Text>().enabled = true;
            restartTextOBJ.GetComponent<TMP_Text>().enabled = true;
            restartButtonOBJ.GetComponent<Image>().enabled = true;
            backToMenuButtonOBJ.GetComponent<Image>().enabled = true;
            restart.CrossFadeAlpha(1,2,true);
            backToMenu.CrossFadeAlpha(1,2,true);
            if(savedCoins){
                GameManager.gameManager.SaveCoins();
                savedCoins = false;
            }
        }
    }
}
