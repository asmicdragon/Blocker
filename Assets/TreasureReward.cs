using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TreasureReward : MonoBehaviour
{
    public static TreasureReward treasureReward {get;set;}
    RectTransform rectTransform;
    TMP_Text rewardText;
    
    Animator anim;
    public bool triggerAnimation;
    int treasureUpgrade;

    public int rewardCoins;

    // Start is called before the first frame update
    void Start()
    {
        treasureReward = this;
        rectTransform = GetComponent<RectTransform>();
        rewardText = GetComponent<TMP_Text>();
        anim = GetComponent<Animator>();
        triggerAnimation = false;
        treasureUpgrade = PlayerPrefs.GetInt("treasureupgrade", 0);
        treasureReward.enabled = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(this.gameObject.activeInHierarchy == true){
            MoveText();
        }
    }
    public void RewardAnimation()
    {
        // if(treasureUpgrade != 0)
        // {
        //     if(triggerAnimation)
        //     {
        
        
        rewardText.CrossFadeAlpha(1,0.01f, true);
        rewardText.text = "+"+ rewardCoins.ToString();
        anim.SetTrigger("Play");
        StartCoroutine(Wait(5f));
        rewardText.CrossFadeAlpha(0,2f,true);
        StartCoroutine(WaitAndOff(2.5f));
        
        



        //     }
        // }
    }
    void MoveText()
    {
        
        rectTransform.position += new Vector3(0,5 * Time.unscaledDeltaTime,0);
        rectTransform.eulerAngles += new Vector3(0,0,10 * Time.unscaledDeltaTime);
    }
    IEnumerator Wait(float seconds)
    {
        yield return new WaitForSecondsRealtime(seconds);
    }
        IEnumerator WaitAndOff(float seconds)
    {
        yield return new WaitForSecondsRealtime(seconds);
        // rectTransform.anchoredPosition = new Vector3(0,0,0);
        rectTransform.position = new Vector3(Random.Range(380,500),Random.Range(70,140),0);
        rectTransform.eulerAngles = new Vector3(0,0,0);
        treasureReward.enabled = false;
    }

}
