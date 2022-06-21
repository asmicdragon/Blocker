using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EfficiencyBarsController : MonoBehaviour
{
    public Slider gameplaySlider, offlineSlider;
    public TMP_Text gameplayPercentText, offlinePercentText;
    public int gameplayPercent, offlinePercent;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(this.gameObject.activeInHierarchy){
            UpdateText();
            UpdateSlider();
            gameplayPercent = PlayerPrefs.GetInt("gameplayPercent", 100);
            offlinePercent = PlayerPrefs.GetInt("offlinePercent", 0);
        }
    }

    void UpdateText()
    {
        //Use brackets to put the whole equation into an int so that it rounds smaller units

        gameplayPercentText.text = gameplayPercent.ToString() + "%";
        offlinePercentText.text = offlinePercent.ToString() + "%";
    }
    void UpdateSlider()
    {
        gameplaySlider.value = gameplayPercent;
        offlineSlider.value = offlinePercent;
    }
}
