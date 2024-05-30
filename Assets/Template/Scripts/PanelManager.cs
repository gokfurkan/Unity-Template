using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Template.Externals.DailyRewards_V1.Scripts;
using UnityEngine;

namespace Template.Scripts
{
    public class PanelManager : Singleton<PanelManager>
    {
        private List<PanelTypeHolder> allPanels = new List<PanelTypeHolder>();

        protected override void Initialize()
        {
            base.Initialize();
            
            InitializePanelSystem();
        }

        private void OnEnable()
        {
            BusSystem.OnLevelStart += ActivateGamePanel;
            BusSystem.OnLevelEnd += ActivateEndPanel;
        }

        private void OnDisable()
        {
            BusSystem.OnLevelStart -= ActivateGamePanel;
            BusSystem.OnLevelEnd -= ActivateEndPanel;
        }

        private void InitializePanelSystem()
        {
            GetAllPanels();
            ActivateMenuPanel();
        }

        private void ActivateMenuPanel()
        {
            DisableAll();
            
            Activate(PanelType.Money);
            Activate(PanelType.Level);
            Activate(PanelType.OpenDev);
            Activate(PanelType.OpenSettings);
            
            // Activate(PanelType.Restart);
            
            // Activate(PanelType.OpenShop);
            // Activate(PanelType.OpenDailyRewards);
        }

        private void ActivateGamePanel()
        {
            Activate(PanelType.OpenDev , false);
            Activate(PanelType.OpenSettings , false);
        }

        private void ActivateEndPanel(bool win)
        {
            DisableAll();
            
            Activate(PanelType.Money);
            
            StartCoroutine(ActivateEndPanelDelay(win));
        }
        
        private IEnumerator ActivateEndPanelDelay(bool win)
        {
            Activate(PanelType.EndContinue , false);
            
            if (win)
            {
                yield return new WaitForSeconds(InfrastructureManager.Instance.gameSettings.uiOptions.winPanelDelay);
                
                Activate(PanelType.Win);
                
                EconomyManager.Instance.AddMoneys(InfrastructureManager.Instance.gameSettings.economyOptions.winIncome);
                EconomyManager.Instance.SpawnMoneys(null);
            }
            else
            {
                yield return new WaitForSeconds(InfrastructureManager.Instance.gameSettings.uiOptions.losePanelDelay);
                
                Activate(PanelType.Lose);
                
                EconomyManager.Instance.AddMoneys(InfrastructureManager.Instance.gameSettings.economyOptions.loseIncome);
            }
            
            yield return new WaitForSeconds(InfrastructureManager.Instance.gameSettings.uiOptions.endContinueDelay);
                
            Activate(PanelType.EndContinue);
        }

        public void ChangeSettingsPanelEnabled(bool status)
        {
            Activate(PanelType.OpenSettings , !status);
            Activate(PanelType.Settings , status);
        }
        
        public void ChangeShopPanelEnabled(bool status)
        {
            Activate(PanelType.OpenShop , !status);
            Activate(PanelType.Shop , status);
        }
        
        public void ChangeDailyRewardsPanelEnabled(bool status)
        {
            DailyRewardManager.Instance.RefreshDateTime();
            DailyRewardManager.Instance.HasOpenRewardPanel = status;
            
            Activate(PanelType.OpenDailyRewards , !status);
            Activate(PanelType.DailyRewards , status);
        }

        public void ChangeDevPanelEnabled(bool status)
        {
            Activate(PanelType.Dev , status);
            Activate(PanelType.OpenDev , !status);
        }

        public void ChangeInternetErrorPanelEnabled(bool status)
        {
            Activate(PanelType.InternetCheck , status);
        }

        public void LoadLevel()
        {
            BusSystem.CallLevelLoad();
        }

        public void ReloadGame()
        {
            BusSystem.CallGameReload();
        }
        
        private void Activate(PanelType panelType, bool activate = true)
        {
            List<PanelTypeHolder> panels = FindPanels(panelType);

            if (panels != null)
            {
                for (int i = 0; i < panels.Count; i++)
                {
                    panels[i].gameObject.SetActive(activate);
                }
            }
            else
            {
                Debug.LogWarning("Panel not found: " + panelType.ToString());
            }
        }
        
        private void DisableAll()
        {
            foreach (var panel in allPanels)
            {
                panel.gameObject.SetActive(false);
            }
        }
        
        private List<PanelTypeHolder> FindPanels(PanelType panelType)
        {
            return allPanels.FindAll(panel => panel.panelType == panelType);
        }
        
        private void GetAllPanels()
        {
            allPanels = transform.root.GetComponentsInChildren<PanelTypeHolder>(true).ToList();
        }
    }
}