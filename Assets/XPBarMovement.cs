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

    void Start()
    {
         //Get the RectTransform component
        xpBar = GetComponent<RectTransform>();
        
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
                
            }
            
        }
    }
    IEnumerator WaitForAnimationForXP(float animationTime){
        //uses unscaled time since its on paused timescale (0)
        yield return new WaitForSecondsRealtime(animationTime);
            if(!GameManager.gameManager.isXPAdded)
            {
                GameManager.gameManager.currentXP += GameManager.gameManager.score * 100;
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