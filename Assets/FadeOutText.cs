using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class FadeOutText : MonoBehaviour
{
    public static FadeOutText instance {get; set;}
    [SerializeField]
    
    TMP_Text fastText, slowText,fastCostText, slowCostText;
    float colorAlpha;
    float speed = 0.25f;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        // FadeOut();
    }
    // void FadeOut()
    // {
    //     if(!ShopManager.instance.fadeOut && ShopManager.instance.slowDescentSelected)
    //     {
    //         Debug.Log("fading slow");
    //         slowText.CrossFadeAlpha(0, 0.1f, true);
    //     }
    //     if(!ShopManager.instance.fadeOut && ShopManager.instance.fastDescentSelected)
    //     {
    //         Debug.Log("fading fast");
    //         fastText.CrossFadeAlpha(0, 0.1f, true);
    //     }
    // }
    public void SlowFadeText()
    {
        slowText.CrossFadeAlpha(0, speed, true);
        slowCostText.CrossFadeAlpha(0, speed, true);
    }
    public void FastFadeText()
    {
        fastText.CrossFadeAlpha(0, speed, true);
        fastCostText.CrossFadeAlpha(0, speed, true);
    }
}
