using System;
using System.Collections.Generic;
using UnityEngine;

namespace Template.Scripts
{
    [Serializable]
    public class SaveData
    {
        //Set
        public int level;
        public int moneys;
        
        [Header("Settings")]
        public bool sound = true;
        public bool haptic = true;
        
        [Header("Debug")]
        public List<LogData> logs = new List<LogData>();
        
        [Header("Shop")]
        public int currentSkin;
        public int firstSetShopUnlockStatus = 0;
        public List<bool> skinsUnlockStatus = new List<bool>(27);
        
        [Header("Daily Rewards")]
        public string lastRewardClaimDate;
        public int firstSetRewardUnlockStatus = 0;
        public List<bool> rewardsUnlockStatus = new List<bool>(7);
        
        
        //Get
        public int GetLevel()
        {
            return level;
        }

        public int GetMoneys()
        {
            return moneys;
        }

        public bool GetSound()
        {
            return sound;
        }

        public bool GetHaptic()
        {
            return haptic;
        }
    }
}