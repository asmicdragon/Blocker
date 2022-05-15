using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XPBarSlider : MonoBehaviour
{
    public static XPBarSlider instance {get; set;}

    [SerializeField]
    Slider xpBarSlider;

    public int xpBarMaxValue, xpBarValue;

    // Start is called before the first frame update
    
    void Start()
    {
        instance = this;

        // xpBarMaxValue = Mathf.FloorToInt(xpBarSlider.maxValue);
        // xpBarSlider.maxValue = xpBarMaxValue;

        // xpBarValue = Mathf.FloorToInt(xpBarSlider.value);
        // xpBarSlider.value = xpBarValue;
        
        if(GameManager.gameManager.gameObject != null) 
        {
            xpBarSlider.maxValue = PlayerPrefs.GetInt("targetxp", 0);
            xpBarSlider.value = PlayerPrefs.GetInt("currentxp", 0);
        }
    }
    public void ProgressBar()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

   

    
}
