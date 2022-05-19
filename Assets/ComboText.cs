using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ComboText : MonoBehaviour
{
    TMP_Text lifeUpgradeText;
    // Start is called before the first frame update
    void Start()
    {
        lifeUpgradeText = GetComponent<TMP_Text>();
        lifeUpgradeText.text = ""+ShopManager.instance.lifeUpgrade;
    }

    // Update is called once per frame
    void Update()
    {
        lifeUpgradeText.text = ""+ShopManager.instance.lifeUpgrade.ToString();
    }
}
