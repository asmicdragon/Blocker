using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class DailyRewards : MonoBehaviour
{
    public static DailyRewards dailyRewards {get; set;}
        public TMP_Text Time;
        [HideInInspector]
        public float msToWait;
        public Button ClickButton;
        public Button[] DayArray = new Button[7];
        private ulong lastTimeClicked;
        public bool rewardReady = false;
        
        public int currentDay = 0;
        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>

        private void Start()
        {
            
            dailyRewards = this;
            currentDay = PlayerPrefs.GetInt("currentDay", 0);
            msToWait = PlayerPrefs.GetFloat("mstowait", 0);

            ClickButton = DayArray[currentDay];
            
            if(PlayerPrefs.HasKey("LastTimeClicked")){
			lastTimeClicked = ulong.Parse(PlayerPrefs.GetString("LastTimeClicked"));
            Debug.Log("Player has: "+ lastTimeClicked.ToString());
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
                    int showDay = currentDay + 1;
                    ClickButton.interactable = true;
                    Time.text = "Day "+ showDay.ToString() +" Reward Available!";
                    rewardReady = true;
                    return;
                }
                int showRewardDay = PlayerPrefs.GetInt("currentDay", 0) + 1;
                if(showRewardDay > 7) {
                    showRewardDay = 1;
                }
                ulong diff = ((ulong)DateTime.Now.Ticks - lastTimeClicked);
                ulong m = diff / TimeSpan.TicksPerMillisecond;
                float secondsLeft = (float)(msToWait - m) / 1000.0f;
    
                string r = "";
                //HOURS
                r += ((int)secondsLeft / 3600).ToString() + "h ";
                secondsLeft -= ((int)secondsLeft / 3600) * 3600;
                //MINUTES
                r += ((int)secondsLeft / 60).ToString("00") + "m ";
                //SECONDS
                r += (secondsLeft % 60).ToString("00") + "s";
                Time.text = "Day "+showRewardDay.ToString()+" Reward in "+r.ToString();

            }
        }
        // public void DailyIconClick()
        // {
        //     if(PlayerPrefs.HasKey("LastTimeClicked")){
		// 	lastTimeClicked = ulong.Parse(PlayerPrefs.GetString("LastTimeClicked"));
        //     Debug.Log("Player has: "+ lastTimeClicked.ToString());
        //     }else{
        //         lastTimeClicked = (ulong)DateTime.Now.Ticks;
        //         PlayerPrefs.SetString("LastTimeClicked", lastTimeClicked.ToString());
        //         Debug.Log("Last time clicked wasn't set, it is now: "+ lastTimeClicked.ToString());
        //         NewPlayerDayOne();
        //     }
        // }
    
        public void Click()
        {

                lastTimeClicked = (ulong)DateTime.Now.Ticks;
                PlayerPrefs.SetString("LastTimeClicked", lastTimeClicked.ToString());
                msToWait = 86400000; // Day time in ticks
                PlayerPrefs.SetFloat("mstowait", msToWait);
                PlayerPrefs.Save();


                if(currentDay < 6)
                {
                    currentDay++;
                    SaveDay();
                } else if(currentDay >= 6){
                    currentDay = 0;
                    SaveDay();
                }
                rewardReady = false;
                ClickButton.interactable = false;

                ClickButton = DayArray[currentDay];
    
        }
        private bool Ready()
        {
            ulong diff = ((ulong)DateTime.Now.Ticks - lastTimeClicked);
            ulong m = diff / TimeSpan.TicksPerMillisecond;
    
            float secondsLeft = (float)(msToWait - m) / 1000.0f;
    
            if (secondsLeft < 0)
            {
                //do stuff here
                
                return true;
            }
    
            return false;
        }
        void NewPlayerDayOne()
        {
            
            msToWait = 0;
            StartCoroutine(WaitForFrame());

        }
        void SaveDay()
        {
            PlayerPrefs.SetInt("currentDay", currentDay);
            PlayerPrefs.Save();
        }
        public void AddCoins(int coins)
        {
            var globalCoins = PlayerPrefs.GetInt("globalCoins", 0) + coins;
            PlayerPrefs.SetInt("globalCoins", globalCoins);
            PlayerPrefs.Save();
        }
        IEnumerator WaitForFrame()
        {
            yield return new WaitForEndOfFrame();
            msToWait = 86400000;
            PlayerPrefs.SetFloat("mstowait", msToWait);
            PlayerPrefs.Save();
        }
    }
