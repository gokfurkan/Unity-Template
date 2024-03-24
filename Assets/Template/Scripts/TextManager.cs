using Template.Scripts.SO;
using TMPro;
using UnityEngine;

namespace Template.Scripts
{
    public class TextManager : Singleton<TextManager>
    {
        private TMP_FontAsset textFont;
        private UIOptions uiOptions;
        
        protected override void Initialize()
        {
            base.Initialize();

            InitializeTextSystem();
        }

        private void InitializeTextSystem()
        {
            uiOptions = InfrastructureManager.Instance.gameSettings.uiOptions;
            textFont = uiOptions.textFont;
        }
        
        public void SetFont(TextMeshProUGUI textComponent)
        {
            if (textComponent != null && textFont != null)
            {
                textComponent.font = textFont;
            }
            else
            {
                Debug.LogWarning("Text component or font asset is null.");
            }
        }
    }
}