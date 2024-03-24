using UnityEngine;

namespace Template.Scripts
{
    public class Development : Singleton<Development>
    {
#if UNITY_EDITOR
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                Debug.Break();
            }
            
            if (Input.GetKeyDown(KeyCode.G))
            {
                BusSystem.CallLevelEnd(true);
            }
            
            if (Input.GetKeyDown(KeyCode.H))
            {
                BusSystem.CallLevelEnd(false);
            }
            
            if (Input.GetKeyDown(KeyCode.R))
            {
                BusSystem.CallLevelLoad();
            }
            
            if (Input.GetKeyDown(KeyCode.M))
            {
                EconomyManager.Instance.AddMoneys(5000);
            }
            
            if (Input.GetKeyDown(KeyCode.F))
            {
                TransitionManager.Instance.StartTransition(TransitionType.Door);
            }
        }
#endif
    }
}