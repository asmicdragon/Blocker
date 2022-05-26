using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class DailyRewards : MonoBehaviour
{

        public TMP_Text Time;
        public float msToWait = 864000;
        public Button ClickButton;
        public Button[] DayArray = new Button[7];
        private ulong lastTimeClicked;
        
        public int currentDay = 1;
    
        private void Start()
        {
    	if(PlayerPrefs.HasKey("LastTimeClicked")){
			lastTimeClicked = ulong.Parse(PlayerPrefs.GetString("LastTimeClicked"));
            Debug.Log("Player has: "+ lastTimeClicked.ToString());
		}else{
			lastTimeClicked = (ulong)DateTime.Now.Ticks;
			PlayerPrefs.SetString("LastTimeClicked", lastTimeClicked.ToString());
            Debug.Log("Last time clicked wasn't set, it is now: "+ lastTimeClicked.ToString());
		}
 
		if (!Ready())
			ClickButton.interactable = false;
        }
    
        private void Update()
        {
            
            if (!ClickButton.IsInteractable())
            {
                if (Ready())
                {
                    ClickButton.interactable = true;
                    Time.text = "Ready!";
                    return;
                }
                ulong diff = ((ulong)DateTime.Now.Ticks - lastTimeClicked);
                ulong m = diff / TimeSpan.TicksPerMillisecond;
                float secondsLeft = (float)(msToWait - m) / 1000.0f;
    
                string r = "";
                //HOURS
                r += ((int)secondsLeft / 3600).ToString() + "h";
                secondsLeft -= ((int)secondsLeft / 3600) * 3600;
                //MINUTES
                r += ((int)secondsLeft / 60).ToString("00") + "m ";
                //SECONDS
                r += (secondsLeft % 60).ToString("00") + "s";
                Time.text = r;

            }
        }
    
    
        public void Click()
        {
                lastTimeClicked = (ulong)DateTime.Now.Ticks;
                PlayerPrefs.SetString("LastTimeClicked", lastTimeClicked.ToString());
                ClickButton.interactable = false;
                
    
    
        }
        private bool Ready()
        {
            ulong diff = ((ulong)DateTime.Now.Ticks - lastTimeClicked);
            ulong m = diff / TimeSpan.TicksPerMillisecond;
    
            float secondsLeft = (float)(msToWait - m) / 1000.0f;
    
            if (secondsLeft < 0)
            {
                //do stuff here
                currentDay++;
                SaveDay();
                return true;
            }
    
            return false;
        }
        void SaveDay()
        {
            PlayerPrefs.SetInt("currentDay", currentDay);
            PlayerPrefs.Save();
        }
    }
