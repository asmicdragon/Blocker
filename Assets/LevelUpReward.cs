using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelUpReward : MonoBehaviour
{
    public static LevelUpReward instance {get; set;}
    [SerializeField]
    GameObject levelUpReward;
    TMP_Text text;

    Animator anim;
    public bool leveledUP;

    public int rewardCoins;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        text = GetComponent<TMP_Text>();
        levelUpReward.SetActive(false);
        anim = GetComponent<Animator>();
        leveledUP = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnLevelUp()
    {
        if(levelUpReward.activeInHierarchy == false)
        {
            levelUpReward.SetActive(true);
        }
        if(levelUpReward.activeInHierarchy == true && leveledUP)
        {
            text.text = "Level UP!!";
            text.text = "+" +rewardCoins.ToString();
            anim.SetTrigger("Play");
        }
    }

}
