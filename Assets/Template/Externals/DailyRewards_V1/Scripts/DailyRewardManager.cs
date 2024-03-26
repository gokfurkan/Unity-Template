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
        [SerializeField] private List<DailyRewardButton> rewardButtons;
        [SerializeField] private List<RewardButtonOption> rewardButtonOptions;

        [SerializeField] private TextMeshProUGUI remainingRewardText;

        private SaveData saveData;

        protected override void Initialize()
        {
            base.Initialize();
            
            InitializeDailyRewardSystem();
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
                    EconomyManager.Instance.SpawnMoneys(rewardOption.rewardImage.rectTransform , 50);
                    break;
            }
            
            saveData.rewardsUnlockStatus[saveData.lastRewardClaimIndex] = 2;
            saveData.lastRewardClaimIndex++;
            
            RefreshRewardButtons();
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
        public Image rewardImage;
        public Sprite rewardIcon;
        public int rewardAmount;
    }
    
    public enum DailyRewardGiveType
    {
        Money,
    }
}