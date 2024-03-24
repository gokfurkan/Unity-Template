using Template.Scripts.SO;
using UnityEngine;

namespace Template.Scripts
{
    public class InfrastructureManager : PersistentSingleton<InfrastructureManager>
    {
        public GameSettings gameSettings;

        protected override void Initialize()
        {
            base.Initialize();

            SetFrameRateSettings();
            ChangeLogEnabled();
        }
        
        private void SetFrameRateSettings()
        {
            QualitySettings.vSyncCount = gameSettings.gamePlayOptions.vSyncEnabled ? 1 : 0;
            Application.targetFrameRate = gameSettings.gamePlayOptions.targetFPS;
        }

        private void ChangeLogEnabled()
        {
            if (Application.isEditor)
            {
                Debug.unityLogger.logEnabled = true;
            }
            else
            {
                Debug.unityLogger.logEnabled = false;
            }
        }
    }
}
