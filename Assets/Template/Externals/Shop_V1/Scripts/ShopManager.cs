using System;
using System.Collections.Generic;
using Template.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Shop_V1.Scripts
{
    public class ShopManager : Singleton<ShopManager>
    {
        public ShopOptions shopOptions;
        public PageSwiper pageSwiper;
        public GameObject skinButton;
        
        [Space(10)]
        public GameObject exclamationMark;
        public Image selectedSkin;
        public List<TextMeshProUGUI> costTexts;

        [Space(10)] 
        public ButtonClickController unlockButtonClick;
        public GameObject activeUnlockButton;
        public GameObject deActiveUnlockButton;
        
        [Space(10)]
        public List<RectTransform> rarityHolders;
        public List<Sprite> buttonIcons;
        public List<ShopButton> shopButtons;

        private int totalCreatedButton;
        

        private void OnEnable()
        {
            BusSystem.OnSetMoneys += CallSetExclamationMarkEnabledDelayed;
            BusSystem.OnSetMoneys += SetUnlockButtonActivate;
            BusSystem.OnChangeShopPanelPage += OnChangeShopPage;
        }

        private void OnDisable()
        {
            BusSystem.OnSetMoneys -= CallSetExclamationMarkEnabledDelayed;
            BusSystem.OnSetMoneys -= SetUnlockButtonActivate;
            BusSystem.OnChangeShopPanelPage -= OnChangeShopPage;
        }

        protected override void Initialize()
        {
            base.Initialize();
            
            exclamationMark.SetActive(false);
            InitShop();
        }

        private void InitShop()
        {
            InitializeShopButtons();
            InitializeSkinUnlockData();
            InitSetSkin(SaveManager.Instance.saveData.currentSkin);
        }

        private void InitSetSkin(int selectedItem)
        {
            SetItemUnlockStatus();
            SetItemSelectStatus(selectedItem);
            
            BusSystem.CallSetPlayerSkin();
        }
        
        public void OnSetSkin(int buttonIndex)
        {
            SaveManager.Instance.saveData.currentSkin = buttonIndex;
            SaveManager.Instance.Save();

            SetItemUnlockStatus();
            SetItemSelectStatus(buttonIndex);
            
            BusSystem.CallSetPlayerSkin();
        }
        
        public void UnlockRandomSkin()
        {
            var currentRarity = shopOptions.rarityOptions[pageSwiper.currentPage - 1];
            var currentRarityCost = currentRarity.rarityCost;
            var buttonAmount = currentRarity.buttonAmount;

            if (SaveManager.Instance.saveData.GetMoneys() < currentRarityCost) return;

            var startIndex = (pageSwiper.currentPage - 1) * buttonAmount;
            var endIndex = pageSwiper.currentPage * buttonAmount;

            var lockedItemIndices = FindLockedItems(startIndex, endIndex);

            if (lockedItemIndices.Count == 0)
            {
                Debug.Log("All items are already unlocked.");   
                return;
            }

            var randomLockedIndex = lockedItemIndices[Random.Range(0, lockedItemIndices.Count)];
            SaveManager.Instance.saveData.skinsUnlockStatus[randomLockedIndex] = true;
            SaveManager.Instance.Save();

            SetItemUnlockStatus();
            EconomyManager.Instance.AddMoneys(-currentRarityCost);
        }
        
        private void OnChangeShopPage()
        {
            SetUnlockButtonActivate();
            SetUnlockCostText();
            SetItemUnlockStatus();
        }
        
        private void SetUnlockCostText()
        {
            var currentCost = shopOptions.rarityOptions[pageSwiper.currentPage - 1].rarityCost;
            for (var i = 0; i < costTexts.Count; i++)
            {
                costTexts[i].text = currentCost.ToString();
            }
        }
        
        private void SetItemUnlockStatus()
        {
            for (var i = 0; i < shopButtons.Count; i++)
            {
                var isSkinUnlocked = SaveManager.Instance.saveData.skinsUnlockStatus[i];
                shopButtons[i].ChangeButtonLockStatus(!isSkinUnlocked);
            }
        }

        private void SetItemSelectStatus(int selectedItem)
        {
            for (int i = 0; i < shopButtons.Count; i++)
            {
                shopButtons[i].ChangeButtonSelectStatus(selectedItem);
            }

            selectedSkin.sprite = buttonIcons[0];
        }
        
        private void InitializeShopButtons()
        {
            for (int rarityIndex = 0; rarityIndex < shopOptions.rarityOptions.Count; rarityIndex++)
            {
                for (int buttonIndex = 0; buttonIndex < shopOptions.rarityOptions[rarityIndex].buttonAmount; buttonIndex++)
                {
                    GameObject newSkinButtonObject = Instantiate(skinButton, rarityHolders[rarityIndex]);
                    ShopButton newSkinButtonComponent = newSkinButtonObject.GetComponent<ShopButton>();

                    newSkinButtonComponent.buttonOptions.skinIcon.sprite = buttonIcons[0];
                    newSkinButtonComponent.buttonOptions.buttonIndex = totalCreatedButton;
                    newSkinButtonComponent.buttonOptions.skinRarity = shopOptions.rarityOptions[rarityIndex].skinRarity;
                    
                    shopButtons.Add(newSkinButtonComponent);
                    totalCreatedButton++;
                }
            }
        }
        
        private void InitializeSkinUnlockData()
        {
            if (SaveManager.Instance.saveData.firstSetShopUnlockStatus == 0)
            {
                SaveManager.Instance.saveData.skinsUnlockStatus.Clear();
                
                for (int i = 0; i < shopButtons.Count; i++)
                {
                    SaveManager.Instance.saveData.skinsUnlockStatus.Add(i == 0);
                }
                
                SaveManager.Instance.saveData.firstSetShopUnlockStatus = 1;
                
                SaveManager.Instance.Save();
            }
        }

        private void SetUnlockButtonActivate()
        {
            bool HasMoney()
            {
                int money = SaveManager.Instance.saveData.GetMoneys();
                return money >= GetCurrentRarityCost();
            }

            unlockButtonClick.enabled = HasMoney();
            activeUnlockButton.gameObject.SetActive(HasMoney());
            deActiveUnlockButton.gameObject.SetActive(!HasMoney());
            
            var buttonAmount = shopOptions.rarityOptions[pageSwiper.currentPage - 1].buttonAmount;
            var startIndex = (pageSwiper.currentPage - 1) * buttonAmount;
            var endIndex = pageSwiper.currentPage * buttonAmount;
            var lockedItemIndices = FindLockedItems(startIndex, endIndex);
            unlockButtonClick.gameObject.SetActive(lockedItemIndices.Count != 0);
        }
        
        private void CallSetExclamationMarkEnabledDelayed()
        {
            Invoke(nameof(SetExclamationMarkEnabled), shopOptions.markControlDelay);
        }
        
        private void SetExclamationMarkEnabled()
        {
            // Check if the exclamation mark should be shown
            bool shouldShowExclamationMark = ShouldShowExclamationMark();
            exclamationMark.SetActive(shouldShowExclamationMark);
        }
        
        private List<int> FindLockedItems(int startIndex, int endIndex)
        {
            var lockedItems = new List<int>();

            for (var i = startIndex; i < endIndex; i++)
            {
                if (!SaveManager.Instance.saveData.skinsUnlockStatus[i])
                {
                    lockedItems.Add(i);
                }
            }

            return lockedItems;
        }

        private int[] GetPageEndAmounts()
        {
            int[] pageEndAmounts = new int[shopOptions.rarityOptions.Count];
            
            for (int i = 0; i < shopOptions.rarityOptions.Count; i++)
            {
                if (i == 0)
                {
                    pageEndAmounts[i] = shopOptions.rarityOptions[i].buttonAmount;
                }
                else
                {
                    pageEndAmounts[i] = pageEndAmounts[i - 1] + shopOptions.rarityOptions[i].buttonAmount;
                }
            }

            return pageEndAmounts;
        }

        private bool[] GetPageFullBuyStatus()
        {
            int[] pageEndAmounts = GetPageEndAmounts();
            
            bool[] pageFullBuyStatuses = new bool[pageEndAmounts.Length];
            
            for (int i = 0; i < pageEndAmounts.Length; i++)
            {
                int startAmount = (i == 0) ? 0 : pageEndAmounts[i - 1];
                int endAmount = pageEndAmounts[i];
                pageFullBuyStatuses[i] = CheckPageFullBuy(SaveManager.Instance.saveData.skinsUnlockStatus, startAmount, endAmount);
            }

            return pageFullBuyStatuses;
        }

        private bool CheckPageFullBuy(IReadOnlyList<bool> skinsUnlockStatus, int startIndex, int endIndex)
        {
            for (int i = startIndex; i < endIndex; i++)
            {
                if (!skinsUnlockStatus[i])
                {
                    return false;
                }
            }
            return true;
        }

        private int[] GetRarityCosts()
        {
            int[] rarityCosts = new int[shopOptions.rarityOptions.Count];
            
            for (int i = 0; i < shopOptions.rarityOptions.Count; i++)
            {
                rarityCosts[i] = shopOptions.rarityOptions[i].rarityCost;
            }

            return rarityCosts;
        }

        private int GetCurrentRarityCost()
        {
            return GetRarityCosts()[pageSwiper.currentPage - 1];
        }
        
        private bool ShouldShowExclamationMark()
        {
            int money = SaveManager.Instance.saveData.GetMoneys();
            
            int[] rarityCosts = GetRarityCosts();
            
            bool[] pageFullBuyStatuses = GetPageFullBuyStatus();
                
            for (int i = 0; i < rarityCosts.Length; i++)
            {
                if (!pageFullBuyStatuses[i] && money >= rarityCosts[i])
                {
                    return true;
                }
            }

            return false;
        }
    }
    
    [Serializable]
    public class ShopButtonOptions
    {
        public int buttonIndex;
        public SkinRarity skinRarity;
        
        [Space(10)] 
        public Image skinIcon;
        public Image lockIcon;
        public Image buttonBg;

        [Space(10)]
        public Image darkOutline;
        public Image whiteOutline;
        public Image yellowOutline;
    }

    [Serializable]
    public class ShopRarityOptions
    {
        public SkinRarity skinRarity;
        public int buttonAmount;
        public int rarityCost;
        public Color activeButtonColor;
        public Color deActiveButtonColor;
    }
}