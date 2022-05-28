using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplierManager : MonoBehaviour
{
    public int gameplayPercent, offlinePercent;
    public int gameplayTime;
    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        gameplayTime = PlayerPrefs.GetInt("gameplayTime", 0);
        gameplayPercent = PlayerPrefs.GetInt("gameplayPercent", 100);
        offlinePercent = PlayerPrefs.GetInt("offlinePercent", 0);

        CalculatePercent();
    }
    void CalculatePercent()
    {
        if(gameplayTime >= 30)
        {
            if(gameplayPercent <= 100 && gameplayPercent > 20)
            {
                PlayerPrefs.SetInt("gameplayPercent", gameplayPercent - 10);
                PlayerPrefs.SetInt("gameplayTime", gameplayTime - 30);
            }
            if(offlinePercent >= 0 && offlinePercent < 100)
            {
                PlayerPrefs.SetInt("offlinePercent", offlinePercent + 25);
            }
            
            if(gameplayTime < 30)
            {
                PlayerPrefs.Save();
            }
            
        }
    }

}
