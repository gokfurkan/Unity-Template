using System.IO;
using UnityEngine;

namespace Template.Scripts
{
    public class SaveManager : Singleton<SaveManager>
    {
        public SaveData saveData;

        protected override void Initialize()
        {
            base.Initialize();
            
            Load();
        }

        public void Save()
        {
            string jsonData = JsonUtility.ToJson(saveData);
            File.WriteAllText(GetSavePath(), jsonData);

            Debug.Log("Game saved!");
        }

        public void Load()
        {
            if (File.Exists(GetSavePath()))
            {
                string jsonData = File.ReadAllText(GetSavePath());
                saveData = JsonUtility.FromJson<SaveData>(jsonData);

                Debug.Log("Save loaded!");
            }
            else
            {
                saveData = new SaveData();
                Debug.Log("No saved game state found.");
            }
        }

        public void Delete()
        {
            if (File.Exists(GetSavePath()))
            {
                File.Delete(GetSavePath());
                Debug.Log("Save data deleted!");
            }
            else
            {
                Debug.Log("No Save data to delete found");
            }
        }

        private string GetSavePath()
        {
            return Application.persistentDataPath + "/saveData.json";
        }
    }
}