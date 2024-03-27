using Template.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Template.Externals.DailyRewards_V1.Scripts
{
    public class DailyRewardButton : MonoBehaviour
    {
        [SerializeField] private ButtonClickController collectButton;
        
        [Space(10)]
        [SerializeField] private GameObject lockStatus;
        [SerializeField] private GameObject unLockStatus;
        [SerializeField] private GameObject collectStatus;
        
        [Space(10)]
        [SerializeField] private TextMeshProUGUI rewardAmountText;
        public Image rewardIconImage;
        
        public RewardButtonOption DailyRewardOption { get; set; }
        
        public void InitializeRewardButton()
        {
            DisableAllStatus();
            
            rewardIconImage.sprite = DailyRewardOption.rewardIcon;
            rewardAmountText.text = DailyRewardOption.rewardAmount.ToString();
        }
        
        public void RefreshRewardButton(int activeStatus)
        {
            DisableAllStatus();
            
            switch (activeStatus)
            {
                case 0:
                    lockStatus.SetActive(true);
                    break;
                case 1:
                    unLockStatus.SetActive(true);
                    collectButton.enabled = true;
                    break;
                case 2:
                    unLockStatus.SetActive(true);
                    collectStatus.SetActive(true);
                    break;
            }
        }

        public void CollectReward()
        {
            DailyRewardManager.Instance.OnCollectReward(this);
        }

        private void DisableAllStatus()
        {
            lockStatus.SetActive(false);
            unLockStatus.SetActive(false);
            collectStatus.SetActive(false);
            collectButton.enabled = false;
        }
    }
}