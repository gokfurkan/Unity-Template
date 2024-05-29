using System;
using Template.Scripts.SO;
using TMPro;
using UnityEngine;

namespace Template.Scripts
{
    public class UIManager : Singleton<UIManager>
    {
        #region CurrentLevel

        private int levelNumber;
        private string levelText;
        private string completedText;
        private string failedText;

        #endregion
        
        private UIOptions uiOptions;
        private EconomyOptions economyOptions;

        protected override void Initialize()
        {
            base.Initialize();

            InitializeUISystem();
        }

        private void InitializeUISystem()
        {
            uiOptions = InfrastructureManager.Instance.gameSettings.uiOptions;
            economyOptions = InfrastructureManager.Instance.gameSettings.economyOptions;

            //Current level
            levelText = uiOptions.levelText;
            completedText = uiOptions.levelCompletedText;
            failedText = uiOptions.levelFailedText;
            levelNumber = SaveManager.Instance.saveData.GetLevel();
        }

        public void SetCurrentLevelText(TextMeshProUGUI textComponent , LevelTextType textType)
        {
            if (textComponent != null)
            {
                switch (textType)
                {
                    case LevelTextType.Level:
                        textComponent.text = $"{levelText}{levelNumber + 1}";
                        break;
                    case LevelTextType.LevelCompleted:
                        textComponent.text = uiOptions.hasEndPanelLevel ? $"{levelText} {completedText}" : completedText;
                        break;
                    case LevelTextType.LevelFailed:
                        textComponent.text = uiOptions.hasEndPanelLevel ? $"{levelText} {failedText}" : failedText;
                        break;
                    default:
                        break;
                }
            }
            else
            {
                Debug.LogWarning("Text component is null.");
            }
        }

        public void SetCurrentIncomeText(TextMeshProUGUI textComponent, IncomeTextType textType)
        {
            if (textComponent != null)
            {
                switch (textType)
                {
                    case IncomeTextType.Win:
                        textComponent.text = economyOptions.winIncome.ToString();
                        break;
                    case IncomeTextType.Lose:
                        textComponent.text = economyOptions.loseIncome.ToString();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(textType), textType, null);
                }
            }
            else
            {
                Debug.LogWarning("Text component is null.");
            }
        }
    }
}