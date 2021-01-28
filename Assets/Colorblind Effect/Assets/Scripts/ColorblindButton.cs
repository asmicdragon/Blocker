using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This class is made to access the colorblind script through the use of buttons.
public class ColorblindButton : MonoBehaviour
{ 
    // Start is called before the first frame update
    public int colorType = 0;
    string colorTypeKey = "ColorType";
    void Start()
    {
        colorType = PlayerPrefs.GetInt(colorTypeKey, 0);
    }    

    public void NormalColorMode(){
        Wilberforce.Colorblind.colorBlind.Type = 0;
        colorType = Wilberforce.Colorblind.colorBlind.Type;
        PlayerPrefs.SetInt(colorTypeKey, colorType);
        PlayerPrefs.Save();
    }
    public void ProtanopiaMode(){
        Wilberforce.Colorblind.colorBlind.Type = 1;
        colorType = Wilberforce.Colorblind.colorBlind.Type;
        PlayerPrefs.SetInt(colorTypeKey, colorType);
        PlayerPrefs.Save();
    }
    public void DeuteranopiaMode(){
        Wilberforce.Colorblind.colorBlind.Type = 2;
        colorType = Wilberforce.Colorblind.colorBlind.Type;
        PlayerPrefs.SetInt(colorTypeKey, colorType);
        PlayerPrefs.Save();
    }
    public void TritanopiaMode(){
        Wilberforce.Colorblind.colorBlind.Type = 3;
        colorType = Wilberforce.Colorblind.colorBlind.Type;
        PlayerPrefs.SetInt(colorTypeKey, colorType);
        PlayerPrefs.Save();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
