using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;


     
    public class TimeReward : MonoBehaviour
    {
        DateTime lastLogin;
        public TMP_Text timer, coinsPMText, xpPMText;
        TimeSpan timeSpan;
        int sec, min, hours;
        int currentLevel;
        int coinsPM, xpPM;
    private int globalCoins, currentXP;

    void Start()
        {
            


            if(PlayerPrefs.HasKey("lastLogin"))
            {
                lastLogin = DateTime.Parse(PlayerPrefs.GetString("lastLogin", "Nothing here"));

            } else {

                PlayerPrefs.SetString("lastLogin", DateTime.Now.ToString());
                PlayerPrefs.Save();
            }            
            
            Debug.Log(lastLogin);

        }
        private void Update()
        {
            currentLevel = PlayerPrefs.GetInt("currentlevel", 0);
            TimeCounter();
            RewardSet();

        }
        public void OnClaim()
        {
            int rewardCoins;
            int rewardXP;
            if(min > 0)
            {
                globalCoins = PlayerPrefs.GetInt("globalCoins", 0);
                currentXP = PlayerPrefs.GetInt("currentxp", 0);

                rewardCoins = coinsPM * (int)timeSpan.TotalMinutes;//convert the total minutes to int
                rewardXP = xpPM * (int)timeSpan.TotalMinutes;//convert the total minutes to int
                //Reset the Timer on click
                PlayerPrefs.SetString("lastLogin", DateTime.Now.ToString());
                
                PlayerPrefs.SetInt("globalCoins", globalCoins + rewardCoins);
                
                PlayerPrefs.Save();
            }
        }
        void RewardSet()
        {
            coinsPM = (int)(((currentLevel * (currentLevel / 2)) / 80) * 5) + 20;
            xpPM = (int)(((currentLevel * (currentLevel / 2)) / 80) * 10) + 30;
            coinsPMText.text = coinsPM.ToString() + " P/M";
            xpPMText.text = xpPM.ToString() + " P/M";
        }
        public void ResetOnPlay()
        {
            PlayerPrefs.SetString("lastLogin", DateTime.Now.ToString());
            PlayerPrefs.Save();
        }
        void TimeCounter()
        {
            if(PlayerPrefs.HasKey("lastLogin"))
            {

                timeSpan = DateTime.Now - lastLogin;
                if(timeSpan.Minutes < 15)
                {
                    timeSpan = DateTime.Now - lastLogin;
                    
                    
                } else {
                    timeSpan = new TimeSpan(0,15,0);
                }
                string str = "";

                sec = timeSpan.Seconds;
                min = timeSpan.Minutes;
                hours = timeSpan.Hours;

                str += hours.ToString("0") + ":";
                str += min.ToString("00") + ":";
                str += sec.ToString("00") + "s";

                timer.text = str;
                

                
                
            }
        }


    }