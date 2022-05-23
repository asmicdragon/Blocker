using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CTXPSlider : MonoBehaviour
{
    public static CTXPSlider instance {get; set;}

    [SerializeField]
    public Slider xpBarSlider;

    public int xpBarMaxValue, xpBarValue, currentXP, targetXP, convertedXP, progress;

    // Start is called before the first frame update
    
    void Start()
    {
        instance = this;

            if(this.gameObject != null) {
                xpBarSlider.maxValue = PlayerPrefs.GetInt("targetxp", 0);
                xpBarSlider.value = PlayerPrefs.GetInt("currentxp", 0);
            }
            convertedXP = FindObjectOfType<CoinsToXP>().convertedXP;
            progress = Mathf.RoundToInt(xpBarSlider.value) + convertedXP;
            
    }


    // Update is called once per frame
    void Update()
    {

        currentXP = PlayerPrefs.GetInt("currentxp", 0);
        targetXP = PlayerPrefs.GetInt("targetxp", 0);
    if(GameObject.Find("ShopManager") != null)
    {
        if(ShopManager.instance.ctxpBuy)
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

        
    }

    
}
