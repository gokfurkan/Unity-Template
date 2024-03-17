using UnityEngine;

namespace Template.Scripts
{
    public class HapticManager : Singleton<HapticManager>
    {
        private float hapticTime;
        private bool canPlayHaptic;
        
        protected override void Initialize()
        {
            base.Initialize();
            
            bool currentHapticState = SaveManager.Instance.saveData.GetHaptic();
            MoreMountains.NiceVibrations.MMVibrationManager.SetHapticsActive(currentHapticState);
        }

        private void Update()
        {
            CheckHaptic();
        }

        private void CheckHaptic()
        {
            if (canPlayHaptic)
                return;

            hapticTime += Time.deltaTime;
            if (hapticTime >= 0.1f)
            {
                canPlayHaptic = true;
                hapticTime = 0;
            }
        }

        public void PlayHaptic(MoreMountains.NiceVibrations.HapticTypes type)
        {
            if (!canPlayHaptic) return;
            
            MoreMountains.NiceVibrations.MMVibrationManager.Haptic(type);
        }
    }
}