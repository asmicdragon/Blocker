using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XPBarSlider : MonoBehaviour
{
    public static XPBarSlider instance {get; set;}
    public Slider xpBarSlider;
    
    private int xpBarMaxValue;

    // Start is called before the first frame update
    private void Awake() {
        
        xpBarSlider.value = PlayerPrefs.GetInt("currentxp", 0);
        xpBarSlider.maxValue = PlayerPrefs.GetInt("targetxp", 0);
        xpBarSlider.minValue = 0;
        
    }
    void Start()
    {
        instance = this;
        

    }

    // Update is called once per frame
    void Update()
    {
        IncrementProgress();
        CheckForValues();
        
    }
    void IncrementProgress()
    {
        int progress = PlayerPrefs.GetInt("currentxp", 0);
        int maxProgress = PlayerPrefs.GetInt("targetxp", 0);


            if(xpBarSlider.value < progress)
            {
                xpBarSlider.value += progress * Time.unscaledDeltaTime;
            }  
                
            
        
    }
    void CheckForValues()
    {
        xpBarSlider.maxValue = PlayerPrefs.GetInt("targetxp", 0);
    }
    
}
