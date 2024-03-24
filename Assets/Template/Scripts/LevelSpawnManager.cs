using System.Collections.Generic;
using UnityEngine;

namespace Template.Scripts
{
    public class LevelSpawnManager : Singleton<LevelSpawnManager>
    {
        [SerializeField] private Transform playZone;
        [SerializeField] private int tutorialLevels = 0;
        [SerializeField] private List<GameObject> levelList;
        
        private GameObject loadedLevel;
        private List<GameObject> levelListCache = new List<GameObject>();

        protected override void Initialize()
        {
            base.Initialize();
            
            HandleNewLevelLoad();
        }

        private void HandleNewLevelLoad()
        {
            if (levelList.Count == 0)
            {
                Debug.LogWarning("Level list is empty!");    
                return;
            }
            
            playZone.RemoveAllChildren();

            levelListCache = new List<GameObject>();
            levelListCache.AddRange(levelList);
            
            int delta = 0;
            
            if (SaveManager.Instance.saveData.GetLevel() >= tutorialLevels)
            {
                for (int j = 0; j < tutorialLevels; j++)
                {
                    levelListCache.Remove(levelList[j]);
                }
                delta = tutorialLevels;
            }
            
            int levelIndex = (SaveManager.Instance.saveData.GetLevel() - delta) % levelListCache.Count;
            loadedLevel = Instantiate(levelListCache[levelIndex], playZone, true);
        }
    }
}