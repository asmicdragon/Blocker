using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Added the using UnityEngine.UI; to use the UI scripts
using UnityEngine.UI;

public class CoinsText : MonoBehaviour
{
    Text coins;
    // Start is called before the first frame update
    void Start()
    {
        coins = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        coins.text = "" + GameManager.FindObjectOfType<GameManager>().coins; 
    }
}
