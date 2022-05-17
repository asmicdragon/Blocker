using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPBarMovement : MonoBehaviour
{
     //Declare RectTransform in script
    RectTransform xpBar;
    Vector3 newPos = new Vector3(-20, -30, 0);
     //Reference value used for the Smoothdamp method
    private Vector3 barVelocity = Vector3.zero;
     //Smooth time
    private float smoothTime = 0.5f;
    bool gameOver;
    [SerializeField]
    private bool animationDone = false;

    bool barIsFull;
    bool barIsReset;

    void Start()
    {
         //Get the RectTransform component
        xpBar = GetComponent<RectTransform>();
        barIsFull = false;
        barIsReset = false;
        
    }
    
    void Update()
    {
        gameOver = GameManager.gameManager.gameOver;
        PositionChanging();
    }
    void PositionChanging(){
        if(gameOver == true){

            StartCoroutine(WaitForAnimation(1f));
            //Update the localPosition towards the newPos
            if(animationDone == true){
                xpBar.localPosition = Vector3.Lerp(xpBar.localPosition, newPos, Time.unscaledDeltaTime * 3.5f);
                StartCoroutine(WaitForAnimationForXP(2f));
                
                ProgressBar();


            }
            
        }
    }
public void ProgressBar()
    {
        if(XPBarSlider.instance.xpBarSlider.value < XPBarSlider.instance.progress && XPBarSlider.instance.xpBarSlider.value != XPBarSlider.instance.xpBarSlider.maxValue && !barIsFull)
        {
            XPBarSlider.instance.xpBarSlider.value += XPBarSlider.instance.progress * Time.unscaledDeltaTime;
            barIsReset = false;
            
        }
        if(XPBarSlider.instance.xpBarSlider.value >= XPBarSlider.instance.xpBarSlider.maxValue && !barIsReset)
        {
            barIsFull = true;
            XPBarSlider.instance.xpBarSlider.value = 0;
            GameManager.gameManager.currentLevel++;
            LevelUpReward.instance.leveledUP = true;
            GameManager.gameManager.SaveLevelUpReward();
            GameManager.gameManager.coins += GameManager.gameManager.levelUPCoins;
            GameManager.gameManager.SaveCoins();

            LevelUpReward.instance.OnLevelUp();

            XPBarSlider.instance.progress -= GameManager.gameManager.targetXP;
            GameManager.gameManager.SaveLevel();
            barIsFull = false;
            barIsReset = true;
            //The booleans are used to switch between if statements, if one is running the other cannot at the same time
            //This ensures everything runs in order because of spaghetti
        }
        if(barIsReset)
        {
        if(XPBarSlider.instance.xpBarSlider.value < GameManager.gameManager.currentXP)
        {
            XPBarSlider.instance.xpBarSlider.value += GameManager.gameManager.currentXP * Time.unscaledDeltaTime;
        }
    }
}
    IEnumerator WaitForAnimationForXP(float animationTime){
        //uses unscaled time since its on paused timescale (0)
        yield return new WaitForSecondsRealtime(animationTime);
            if(!GameManager.gameManager.isXPAdded)
            {
                GameManager.gameManager.currentXP += GameManager.gameManager.xpThisRound;
                GameManager.gameManager.SaveCurrentXP();
                GameManager.gameManager.isXPAdded = true;
                
            }

        GameManager.gameManager.LevelUp();
        
        
    }
        IEnumerator WaitForAnimation(float animationTime){
        //uses unscaled time since its on paused timescale (0)
        yield return new WaitForSecondsRealtime(animationTime);

        animationDone = true;
        
    }

}