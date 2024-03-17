using TMPro;
using UnityEngine;

namespace Template.Scripts
{
    public class SettingsManager : MonoBehaviour
    {
        [SerializeField] private GameObject soundOn;
        [SerializeField] private GameObject soundOff;
        [SerializeField] private GameObject hapticOn;
        [SerializeField] private GameObject hapticOff;

        [Space(10)]
        [SerializeField] private TextMeshProUGUI versionText;

        private void Start()
        {
            InitSettings();
            InitGameVersion();
        }

        public void ToggleSound()
        {
            bool currentSoundState = SaveManager.Instance.saveData.GetSound();
    
            soundOn.SetActive(!currentSoundState);
            soundOff.SetActive(currentSoundState);
            
            AudioListener.volume = !currentSoundState ? 1 : 0;

            SaveManager.Instance.saveData.sound = !currentSoundState;
            SaveManager.Instance.Save();
        }

        public void ToggleHaptic()
        {
            bool currentHapticState = SaveManager.Instance.saveData.GetHaptic();
    
            hapticOn.SetActive(!currentHapticState);
            hapticOff.SetActive(currentHapticState);
            
            MoreMountains.NiceVibrations.MMVibrationManager.SetHapticsActive(!currentHapticState);

            SaveManager.Instance.saveData.haptic = !currentHapticState;
            SaveManager.Instance.Save();
        }

        private void InitSettings()
        {
            bool currentSoundState = SaveManager.Instance.saveData.GetSound();
            bool currentHapticState = SaveManager.Instance.saveData.GetHaptic();

            SetActiveState(soundOn, soundOff, currentSoundState);
            SetActiveState(hapticOn, hapticOff, currentHapticState);

            void SetActiveState(GameObject onObject, GameObject offObject, bool state)
            {
                onObject.SetActive(state);
                offObject.SetActive(!state);
            }
        }

        private void InitGameVersion()
        {
            string title = "version ";
            versionText.text = title + Application.version;
        }
    }
}