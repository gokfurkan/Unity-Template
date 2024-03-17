using UnityEditor;
#if UNITY_EDITOR
using UnityEditor.SceneManagement;
using UnityEngine.Windows;
#endif
using UnityEngine;

namespace Template.Scripts
{
    public class WindowEditor : MonoBehaviour
    {
#if UNITY_EDITOR
        [MenuItem("Template/Save/Delete")]
        private static void DeleteSave()
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

            string GetSavePath()
            {
                return Application.persistentDataPath + "/saveData.json";
            }
        }
        
        [MenuItem("Template/Scenes/Load")]
        private static void SwitchToLoad()
        {
            string sceneName = "Load";
            
            EditorSceneManager.OpenScene("Assets/Template/Scenes/" + sceneName + ".unity");
        }
        
        [MenuItem("Template/Scenes/Game")]
        private static void SwitchToGame()
        {
            string sceneName = "Game";
            EditorSceneManager.OpenScene("Assets/Template/Scenes/" + sceneName + ".unity");
        }
#endif
    }
}