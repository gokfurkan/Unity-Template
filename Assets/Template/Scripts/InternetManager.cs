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
                ControlNoInternetPanel();
                
                remainingCheckTime = checkPerTime;
            }
        }

        private void CheckInternetConnection()
        {
            switch (Application.internetReachability)
            {
                case NetworkReachability.NotReachable:
                    hasInternet = false;
                    // Debug.Log("No internet connection");
                    break;
                case NetworkReachability.ReachableViaCarrierDataNetwork:
                    hasInternet = true;
                    // Debug.Log("Internet connection available via mobile data");
                    break;
                case NetworkReachability.ReachableViaLocalAreaNetwork:
                    hasInternet = true;
                    // Debug.Log("Internet connection available via WiFi");
                    break;
            }
        }

        private void ControlNoInternetPanel()
        {
            if (hasInternet)
            {
                PanelManager.Instance.DeActivateNoInternetPanel();
            }
            else
            {
                PanelManager.Instance.ActivateNoInternetPanel();
            }
        }

        public void RetryCheckInternetWithRestart()
        {
            // SingletonManager.Instance.DestroyAllSingletons();
            SceneManager.LoadScene((int)SceneType.Load);
        }

        public bool GetHasInternet()
        {
            return hasInternet;
        }
    }
}