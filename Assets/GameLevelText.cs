using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameLevelText : MonoBehaviour
{
    public static GameLevelText gameLevelText {get; set;}
    public TextMeshProUGUI currentLevelText;
    public int currentLevel;
    // Start is called before the first frame update
    void Awake() {
    
    if(this.gameObject != null)
        {
            gameLevelText = this;
        }

    }
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        UpdateText();
        currentLevel = PlayerPrefs.GetInt("currentlevel", 0);
    }
    public void UpdateText()
    {
        currentLevelText.text = GameManager.gameManager.currentLevel.ToString();
    }

}
