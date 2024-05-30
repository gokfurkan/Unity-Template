using Sirenix.OdinInspector;
using Template.Scripts.SO;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Template.Scripts
{
    public class InternetManager : Singleton<InternetManager>
    {
        [ReadOnly] public bool hasInternet;
        [ReadOnly] public float remainingCheckTime;
        
        private float checkPerTime;
        private GameSettings gameSettings;

        protected override void Initialize()
        {
            base.Initialize();

            gameSettings = InfrastructureManager.Instance.gameSettings;
            
            checkPerTime = gameSettings.connectionOptions.checkInternetPerTime;
            remainingCheckTime = gameSettings.connectionOptions.startCheckTime;
        }

        private void Update()
        {
            remainingCheckTime -= Time.deltaTime;
            
            if (remainingCheckTime <= 0)
            {
                CheckInternetConnection();
                ControlInternetErrorPanel();
                
                remainingCheckTime = checkPerTime;
            }
        }

        private void CheckInternetConnection()
        {
            switch (Application.internetReachability)
            {
                case NetworkReachability.NotReachable:
                    hasInternet = false;
                    break;
                case NetworkReachability.ReachableViaCarrierDataNetwork:
                    hasInternet = true;
                    break;
                case NetworkReachability.ReachableViaLocalAreaNetwork:
                    hasInternet = true;
                    break;
            }
        }

        private void ControlInternetErrorPanel()
        {
            PanelManager.Instance.ChangeInternetErrorPanelEnabled(!hasInternet);
        }
    }
}