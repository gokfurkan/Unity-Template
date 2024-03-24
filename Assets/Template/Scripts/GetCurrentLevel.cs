using TMPro;
using UnityEngine;

namespace Template.Scripts
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class GetCurrentLevel : MonoBehaviour
    {
        [SerializeField] private LevelTextType textType;

        private void Start()
        {
            InitializeLevelText();
        }

        private void InitializeLevelText()
        {
            UIManager.Instance.SetCurrentLevelText(GetComponent<TextMeshProUGUI>() , textType);
        }
    }
}