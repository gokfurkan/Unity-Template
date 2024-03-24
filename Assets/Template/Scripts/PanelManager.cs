using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            
            Activate(PanelType.OpenDev);
            Activate(PanelType.Money);
            Activate(PanelType.Level);
            Activate(PanelType.OpenSettings);
            Activate(PanelType.Restart);
        }

        private void ActivateGamePanel()
        {
            
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
                EconomyManager.Instance.SpawnMoneys();
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

        public void ActivateSettingsPanel()
        {
            Activate(PanelType.OpenSettings , false);
            Activate(PanelType.Settings);
        }

        public void DeActivateSettingsPanel()
        {
            Activate(PanelType.Settings , false);
            Activate(PanelType.OpenSettings);
        }
        
        public void ActivateShopPanel()
        {
            Activate(PanelType.OpenShop , false);
            Activate(PanelType.Shop);
        }

        public void DeActivateShopPanel()
        {
            Activate(PanelType.Shop , false);
            Activate(PanelType.OpenShop);
        }
        
        public void ActivateDailyRewardsPanel()
        {
            Activate(PanelType.OpenDailyRewards , false);
            Activate(PanelType.DailyRewards);
        }

        public void DeActivateDailyRewardsPanel()
        {
            Activate(PanelType.DailyRewards , false);
            Activate(PanelType.OpenDailyRewards);
        }

        public void ActivateDevPanel()
        {
            Activate(PanelType.Dev);
            Activate(PanelType.OpenDev , false);
        }
        
        public void DeActivateDevPanel()
        {
            Activate(PanelType.Dev , false);
            Activate(PanelType.OpenDev);
        }

        public void LoadLevel()
        {
            BusSystem.CallLevelLoad();
        }
        
        public void Activate(PanelType panelType, bool activate = true)
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
        
        public void DisableAll()
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