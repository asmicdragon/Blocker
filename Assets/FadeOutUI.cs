using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOutUI : MonoBehaviour
{
    public static FadeOutUI instance {get; set;}
    [SerializeField]
    Image slowDescent, fastDescent,slowDescentIcon, fastDescentIcon;
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
    //         slowDescent.CrossFadeAlpha(0, 0.1f, true);
    //     }
    //     if(!ShopManager.instance.fadeOut && ShopManager.instance.fastDescentSelected)
    //     {
    //         fastDescent.CrossFadeAlpha(0, 0.1f, true);
    //     }
    // }
    public void SlowFadeUI()
    {
        slowDescent.CrossFadeAlpha(0, speed, true);
        slowDescentIcon.CrossFadeAlpha(0, speed, true);
    }
    public void FastFadeUI()
    {
        fastDescent.CrossFadeAlpha(0, speed, true);
        fastDescentIcon.CrossFadeAlpha(0, speed, true);
    }
}
