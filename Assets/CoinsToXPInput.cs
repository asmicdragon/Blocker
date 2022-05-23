using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinsToXPInput : MonoBehaviour
{
    private string input;
    public int coinsFromInput;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ReadStringInput(string str)
    {
        coinsFromInput = int.Parse(str);
        Debug.Log(coinsFromInput);
        
        
        
    }


}
