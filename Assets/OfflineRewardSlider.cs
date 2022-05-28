using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OfflineRewardSlider : MonoBehaviour
{
    public static OfflineRewardSlider instance {get; set;}

    [SerializeField]
    public Slider xpBarSlider;

    public int xpBarMaxValue, xpBarValue, currentXP, targetXP, rewardXP, progress;

    // Start is called before the first frame update
    
    void Start()
    {
        instance = this;

            if(this.gameObject != null) {
                xpBarSlider.maxValue = PlayerPrefs.GetInt("targetxp", 0);
                xpBarSlider.value = PlayerPrefs.GetInt("currentxp", 0);
            }


            rewardXP = TimeReward.timeReward.rewardXP;
            progress = Mathf.RoundToInt(xpBarSlider.value) + rewardXP;
    }


    // Update is called once per frame
    void Update()
    {

        currentXP = PlayerPrefs.GetInt("currentxp", 0);
        targetXP = PlayerPrefs.GetInt("targetxp", 0);
    if(GameObject.Find("ShopManager") != null)
    {
        if(ShopManager.instance.claimPressed)
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
