using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CoinsToXP : MonoBehaviour
{
    public static CoinsToXP instance {get; set;}
    public TMP_Text coinsText, xpText;

    public GameObject coinsTextOBJ, xpTextOBJ, uiOBJ;

    public int convertedXP, globalCoins;
    void Awake()
    {
        instance = this;
        uiOBJ = GameObject.Find("PopUpUI");
        coinsTextOBJ = GameObject.Find("CoinsText2");
        xpTextOBJ = GameObject.Find("XPText");
        uiOBJ = GameObject.Find("PopUpUI");
        coinsText = coinsTextOBJ.GetComponent<TMP_Text>();
        xpText = xpTextOBJ.GetComponent<TMP_Text>();
        }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
            ConvertCoinsToXP();
            ShowText();
            globalCoins = PlayerPrefs.GetInt("globalCoins", 0);
        
    }
    void ShowText()
    {
        if(coinsTextOBJ || xpText != null){
            
            coinsText.text = CoinsToXPInput.FindObjectOfType<CoinsToXPInput>().coinsFromInput.ToString();

            xpText.text = convertedXP.ToString();
        }
        if(FindObjectOfType<CoinsToXPInput>().coinsFromInput > globalCoins)
        {
            FindObjectOfType<CoinsToXPInput>().coinsFromInput = globalCoins;
            ShopManager.instance.MaxButton();
            

        }

    }
    void ConvertCoinsToXP()
    {
        convertedXP = Mathf.RoundToInt(CoinsToXPInput.FindObjectOfType<CoinsToXPInput>().coinsFromInput * 0.35f);//35% of coins
    }

}
