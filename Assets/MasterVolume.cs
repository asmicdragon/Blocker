using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MasterVolume : MonoBehaviour
{
    
    string volumeKey = "Volume";
    public Slider masterSlider;
    [Range(0.0f, 1.0f)]
    [SerializeField]
    private float masterVolume = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        masterSlider.maxValue = masterVolume;

        masterVolume = PlayerPrefs.GetFloat(volumeKey, 1.0f);
        AudioListener.volume = PlayerPrefs.GetFloat(volumeKey, 1.0f);
        masterSlider.value = PlayerPrefs.GetFloat(volumeKey, 1.0f);


    }

    // Update is called once per frame
    void Update()
    {
        SaveVolume();
        
    }
    public void SaveVolume(){
        masterVolume = masterSlider.value;
        PlayerPrefs.SetFloat(volumeKey, masterVolume);
        PlayerPrefs.Save();
    }
}
