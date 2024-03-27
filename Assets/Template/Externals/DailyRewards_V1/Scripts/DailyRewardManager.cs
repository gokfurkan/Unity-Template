using System;
using System.Collections.Generic;
using Template.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Template.Externals.DailyRewards_V1.Scripts
{
    public class DailyRewardManager : Singleton<DailyRewardManager>
    {
        [SerializeField] private DailyRewardOptions dailyRewardOptions;
        [SerializeField] private TextMeshProUGUI remainingRewardText;
        
        [Space(10)]
        [SerializeField] private List<DailyRewardButton> rewardButtons;
        [SerializeField] private List<RewardButtonOption> rewardButtonOptions;
        
        public bool HasOpenRewardPanel { get; set; }

        private DateTime lastRewardClaimDate;
        private DateTime currentDateTime;
        private TimeSpan timeSinceLastClaim;

        private float leftControlRemainingReward;
        
        private SaveData saveData;

        protected override void Initialize()
        {
            base.Initialize();
            
            InitializeDailyRewardSystem();
        }

        private void Update()
        {
            if (HasOpenRewardPanel)
            {
                leftControlRemainingReward -= Time.deltaTime;
                if (leftControlRemainingReward <= 0)
                {
                    RefreshDateTime();
                    leftControlRemainingReward = dailyRewardOptions.controlRewardLeftHours;
                }
            }
        }

        private void InitializeDailyRewardSystem()
        {
            saveData = SaveManager.Instance.saveData;
            
            InitializeRewardData();
            InitializeRewardButtons();
            RefreshRewardButtons();
        }
        
        private void InitializeRewardButtons()
        {
            for (int i = 0; i < rewardButtons.Count; i++)
            {
                rewardButtons[i].DailyRewardOption = rewardButtonOptions[i];
                rewardButtons[i].InitializeRewardButton();
            }
        }
        
        private void RefreshRewardButtons()
        {
            for (int i = 0; i < rewardButtons.Count; i++)
            {
                rewardButtons[i].RefreshRewardButton(saveData.rewardsUnlockStatus[i]);
            }
        }

        public void OnCollectReward(DailyRewardButton rewardButton)
        {
            var rewardOption = rewardButton.DailyRewardOption;
            
            switch (rewardOption.rewardGiveType)
            {
                case DailyRewardGiveType.Money:
                    EconomyManager.Instance.AddMoneys(rewardOption.rewardAmount);
                    EconomyManager.Instance.SpawnMoneys(rewardOption.rewardImage.rectTransform , dailyRewardOptions.rewardSpawnUISize);
                    break;
            }
            
            saveData.rewardsUnlockStatus[saveData.lastRewardClaimIndex] = 2;
            
            currentDateTime = WorldTimeAPI.Instance.GetCurrentDateTime();
            saveData.LastRewardClaimDate = currentDateTime;
            timeSinceLastClaim = currentDateTime - lastRewardClaimDate;
            
            RefreshDateTime();
            
            SaveManager.Instance.Save();
        }

        public void RefreshDateTime()
        {
            lastRewardClaimDate = saveData.LastRewardClaimDate;
            currentDateTime = WorldTimeAPI.Instance.GetCurrentDateTime();
            timeSinceLastClaim = currentDateTime - lastRewardClaimDate;
            
            if (lastRewardClaimDate == DateTime.MinValue)
            {
                Debug.Log("No previous reward claim record found.");

                remainingRewardText.gameObject.SetActive(false);
                
                lastRewardClaimDate = currentDateTime;
                timeSinceLastClaim = TimeSpan.Zero;
                
                saveData.LastRewardClaimDate = lastRewardClaimDate;
                SaveManager.Instance.Save();
            }
            else
            {
                double hoursSinceLastClaim = timeSinceLastClaim.TotalHours;

                if (hoursSinceLastClaim > dailyRewardOptions.rewardLoopHours)
                {
                    if (HasAllCollected())
                    {
                        ResetRewardData();
                        
                        SaveManager.Instance.Save();
                        
                        remainingRewardText.gameObject.SetActive(false);
                        
                        Debug.Log("All collected");
                    }
                    else
                    {
                        if (saveData.rewardsUnlockStatus[saveData.lastRewardClaimIndex] == 1)
                        {
                            remainingRewardText.gameObject.SetActive(false);
                            Debug.Log("Reward not collected yet.");
                        }
                        else
                        {
                            remainingRewardText.gameObject.SetActive(false);

                            saveData.lastRewardClaimIndex++;
                        
                            if (saveData.lastRewardClaimIndex >= saveData.rewardsUnlockStatus.Count)
                            {
                                Debug.Log("All rewards collected");
                            }
                            else
                            {
                                saveData.rewardsUnlockStatus[saveData.lastRewardClaimIndex] = 1;
                            }
                        
                            SaveManager.Instance.Save();
                        
                            Debug.Log("Daily reward loop completed.");
                        }   
                    }
                }
                else
                {
                    remainingRewardText.gameObject.SetActive(true);
                    Debug.Log("Daily reward loop not completed yet.");
                }
            }

            SetRemainingRewardText();
            RefreshRewardButtons();
        }

        private void SetRemainingRewardText()
        {
            var remainingReward = lastRewardClaimDate.AddHours(dailyRewardOptions.rewardLoopHours) - currentDateTime;
            Debug.Log(remainingReward);
            
            remainingRewardText.text = $"{remainingReward.Hours.ToString().PadLeft(2, '0')}h" +
                                       $" {(remainingReward.Minutes).ToString().PadLeft(2, '0')}m" + " left to claim reward";
        }

        private bool HasAllCollected()
        {
            bool hasAllCollected = true;
            
            for (int i = 0; i < saveData.rewardsUnlockStatus.Count; i++)
            {
                if (saveData.rewardsUnlockStatus[i] != 2)
                {
                    hasAllCollected = false;
                    break;
                }
            }

            return hasAllCollected;
        }

        private void ResetRewardData()
        {
            for (int i = 0; i < saveData.rewardsUnlockStatus.Count; i++)
            {
                saveData.rewardsUnlockStatus[i] = 0;
            }

            saveData.rewardsUnlockStatus[0] = 1;

            saveData.lastRewardClaimIndex = 0;
        }
        
        private void InitializeRewardData()
        {
            if (saveData.firstSetRewardUnlockStatus == 0)
            {
                saveData.rewardsUnlockStatus.Clear();
                
                for (int i = 0; i < rewardButtons.Count; i++)
                {
                    saveData.rewardsUnlockStatus.Add(0);
                }

                saveData.rewardsUnlockStatus[0] = 1;
                
                saveData.firstSetRewardUnlockStatus = 1;
                
                SaveManager.Instance.Save();
            }
        }
    }

    [Serializable]
    public class RewardButtonOption
    {
        public DailyRewardGiveType rewardGiveType;
        public Image rewardImage;
        public Sprite rewardIcon;
        public int rewardAmount;
    }
    
    public enum DailyRewardGiveType
    {
        Money,
    }
}