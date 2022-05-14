using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class XPManager : MonoBehaviour
{
    public TextMeshProUGUI currentLevelText;
    public int currentXP, targetXP;
    private string targetXPKey = "targetxp";
    
    public static int currentLevel;
    int leveled = 0;

    public static XPManager xpManager;
    // Start is called before the first frame update

    void Start()
    {
        xpManager = this;
        currentLevelText.text = currentLevel.ToString();
        currentLevel = PlayerPrefs.GetInt("currentlevel", 0);
        currentXP = PlayerPrefs.GetInt("currentxp", 0);
        
    }
    public void SaveCurrentLevel()
    {
        PlayerPrefs.SetInt("currentlevel", currentLevel);
        PlayerPrefs.Save();
    }


    public void LevelUp()
    {

        if(currentXP >= targetXP) // this means its over the target xp meaning, level up
        {
            currentXP = currentXP - targetXP;
            PlayerPrefs.SetInt("currentxp", currentXP);
            PlayerPrefs.Save();
            if(leveled == 0)
            {
                currentLevel++;
                leveled++;
                StartCoroutine(Reset());
            }
            PlayerPrefs.SetInt("currentlevel", currentLevel);
            PlayerPrefs.Save();
            targetXP = Mathf.FloorToInt(Mathf.Floor(((currentLevel*(currentLevel - 1))/70) * 100)) + 3000;
            currentLevelText.text = currentLevel.ToString();

        }
    }
    // Update is called once per frame
    void Update()
    {
        LevelUp();
        
    }
    IEnumerator Reset()
    {
        yield return new WaitForSecondsRealtime(0.25f);
        leveled = 0;
    }
}
