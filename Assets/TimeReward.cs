using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;


     
    public class TimeReward : MonoBehaviour
    {
        public static TimeReward timeReward {get; set;}
        public DateTime lastLogin;
        public TMP_Text timer, coinsPMText, xpPMText, efficiencyText;
        public TimeSpan timeSpan;

        public Animator anim;
        int sec, min, hours;
        int currentLevel;
        int coinsPM, xpPM;
        public int rewardXP,rewardCoins;
    private int globalCoins, currentXP;
    int offlinePercent;
    public bool enableClaimScreen;
    void Start()
        {

            enableClaimScreen = false;
            timeReward = this;

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

            if(PlayerPrefs.HasKey("newPlayer"))
            {
                anim.SetBool("Play", false);
            } else {
                anim.SetBool("Play", true);
            }
        }
        public void OnClaim()
        {

            if(timeSpan.TotalMinutes > 0)
            {
                enableClaimScreen = true;
                globalCoins = PlayerPrefs.GetInt("globalCoins", 0);
                currentXP = PlayerPrefs.GetInt("currentxp", 0);

                rewardCoins = coinsPM * (int)timeSpan.TotalMinutes;//convert the total minutes to int
                rewardXP = xpPM * (int)timeSpan.TotalMinutes;//convert the total minutes to int
                //Reset the Timer on click
                

                PlayerPrefs.SetInt("gameplayPercent", 100);

                PlayerPrefs.SetInt("offlinePercent", 0);

                PlayerPrefs.SetInt("gameplayTime", 0);
                
                
                PlayerPrefs.SetInt("globalCoins", globalCoins + rewardCoins);

                PlayerPrefs.SetString("lastLogin", DateTime.Now.ToString());
                PlayerPrefs.Save();
            }
        }
        void RewardSet()
        {
            offlinePercent = PlayerPrefs.GetInt("offlinePercent", 0);
            float multiplier;
            string str = "" + offlinePercent.ToString();
            multiplier = float.Parse(str);
            multiplier /= 100;
            coinsPM = (int)(((((currentLevel * (currentLevel / 4)) / 80) * 2) + 30) * multiplier);//Integrated with efficiency
            xpPM = (int)(((((currentLevel * (currentLevel / 4)) / 80) * 5) + 60) * multiplier);//Integrated with efficiency
            coinsPMText.text = coinsPM.ToString() + " P/M";
            xpPMText.text = xpPM.ToString() + " P/M";
            efficiencyText.text = offlinePercent.ToString() + "%";


            // Debug.Log("offline efficiency is " + multiplier.ToString() + " Multiplier");
        }
        // public void ResetOnPlay()
        // {
        //     PlayerPrefs.SetString("lastLogin", DateTime.Now.ToString());
        //     PlayerPrefs.Save();
        // }
        void TimeCounter()
        {
            if(PlayerPrefs.HasKey("lastLogin"))
            {

                timeSpan = DateTime.Now - lastLogin;

                if(timeSpan.Hours < 12)
                {
                    timeSpan = DateTime.Now - lastLogin;
                    
                    
                } else {
                    timeSpan = new TimeSpan(12,0,0);
                    PlayerPrefs.DeleteKey("newPlayer");
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