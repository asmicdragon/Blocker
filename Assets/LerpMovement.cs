using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpMovement : MonoBehaviour
{
    RectTransform rect;
    
    bool barIsReset;
    bool barIsFull;
    int globalCoins;
    

    public int progress;
    bool calculateProgress;
    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<RectTransform>();
        barIsReset = false;
        barIsFull = false;
        calculateProgress = false;
    
        
    }

    // Update is called once per frame
    void Update()
    {
        if(CTXPSlider.instance != null && !calculateProgress){
            progress = CTXPSlider.instance.progress;
            calculateProgress = true;
        }
        
        StartCoroutine(Wait());
        
    }
    public void ProgressBar()
    {
        if(CTXPSlider.instance.xpBarSlider.value < progress && CTXPSlider.instance.xpBarSlider.value != CTXPSlider.instance.xpBarSlider.maxValue && !barIsFull)
        {
            CTXPSlider.instance.xpBarSlider.value += progress * Time.unscaledDeltaTime;
            barIsReset = false;
            
        }
        if(CTXPSlider.instance.xpBarSlider.value >= CTXPSlider.instance.xpBarSlider.maxValue && !barIsReset)
        {
            
            barIsFull = true;
            CTXPSlider.instance.xpBarSlider.value = 0;
            ShopManager.instance.currentLevel++;
            LevelUpReward.instance.leveledUP = true;
            ShopManager.instance.SaveLevelUpReward();
            ShopManager.instance.coins += ShopManager.instance.levelUPCoins;
            
            LevelUpReward.instance.OnLevelUp();
            
            progress -= ShopManager.instance.targetXP;
            globalCoins = PlayerPrefs.GetInt("globalCoins", 0);
            PlayerPrefs.SetInt("globalCoins", ShopManager.instance.coins);
            ShopManager.instance.SaveLevel();
            barIsFull = false;
            barIsReset = true;
            //The booleans are used to switch between if statements, if one is running the other cannot at the same time
            //This ensures everything runs in order because of spaghetti
        }
        if(barIsReset)
        {
        if(CTXPSlider.instance.xpBarSlider.value < ShopManager.instance.currentXP)
        {
            CTXPSlider.instance.xpBarSlider.value += ShopManager.instance.currentXP * Time.unscaledDeltaTime;
        }
    }
}
IEnumerator Wait()
{
    yield return new WaitForSecondsRealtime(1f);
    ProgressBar();
}

}
