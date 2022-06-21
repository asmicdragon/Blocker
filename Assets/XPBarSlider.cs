using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XPBarSlider : MonoBehaviour
{
    public static XPBarSlider instance {get; set;}

    [SerializeField]
    public Slider xpBarSlider;

    public int xpBarMaxValue, xpBarValue, currentXP, targetXP, xpThisRound, progress, newProgress;

    // Start is called before the first frame update
    
    void Start()
    {
        instance = this;

        // xpBarMaxValue = Mathf.FloorToInt(xpBarSlider.maxValue);
        // xpBarSlider.maxValue = xpBarMaxValue;

        // xpBarValue = Mathf.FloorToInt(xpBarSlider.value);
        // xpBarSlider.value = xpBarValue;
            


            
            
    }


    // Update is called once per frame
    void Update()
    {
            if(this.gameObject != null) {
                xpBarSlider.maxValue = PlayerPrefs.GetInt("targetxp", 0);
                xpBarSlider.value = PlayerPrefs.GetInt("currentxp", 0);
            }
        currentXP = PlayerPrefs.GetInt("currentxp", 0);
        targetXP = PlayerPrefs.GetInt("targetxp", 0);
    if(GameObject.Find("GameManager") != null)
    {
        if(!GameManager.gameManager.gameOver)
        {
            xpBarSlider.value = currentXP;
        } else {
            return;
        }
    }

        if(currentXP >= targetXP)
        {
            currentXP -= targetXP; // this is to make a second levelling system in this script to follow the one in gamemanager
        }
    if(GameObject.Find("GameManager") != null)
    {
        xpThisRound = GameManager.gameManager.xpThisRound;
    }
        
    }

    
}
