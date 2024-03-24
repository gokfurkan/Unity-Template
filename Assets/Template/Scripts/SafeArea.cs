using UnityEngine;

namespace Template.Scripts
{
    [RequireComponent(typeof(RectTransform))]
    public class SafeArea : MonoBehaviour
    {
        private RectTransform panel;
        private Rect safeArea = new Rect(0, 0, 0, 0);

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            panel = GetComponent<RectTransform>();
            
            panel.offsetMin = Vector2.zero;
            panel.offsetMax = Vector2.zero;
            
            ApplySafeArea();
        }
        
        private void ApplySafeArea()
        {
            safeArea = Screen.safeArea;
            
            Vector2 anchorMin = safeArea.position;
            Vector2 anchorMax = safeArea.position + safeArea.size;
            
            anchorMin.x /= Screen.width;
            anchorMin.y /= Screen.height;
            anchorMax.x /= Screen.width;
            anchorMax.y /= Screen.height;
            
            panel.anchorMin = anchorMin;
            panel.anchorMax = anchorMax;
        }
    }
}