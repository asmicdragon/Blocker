using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplierManager : MonoBehaviour
{
    public int gameplayPercent, offlinePercent;
    public int gameplayTime, totalSeconds;
    private bool hasSaved;
    private bool hasSaved2;

    // Start is called before the first frame update
    void Start()
    {
        hasSaved = false;
        hasSaved2 = false;
        totalSeconds = PlayerPrefs.GetInt("totalSeconds", 0);
        gameplayTime = PlayerPrefs.GetInt("gameplayTime", 0);
        
    }

    // Update is called once per frame
    void Update()
    {
        
        gameplayPercent = PlayerPrefs.GetInt("gameplayPercent", 100);
        offlinePercent = PlayerPrefs.GetInt("offlinePercent", 0);
        

        CalculatePercent();
    }
    void CalculatePercent()
    {
        Debug.Log("current Gameplay time is: "+gameplayTime.ToString());
        if(totalSeconds > 60)
        {
            totalSeconds -= 60;
            PlayerPrefs.SetInt("totalSeconds", totalSeconds);
            gameplayTime++;
            PlayerPrefs.SetInt("gameplayTime", gameplayTime);
        } 
        if(totalSeconds < 60 && !hasSaved)
        {
            PlayerPrefs.Save();
            hasSaved = true;
        }
        if(gameplayTime >= 5)
        {
            if(gameplayPercent <= 100 && gameplayPercent > 20)
            {
                PlayerPrefs.SetInt("gameplayPercent", gameplayPercent - 10);
                gameplayTime -= 5;
                PlayerPrefs.SetInt("gameplayTime", gameplayTime);
            }
            if(offlinePercent >= 0 && offlinePercent < 100)
            {
                PlayerPrefs.SetInt("offlinePercent", offlinePercent + 25);
            }
            
            if(gameplayTime < 5 && !hasSaved2)
            {
                PlayerPrefs.Save();
                hasSaved2 = true;
            }
            
        }
    }

}
