using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace Template.Scripts
{
    public class DevManager : MonoBehaviour
    {
        [SerializeField] private InputField ipf;

        public void ChangeLevel()
        {
            SaveManager.Instance.saveData.level = int.Parse(ipf.text);
            SaveManager.Instance.Save();
            
            BusSystem.CallLevelLoad();
        }

        public void ResetSaveData()
        {
            SaveManager.Instance.Delete();
            BusSystem.CallLevelLoad();
        }
    }
}