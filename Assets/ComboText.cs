using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ComboText : MonoBehaviour
{
    GameObject  lifeComboOBJ, findTreasureComboOBJ, growthComboOBJ;
    TMP_Text lifeUpgradeText, treasureUpgradeText, growthUpgradeText;
    // Start is called before the first frame update
    void Start()
    {
        lifeComboOBJ = GameObject.Find("LifeComboText1");
        findTreasureComboOBJ = GameObject.Find("CoinComboText1");
        growthComboOBJ = GameObject.Find("GrowthComboText1");
        lifeUpgradeText = GetComponent<TMP_Text>();
        treasureUpgradeText = GetComponent<TMP_Text>();
        growthUpgradeText = GetComponent<TMP_Text>();
        
    }

    // Update is called once per frame
    void Update()
    {
        SetText();
    }
    void SetText()
    {
        if(this.gameObject == lifeComboOBJ ){
            lifeUpgradeText.text = ""+ShopManager.instance.lifeUpgrade.ToString();
        }
        if(this.gameObject == findTreasureComboOBJ){
            treasureUpgradeText.text = ""+ShopManager.instance.treasureUpgrade.ToString();;
        }
        if(this.gameObject == growthComboOBJ){
            growthUpgradeText.text = ""+ShopManager.instance.growthUpgrade.ToString();;
        }
    }
}
