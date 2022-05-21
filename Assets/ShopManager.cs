using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class ShopManager : MonoBehaviour
{
    public static ShopManager instance {get; set;}
    public int coins;
    [SerializeField]
    Button button;
    public int lifeUpgrade, treasureUpgrade, growthUpgrade;

    public GameObject coinsToXP, slowDescent, fastDescent, emptyObject;

    public int haveSlowDescent, haveFastDescent;
    public bool coinsToXPSelected;
    public bool slowDescentSelected;
    public bool fastDescentSelected;
    public bool fadeOut;
    public int treasureMax;
    public int treasureMin;
    private int lifeMaxLevel = 3;
    private int treasureMaxLevel = 6;
    private int growthMaxLevel = 4;
    [SerializeField]
    private int currentLevel;
    public float growthPercent;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        coins = PlayerPrefs.GetInt("globalCoins", 0);
        button = GetComponent<Button>();

        coinsToXPSelected = false;
        slowDescentSelected = false;
        fastDescentSelected = false;
        coinsToXP = GameObject.Find("CoinsToXPButton");
        slowDescent = GameObject.Find("SlowDescentButton");
        fastDescent = GameObject.Find("FastDescentButton");
        haveSlowDescent = PlayerPrefs.GetInt("slowdescent", 0);
        haveFastDescent = PlayerPrefs.GetInt("fastdescent", 0);
        lifeUpgrade = PlayerPrefs.GetInt("lifeupgrade", 0);
        treasureUpgrade = PlayerPrefs.GetInt("treasureupgrade", 0);
        growthUpgrade = PlayerPrefs.GetInt("growthupgrade", 0);
        currentLevel = PlayerPrefs.GetInt("currentlevel", 0);
        fadeOut = true;

        CheckIfBought();

    }
    void CheckIfBought()
    {
        if(haveSlowDescent == 1 && slowDescent != null)
        {
            slowDescent.SetActive(false);

        }
        if(haveFastDescent == 1 && fastDescent != null)
        {
            fastDescent.SetActive(false);

        }
    }
    public void GrowthBuy()
    {

        if(growthUpgrade < growthMaxLevel){
        
        switch (growthUpgrade)
        {
            default: 

                if(coins >= 50000 && currentLevel >= 25)
                {
                    growthUpgrade++;
                    coins -= 50000;
                    growthPercent = 1.01f;
                    SaveCoins();
                    FindObjectOfType<ComboText>().SetText();
                    SaveGrowthUpgrades();

                } else Debug.Log("Not enough coins or required level");

                break;

            case 1: 

            if(coins >= 100000 && currentLevel >= 30)
                {
                    growthUpgrade++;
                    coins -= 100000;
                    growthPercent = 1.02f;
                    SaveCoins();
                    SaveGrowthUpgrades();

            } else Debug.Log("Not enough coins or required level");

                break;

            case 2: 

            if(coins >= 200000 && currentLevel >= 50)
                {
                    growthUpgrade++;
                    coins -= 200000;
                    growthPercent = 1.03f;
                    SaveCoins();
                    SaveGrowthUpgrades();

            } else Debug.Log("Not enough coins or required level");

                break;

            case 3: 

            if(coins >= 500000 && currentLevel >= 80)
                {
                    growthUpgrade++;
                    coins -= 500000;
                    growthPercent = 1.04f;
                    SaveCoins();
                    SaveGrowthUpgrades();

            } else Debug.Log("Not enough coins or required level");

                break;
            }
        }
    }
    public void FindTreasureBuy()
    {

        if(treasureUpgrade < treasureMaxLevel){
        
        switch (treasureUpgrade)
        {
            default: 

                if(coins >= 10000 && currentLevel >= 5)
                {
                    treasureUpgrade++;
                    coins -= 10000;
                    treasureMin = 100;
                    treasureMax = 2000;
                    SaveCoins();
                    FindObjectOfType<ComboText>().SetText();
                    SaveTreasureUpgrades();

                } else Debug.Log("Not enough coins or required level");

                break;

            case 1: 

            if(coins >= 30000 && currentLevel >= 15)
                {
                    treasureUpgrade++;
                    coins -= 30000;
                    treasureMin = 200;
                    treasureMax = 5000;
                    SaveCoins();
                    SaveTreasureUpgrades();

            } else Debug.Log("Not enough coins or required level");

                break;

            case 2: 

            if(coins >= 100000 && currentLevel >= 25)
                {
                    treasureUpgrade++;
                    coins -= 100000;
                    treasureMin = 300;
                    treasureMax = 8000;
                    SaveCoins();
                    SaveTreasureUpgrades();

            } else Debug.Log("Not enough coins or required level");

                break;

            case 3: 

            if(coins >= 400000 && currentLevel >= 70)
                {
                    treasureUpgrade++;
                    coins -= 400000;
                    treasureMin = 800;
                    treasureMax = 15000;
                    SaveCoins();
                    SaveTreasureUpgrades();

            } else Debug.Log("Not enough coins or required level");

                break;

            case 4: 

            if(coins >= 800000 && currentLevel >= 100)
                {
                    treasureUpgrade++;
                    coins -= 800000;
                    treasureMin = 1200;
                    treasureMax = 30000;
                    SaveCoins();
                    SaveTreasureUpgrades();

            } else Debug.Log("Not enough coins or required level");

                break;

            case 5: 

            if(coins >= 1200000 && currentLevel >= 120)
                {
                    treasureUpgrade++;
                    coins -= 1200000;
                    treasureMin = 2000;
                    treasureMax = 60000;
                    SaveCoins();
                    SaveTreasureUpgrades();

            } else Debug.Log("Not enough coins or required level");

                break;
            }
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        haveSlowDescent = PlayerPrefs.GetInt("slowdescent", 0);
        haveFastDescent = PlayerPrefs.GetInt("fastdescent", 0);
        CoinsToXP();
        SlowDescent();
        FastDescent();
        
    }
    public void CoinsToXP()
    {
        if(EventSystem.current.currentSelectedGameObject == coinsToXP)
        {
            EventSystem.current.firstSelectedGameObject = coinsToXP;

        }

        
    }
    public void SlowDescent(){
        if(EventSystem.current.currentSelectedGameObject == slowDescent)
        {
            EventSystem.current.firstSelectedGameObject = slowDescent;
            slowDescentSelected = true;

        }

        
    }
    public void FastDescent(){
        if(EventSystem.current.currentSelectedGameObject == fastDescent)
        {
            EventSystem.current.firstSelectedGameObject = fastDescent;
            fastDescentSelected = true;

        }

    }
    public void LifeUpgradeBuy()
    {
        if(lifeUpgrade < lifeMaxLevel){
        
            switch (lifeUpgrade)
            {
                default: 

                    if(coins >= 10000 && currentLevel >= 5)
                    {
                        lifeUpgrade++;
                        coins -= 10000;
                        SaveCoins();
                        FindObjectOfType<ComboText>().SetText();
                        SaveLifeUpgrades();

                    } else Debug.Log("Not enough coins or required level");

                    break;
                case 1: 

                if(coins >= 40000 && currentLevel >= 10)
                    {
                        lifeUpgrade++;
                        coins -= 40000;
                        SaveCoins();
                        SaveLifeUpgrades();

                } else Debug.Log("Not enough coins or required level");

                    break;
                case 2: 

                if(coins >= 100000 && currentLevel >= 40)
                    {
                        lifeUpgrade++;
                        coins -= 100000;
                        SaveCoins();
                        SaveLifeUpgrades();

                } else Debug.Log("Not enough coins or required level");

                    break;

            }

        } else{

            Debug.LogWarning("Cannot Upgrade Any further");
            // lifeUpgrade = lifeMaxLevel;
            // SaveLifeUpgrades();
        }
    }
    
    void SaveCoins()
    {
        PlayerPrefs.SetInt("globalCoins", coins);
        PlayerPrefs.Save();
    }
    void SaveLifeUpgrades()
    {
        PlayerPrefs.SetInt("lifeupgrade", lifeUpgrade);
        PlayerPrefs.Save();
    }
    void SaveTreasureUpgrades()
    {
        PlayerPrefs.SetInt("treasureupgrade", treasureUpgrade);
        PlayerPrefs.SetInt("treasuremax", treasureMax);
        PlayerPrefs.SetInt("treasuremin", treasureMin);
        PlayerPrefs.Save();
    }
    void SaveGrowthUpgrades()
    {
        PlayerPrefs.SetFloat("growthpercent", growthPercent);
        PlayerPrefs.SetInt("growthupgrade", growthUpgrade);
        PlayerPrefs.Save();
    }

    public void Buy()
    {
        button.animator.Play("Pressed");

        int slowDescentCost = 5000;
        int fastDescentCost = 5000;

        // if(EventSystem.current.firstSelectedGameObject == coinsToXP && coins >= 10)
        // {
        //     coins -= 10;
        //     PlayerPrefs.SetInt("globalCoins", coins);
        //     PlayerPrefs.Save();
        //     EventSystem.current.firstSelectedGameObject = emptyObject; //Using empty game object to change the variable to checkout of the if statement
            
        // }
        
        if(EventSystem.current.firstSelectedGameObject == slowDescent && coins >= slowDescentCost && haveSlowDescent == 0)
        {
            coins -= slowDescentCost;
            PlayerPrefs.SetInt("globalCoins", coins);
            EventSystem.current.firstSelectedGameObject = emptyObject;
            haveSlowDescent++;
            PlayerPrefs.SetInt("slowdescent", haveSlowDescent);
            PlayerPrefs.Save();

            FadeOutText.instance.SlowFadeText();
            FadeOutUI.instance.SlowFadeUI();


            
            IEnumerator WaitForAnim(float seconds){yield return new WaitForSecondsRealtime(seconds); slowDescent.SetActive(false); }
            
            StartCoroutine(WaitForAnim(0.15f));



        }
        if(EventSystem.current.firstSelectedGameObject == fastDescent && coins >= fastDescentCost && haveFastDescent == 0)
        {
            coins -= fastDescentCost;
            PlayerPrefs.SetInt("globalCoins", coins);
            EventSystem.current.firstSelectedGameObject = emptyObject;
            haveFastDescent++;
            PlayerPrefs.SetInt("fastdescent", haveFastDescent);
            PlayerPrefs.Save();
            FadeOutText.instance.FastFadeText();
            FadeOutUI.instance.FastFadeUI();
            
            
            IEnumerator WaitForAnim(float seconds){yield return new WaitForSecondsRealtime(seconds); fastDescent.SetActive(false); }
            
            StartCoroutine(WaitForAnim(0.15f));
            fadeOut = true;
            fastDescentSelected = false;
            
        }
    }

}
