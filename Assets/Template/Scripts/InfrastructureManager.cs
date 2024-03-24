using System;
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
            if (Application.isMobilePlatform)
            {
                Debug.unityLogger.logEnabled = false;
            }
            else
            {
                Debug.unityLogger.logEnabled = true;
            }
        }
    }
}
