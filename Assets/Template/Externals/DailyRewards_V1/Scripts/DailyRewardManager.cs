using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Template.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Template.Externals.DailyRewards_V1.Scripts
{
    public class DailyRewardManager : Singleton<DailyRewardManager>
    {
        [SerializeField] private DailyRewardOptions dailyRewardOptions;
        [SerializeField] private TextMeshProUGUI remainingRewardText;
        [SerializeField] private GameObject exclamationMark;
        
        [Space(10)]
        [SerializeField] private List<DailyRewardButton> rewardButtons;
        [SerializeField] private List<RewardButtonOption> rewardButtonOptions;
        
        public bool HasOpenRewardPanel { get; set; }

        private float leftControlRemainingReward;
        
        private DateTime lastRewardClaimDate;
        private DateTime currentDateTime;
        private TimeSpan timeSinceLastClaim;
        
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
                    leftControlRemainingReward = dailyRewardOptions.perCheckClaimRewardLeft;
                }
            }
        }

        private void InitializeDailyRewardSystem()
        {
            saveData = SaveManager.Instance.saveData;
            leftControlRemainingReward = dailyRewardOptions.perCheckClaimRewardLeft;
            
            InitializeRewardData();
            InitializeRewardButtons();
            RefreshDateTime();
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
                    EconomyManager.Instance.SpawnMoneys(rewardButton.rewardIconImage.rectTransform, dailyRewardOptions.rewardSpawnUISize);
                    break;
            }
            
            saveData.rewardsUnlockStatus[saveData.lastRewardClaimIndex] = 2;
            
            currentDateTime = WorldTimeAPI.Instance.GetCurrentDateTime();
            saveData.lastRewardClaimDate = currentDateTime.ToString();
            timeSinceLastClaim = currentDateTime - lastRewardClaimDate;
            
            RefreshDateTime();
            
            SaveManager.Instance.Save();
        }

        public void RefreshDateTime()
        {
            if (saveData.lastRewardClaimDate != null)
            {
                lastRewardClaimDate = DateTime.Parse(saveData.lastRewardClaimDate);
            }
           
            currentDateTime = WorldTimeAPI.Instance.GetCurrentDateTime();
            timeSinceLastClaim = currentDateTime - lastRewardClaimDate;
            
            if (string.IsNullOrEmpty(saveData.lastRewardClaimDate))
            {
                Debug.Log("No previous reward claim record found.");
                
                exclamationMark.gameObject.SetActive(true);
                remainingRewardText.gameObject.SetActive(false);
                
                lastRewardClaimDate = currentDateTime;
                timeSinceLastClaim = TimeSpan.Zero;
                
                saveData.lastRewardClaimDate = lastRewardClaimDate.ToString();
            }
            else
            {
                if (saveData.rewardsUnlockStatus[saveData.lastRewardClaimIndex] == 1)
                {
                    exclamationMark.gameObject.SetActive(true);
                    remainingRewardText.gameObject.SetActive(false);
                    
                    Debug.Log("Reward not collected yet.");
                }
                else
                {
                    double hoursSinceLastClaim = timeSinceLastClaim.TotalHours;

                    if (hoursSinceLastClaim > dailyRewardOptions.rewardLoopHours)
                    {
                        if (HasAllCollected())
                        {
                            ResetRewardData();
                        
                            exclamationMark.gameObject.SetActive(false);
                            remainingRewardText.gameObject.SetActive(false);
                        
                            Debug.Log("All rewards collected");
                        }
                        else
                        {
                            exclamationMark.gameObject.SetActive(false);
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
                            
                            Debug.Log("Daily reward loop completed.");
                        }
                    }
                    else
                    {
                        exclamationMark.gameObject.SetActive(false);
                        remainingRewardText.gameObject.SetActive(true);
                        
                        Debug.Log("Daily reward loop not completed yet.");
                    }
                }   
            }

            SetRemainingRewardText();
            RefreshRewardButtons();
            
            SaveManager.Instance.Save();
        }

        private void SetRemainingRewardText()
        {
            var remainingReward = lastRewardClaimDate.AddHours(dailyRewardOptions.rewardLoopHours) - currentDateTime;
            
            remainingRewardText.text = $"{remainingReward.Hours.ToString().PadLeft(2, '0')}h" +
                                       $" {(remainingReward.Minutes).ToString().PadLeft(2, '0')}m" + " left to claim reward";
        }

        [Button]
        private void DecreaseRemainingClaimDate()
        {
            lastRewardClaimDate = DateTime.Parse(saveData.lastRewardClaimDate);
            lastRewardClaimDate = lastRewardClaimDate.AddHours(-1);
            saveData.lastRewardClaimDate = lastRewardClaimDate.ToString();
            
            RefreshDateTime();
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
            
            SaveManager.Instance.Save();
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
        [PreviewField]
        public Sprite rewardIcon;
        public int rewardAmount;
    }
    
    public enum DailyRewardGiveType
    {
        Money,
    }
}