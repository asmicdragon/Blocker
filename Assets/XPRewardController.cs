using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class XPRewardController : MonoBehaviour
{
    TimeReward timeReward = new TimeReward();
    OfflineRewardSlider rewardSlider = new OfflineRewardSlider();
    bool barIsReset;
    bool barIsFull;
    public TMP_Text coinsTextReward, xpTextReward;

    public int progress;
    public bool calculateProgress;
    // Start is called before the first frame update
    void Start()
    {

        barIsReset = false;
        barIsFull = false;
        calculateProgress = false;
        
    }

    // Update is called once per frame
    void Update()
    {


        if(OfflineRewardSlider.instance != null)
        {
            if(!calculateProgress)
            {
                StartCoroutine(WaitAgain());
                calculateProgress = true;
            }

            StartCoroutine(Wait());
            
        }
        coinsTextReward.text = "+" + TimeReward.timeReward.rewardCoins.ToString() +" Coins";
        xpTextReward.text = "+" + TimeReward.timeReward.rewardXP.ToString() +" XP";
        
        
    }
    public void ProgressBar()
    {
        if(OfflineRewardSlider.instance.xpBarSlider.value < progress && OfflineRewardSlider.instance.xpBarSlider.value != OfflineRewardSlider.instance.xpBarSlider.maxValue && !barIsFull)
        {
            OfflineRewardSlider.instance.xpBarSlider.value += progress * Time.unscaledDeltaTime;
            barIsReset = false;
            
        }
        if(OfflineRewardSlider.instance.xpBarSlider.value >= OfflineRewardSlider.instance.xpBarSlider.maxValue && !barIsReset)
        {
            barIsFull = true;
            OfflineRewardSlider.instance.xpBarSlider.value = 0;
            ShopManager.instance.currentLevel++;
            LevelUpReward.instance.leveledUP = true;
            ShopManager.instance.SaveLevelUpReward();
            ShopManager.instance.coins += ShopManager.instance.levelUPCoins;
            ShopManager.instance.SaveCoins();

            LevelUpReward.instance.OnLevelUp();

            progress -= ShopManager.instance.targetXP;
            ShopManager.instance.SaveLevel();
            barIsFull = false;
            barIsReset = true;
            //The booleans are used to switch between if statements, if one is running the other cannot at the same time
            //This ensures everything runs in order because of spaghetti
        }
        if(barIsReset)
        {
        if(OfflineRewardSlider.instance.xpBarSlider.value < ShopManager.instance.currentXP)
        {
            OfflineRewardSlider.instance.xpBarSlider.value += ShopManager.instance.currentXP * Time.unscaledDeltaTime;
        }
    }
}
IEnumerator Wait()
{
    yield return new WaitForSecondsRealtime(1f);
    ProgressBar();
}
IEnumerator WaitAgain()
{
    yield return new WaitForSecondsRealtime(0.02f);
    progress = OfflineRewardSlider.instance.progress;
}

}
