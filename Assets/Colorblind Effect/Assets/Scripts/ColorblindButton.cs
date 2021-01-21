using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorblindButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }    
    public void NormalColorMode(){
        Wilberforce.Colorblind.colorBlind.Type = 0;
    }
    public void ProtanopiaMode(){
        Wilberforce.Colorblind.colorBlind.Type = 1;
    }
    public void DeuteranopiaMode(){
        Wilberforce.Colorblind.colorBlind.Type = 2;
    }
    public void TritanopiaMode(){
        Wilberforce.Colorblind.colorBlind.Type = 3;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
