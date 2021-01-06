using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Added the using UnityEngine.UI; to use the UI scripts
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    [SerializeField]
    Text score;
    void Start() {
    //gets the component Text and adds it to the score, which makes this variable 'Text'
    score = GetComponent<Text>();    
    }
    void Update()
    {
        //this line of code sets the score to "Score: " and adds the score from the GameManager
        score.text = "Score: " + GameManager.FindObjectOfType<GameManager>().score;
        
    }
}
