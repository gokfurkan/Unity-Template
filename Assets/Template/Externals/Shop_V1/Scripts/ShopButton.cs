using Template.Scripts;
using UnityEngine;

namespace Template.Externals.Shop_V1.Scripts
{
    public class ShopButton : MonoBehaviour
    {
        public ShopOptions shopOptions;
        public ButtonClickController buttonClickController;
        
        [Space(10)]
        public ShopButtonOptions buttonOptions;

        private void Start()
        {
            gameObject.GetComponent<ButtonClickController>().onClick.AddListener(SetInit);
        }

        private void SetInit()
        {
            ShopManager.Instance.OnSetSkin(buttonOptions.buttonIndex);
        }

        public void ChangeButtonLockStatus(bool lockStatus)
        {
            if (lockStatus)
            {
                buttonClickController.enabled = false;
                buttonOptions.lockIcon.gameObject.SetActive(true);
                buttonOptions.skinIcon.gameObject.SetActive(false);
            }
            else
            {
                buttonClickController.enabled = true;
                buttonOptions.lockIcon.gameObject.SetActive(false);
                buttonOptions.skinIcon.gameObject.SetActive(true);
            }
        }

        public void ChangeButtonSelectStatus(int selectedIndex)
        {
            if (selectedIndex == buttonOptions.buttonIndex)
            {
                buttonOptions.darkOutline.gameObject.SetActive(false);
                buttonOptions.whiteOutline.gameObject.SetActive(true);
                
                buttonOptions.buttonBg.color = shopOptions.rarityOptions[(int)buttonOptions.skinRarity].activeButtonColor;
            }
            else
            {
                buttonOptions.darkOutline.gameObject.SetActive(true);
                buttonOptions.whiteOutline.gameObject.SetActive(false);
                
                buttonOptions.buttonBg.color = shopOptions.rarityOptions[(int)buttonOptions.skinRarity].deActiveButtonColor;
            }
        }
    }
}