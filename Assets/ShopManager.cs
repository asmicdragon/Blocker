using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ShopManager : MonoBehaviour
{
    public int coins;
    [SerializeField]
    Button button;

    GameObject coinsToXP, slowDescent, fastDescent, emptyObject;

    int haveSlowDescent, haveFastDescent;
    public bool coinsToXPSelected;
    bool slowDescentSelected;
    bool fastDescentSelected;
    // Start is called before the first frame update
    void Start()
    {
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

        }

        
    }
    public void FastDescent(){
        if(EventSystem.current.currentSelectedGameObject == fastDescent)
        {
            EventSystem.current.firstSelectedGameObject = fastDescent;

        } 
    }

    public void Buy()
    {
        button.animator.Play("Pressed");

        if(EventSystem.current.firstSelectedGameObject == coinsToXP && coins >= 10)
        {
            coins -= 10;
            PlayerPrefs.SetInt("globalCoins", coins);
            PlayerPrefs.Save();
            EventSystem.current.firstSelectedGameObject = emptyObject; //Using empty game object to change the variable to checkout of the if statement
            
        }
        if(EventSystem.current.firstSelectedGameObject == slowDescent && coins >= 30)
        {
            coins -= 30;
            PlayerPrefs.SetInt("globalCoins", coins);
            PlayerPrefs.Save();
            EventSystem.current.firstSelectedGameObject = emptyObject;
            haveSlowDescent++;
            PlayerPrefs.SetInt("slowdescent", haveSlowDescent);
            IEnumerator WaitForAnim(){button.animator.SetTrigger("Fade");yield return new WaitForSecondsRealtime(0.5f);}
            //continue here tomorrow
            StartCoroutine(WaitForAnim());
            
            GameObject.Find("SlowDescentButton").SetActive(false);

        }
        if(EventSystem.current.firstSelectedGameObject == fastDescent && coins >= 30 && haveFastDescent == 0)
        {
            coins -= 30;
            PlayerPrefs.SetInt("globalCoins", coins);
            PlayerPrefs.Save();
            EventSystem.current.firstSelectedGameObject = emptyObject;
            haveFastDescent++;
            PlayerPrefs.SetInt("fastdescent", haveFastDescent);
        }
    }

}
