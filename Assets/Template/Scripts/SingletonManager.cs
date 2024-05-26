using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Template.Scripts
{
    public class SingletonManager : PersistentSingleton<SingletonManager>
    {
        [ReadOnly] public List<Component> singletons = new List<Component>();
        [ReadOnly] public List<Component> persistentSingletons = new List<Component>();

        public void RegisterSingleton<T>(T singleton) where T : Component
        {
            singletons.Add(singleton);
        }
        
        public void RegisterPersistentSingleton<T>(T singleton) where T : Component
        {
            persistentSingletons.Add(singleton);
        }

        public List<Component> GetSingletons()
        {
            return singletons;
        }
        
        public List<Component> GetPersistentSingletons()
        {
            return persistentSingletons;
        }

        public void DestroyAllSingletons()
        {
            for (int i = 0; i < singletons.Count; i++)
            {
                Destroy(singletons[i].gameObject);
            }
            
            for (int i = 0; i < persistentSingletons.Count; i++)
            {
                Destroy(persistentSingletons[i].gameObject);
            }
            
            singletons.Clear();
            persistentSingletons.Clear();
        }
    }
}