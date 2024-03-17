using UnityEngine;

namespace Template.Scripts
{
    public class AudioManager : Singleton<AudioManager>
    {
        [SerializeField] private AudioClip[] effects;
        [SerializeField] private AudioSource source;

        protected override void Initialize()
        {
            base.Initialize();
            
            bool currentSoundState = SaveManager.Instance.saveData.GetSound();
            AudioListener.volume = currentSoundState ? 1 : 0;
        }

        public void PlaySound(AudioType type)
        {
            source.PlayOneShot(effects[(int)type]);
        }
    }
}