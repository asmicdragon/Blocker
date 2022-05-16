using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class XPBarText : MonoBehaviour
{
    public static XPBarText xpBarText {get; set;}
    public TextMeshProUGUI currentLevelText;
    public int currentLevel;
    // Start is called before the first frame update
    void Awake() {
    
    if(this.gameObject != null)
        {
            xpBarText = this;
        }

    }
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        UpdateText();
        
    }
    public void UpdateText()
    {
        currentLevel = PlayerPrefs.GetInt("currentlevel", 1);
        currentLevelText.text = currentLevel.ToString();
    }

}
