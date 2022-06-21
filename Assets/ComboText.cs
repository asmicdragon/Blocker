using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ComboText : MonoBehaviour
{
    GameObject  lifeComboOBJ, findTreasureComboOBJ, growthComboOBJ;
    TMP_Text lifeUpgradeText, treasureUpgradeText, growthUpgradeText;

    int lifeUpgrade, growthUpgrade, treasureUpgrade;
    // Start is called before the first frame update
    void Start()
    {
        lifeComboOBJ = GameObject.Find("LifeComboText1");
        findTreasureComboOBJ = GameObject.Find("CoinComboText1");
        growthComboOBJ = GameObject.Find("GrowthComboText1");
        lifeUpgradeText = lifeComboOBJ.GetComponent<TMP_Text>();
        treasureUpgradeText = findTreasureComboOBJ.GetComponent<TMP_Text>();
        growthUpgradeText = growthComboOBJ.GetComponent<TMP_Text>();
        lifeUpgrade = PlayerPrefs.GetInt("lifeupgrade", 0);
        growthUpgrade = PlayerPrefs.GetInt("growthupgrade", 0);
        treasureUpgrade = PlayerPrefs.GetInt("treasureupgrade", 0);
    }

    // Update is called once per frame
    void Update()
    {
        SetText();
    }
    public void SetText()
    {
            lifeUpgrade = PlayerPrefs.GetInt("lifeupgrade", 0);
            growthUpgrade = PlayerPrefs.GetInt("growthupgrade", 0);
            treasureUpgrade = PlayerPrefs.GetInt("treasureupgrade", 0);

            lifeUpgradeText.text = ""+lifeUpgrade.ToString();
   
            treasureUpgradeText.text = ""+treasureUpgrade.ToString();;

            growthUpgradeText.text = ""+growthUpgrade.ToString();;
            
    }
}
