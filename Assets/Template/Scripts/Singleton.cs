using UnityEngine;

namespace Template.Scripts
{
    public class Singleton<T> : MonoBehaviour where T : Component
    {
        public static T Instance { get; private set; }
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this as T;
                // SingletonManager.Instance.RegisterSingleton(Instance);
                Initialize();
            }
        }

        protected virtual void Initialize()
        {
        }
    }

    public class PersistentSingleton<T> : MonoBehaviour where T : Component
    {
        public static T Instance { get; private set; }
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this as T;
                // SingletonManager.Instance!.RegisterPersistentSingleton(Instance);
                DontDestroyOnLoad(gameObject);
                Initialize();
            }
        }

        protected virtual void Initialize()
        {
        }
    }
}