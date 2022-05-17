using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ShopManager : MonoBehaviour
{
    public static ShopManager instance {get; set;}
    public int coins;
    [SerializeField]
    Button button;

    public GameObject coinsToXP, slowDescent, fastDescent, emptyObject;

    public int haveSlowDescent, haveFastDescent;
    public bool coinsToXPSelected;
    public bool slowDescentSelected;
    public bool fastDescentSelected;
    public bool fadeOut;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        coins = PlayerPrefs.GetInt("globalCoins", 0);
        button = GetComponent<Button>();

        coinsToXPSelected = false;
        slowDescentSelected = false;
        fastDescentSelected = false;
        coinsToXP = GameObject.Find("CoinsToXPButton");
        slowDescent = GameObject.Find("SlowDescentButton");
        fastDescent = GameObject.Find("FastDescentButton");
        haveSlowDescent = PlayerPrefs.GetInt("slowdescent", 0);
        haveFastDescent = PlayerPrefs.GetInt("fastdescent", 0);
        fadeOut = true;

        CheckIfBought();

    }
    void CheckIfBought()
    {
        if(haveSlowDescent == 1 && slowDescent != null)
        {
            slowDescent.SetActive(false);

        }
        if(haveFastDescent == 1 && fastDescent != null)
        {
            fastDescent.SetActive(false);

        }
    }

    // Update is called once per frame
    void Update()
    {
        CoinsToXP();
        SlowDescent();
        FastDescent();
        
    }
    public void CoinsToXP()
    {
        if(EventSystem.current.currentSelectedGameObject == coinsToXP)
        {
            EventSystem.current.firstSelectedGameObject = coinsToXP;

        }

        
    }
    public void SlowDescent(){
        if(EventSystem.current.currentSelectedGameObject == slowDescent)
        {
            EventSystem.current.firstSelectedGameObject = slowDescent;
            slowDescentSelected = true;

        }

        
    }
    public void FastDescent(){
        if(EventSystem.current.currentSelectedGameObject == fastDescent)
        {
            EventSystem.current.firstSelectedGameObject = fastDescent;
            fastDescentSelected = true;

        }

    }
    public void Buy()
    {
        button.animator.Play("Pressed");

        int slowDescentCost = 10000;
        int fastDescentCost = 10000;

        if(EventSystem.current.firstSelectedGameObject == coinsToXP && coins >= 10)
        {
            coins -= 10;
            PlayerPrefs.SetInt("globalCoins", coins);
            PlayerPrefs.Save();
            EventSystem.current.firstSelectedGameObject = emptyObject; //Using empty game object to change the variable to checkout of the if statement
            
        }
        
        if(EventSystem.current.firstSelectedGameObject == slowDescent && coins >= slowDescentCost && haveSlowDescent == 0)
        {
            coins -= slowDescentCost;
            PlayerPrefs.SetInt("globalCoins", coins);
            EventSystem.current.firstSelectedGameObject = emptyObject;
            haveSlowDescent++;
            PlayerPrefs.SetInt("slowdescent", haveSlowDescent);
            PlayerPrefs.Save();

            FadeOutText.instance.SlowFadeText();
            FadeOutUI.instance.SlowFadeUI();


            
            IEnumerator WaitForAnim(float seconds){yield return new WaitForSecondsRealtime(seconds); slowDescent.SetActive(false); }
            
            StartCoroutine(WaitForAnim(1f));



        }
        if(EventSystem.current.firstSelectedGameObject == fastDescent && coins >= fastDescentCost && haveFastDescent == 0)
        {
            coins -= fastDescentCost;
            PlayerPrefs.SetInt("globalCoins", coins);
            EventSystem.current.firstSelectedGameObject = emptyObject;
            haveFastDescent++;
            PlayerPrefs.SetInt("fastdescent", haveFastDescent);
            PlayerPrefs.Save();
            FadeOutText.instance.FastFadeText();
            FadeOutUI.instance.FastFadeUI();
            
            
            IEnumerator WaitForAnim(float seconds){yield return new WaitForSecondsRealtime(seconds); fastDescent.SetActive(false); }
            
            StartCoroutine(WaitForAnim(1f));
            fadeOut = true;
            fastDescentSelected = false;
            
        }
    }

}
