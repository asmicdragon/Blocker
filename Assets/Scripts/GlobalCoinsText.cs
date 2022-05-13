using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GlobalCoinsText : MonoBehaviour
{
    TMP_Text globalCoins;
    // Start is called before the first frame update
    void Start()
    {
        globalCoins = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        globalCoins.text = "" + PlayerPrefs.GetInt("globalCoins", 0);

    }
}
