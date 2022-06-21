using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BonusXPText : MonoBehaviour
{
    TMP_Text text;
    bool alphaIsZero;
    private int bonusXP;
    // Start is called before the first frame update
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        text = GameObject.Find("BonusXPText").GetComponent<TMP_Text>();
    }
    void Start()
    {
        
        alphaIsZero = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void BonusXP()
    {
        
        if(!alphaIsZero){
            text.CrossFadeAlpha(0,0,true);
            alphaIsZero = true;
        }
        bonusXP = (GameManager.gameManager.xpInc - 100) * GameManager.gameManager.score;
        text.text = "Bonus XP: "+ bonusXP.ToString();
        StartCoroutine(Wait());
    }
    IEnumerator Wait()
    {
        yield return new WaitForSecondsRealtime(2.5f);
        text.CrossFadeAlpha(1,1, true);
    }
}
