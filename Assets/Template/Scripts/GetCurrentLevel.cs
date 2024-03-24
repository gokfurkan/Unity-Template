using Template.Scripts.SO;
using TMPro;
using UnityEngine;

namespace Template.Scripts
{
    public class GetCurrentLevel : MonoBehaviour
    {
        [SerializeField] private LevelTextType textType;

        private void Start()
        {
            InitLevelTextType();
        }

        private void InitLevelTextType()
        {
            TextMeshProUGUI text = GetComponent<TextMeshProUGUI>();
            
            UIOptions uiOptions = InfrastructureManager.Instance.gameSettings.uiOptions;
            string levelNumber = " " + (SaveManager.Instance.saveData.GetLevel() + (textType != LevelTextType.LevelCompleted ? 1 : 0));
            
            string levelText = uiOptions.levelText;
            string completedText = uiOptions.levelCompletedText;
            string failedText = uiOptions.levelFailedText;

            switch (textType)
            {
                case LevelTextType.Level:
                    text.text = $"{levelText}{levelNumber}";
                    break;
                case LevelTextType.LevelCompleted:
                    text.text = uiOptions.hasEndPanelLevel ? $"{levelText} {completedText}" : completedText;
                    break;
                case LevelTextType.LevelFailed:
                    text.text = uiOptions.hasEndPanelLevel ? $"{levelText} {failedText}" : failedText;
                    break;
                default:
                    break;
            }
        }
    }
}