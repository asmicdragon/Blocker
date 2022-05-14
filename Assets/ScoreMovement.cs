using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreMovement : MonoBehaviour
{
     //Declare RectTransform in script
    RectTransform scoreText;
    Vector3 newPos = new Vector3(0, 0, 0);
     //Reference value used for the Smoothdamp method
    private Vector3 scoreVelocity = Vector3.zero;
     //Smooth time
    private float smoothTime = 0.5f;
    bool gameOver;
    [SerializeField]
    private bool animationDone = false;

    void Start()
    {
         //Get the RectTransform component
        scoreText = GetComponent<RectTransform>();
        
    }
    
    void Update()
    {
        gameOver = GameManager.gameManager.gameOver;
        PositionChanging();
    }
    void PositionChanging(){
        if(gameOver == true){

            StartCoroutine(WaitForAnimation(1));
            if(!GameManager.gameManager.isXPAdded)
            {
                GameManager.gameManager.SaveCurrentXP();
                GameManager.gameManager.isXPAdded = true;
            }
            //Update the localPosition towards the newPos
            if(animationDone == true){
                scoreText.localPosition = Vector3.Lerp(scoreText.localPosition, newPos, Time.unscaledDeltaTime * 2f);
            }
        }
    }
    IEnumerator WaitForAnimation(int animationTime){
        //uses unscaled time since its on paused timescale (0)
        yield return new WaitForSecondsRealtime(animationTime);

        animationDone = true;
        
    }

}
