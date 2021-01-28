using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class HighscoreText : MonoBehaviour
{
    public int highScore;
    string highScoreKey = "HighScore";
    public TMP_Text highScoreText;

    void Start()
    {
        //we get the highscore that was saved into the playerprefs by the gamemanager and input it into this local int highscores
        highScore = PlayerPrefs.GetInt(highScoreKey, 0);
        highScoreText = GetComponent<TMP_Text>();
        //giving the highscoreText the highscore
        highScoreText.text = highScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
