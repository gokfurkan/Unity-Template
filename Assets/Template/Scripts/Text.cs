using TMPro;
using UnityEngine;

namespace Template.Scripts
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class Text : MonoBehaviour
    {
        private void Start()
        {
            TextManager.Instance.SetFont(GetComponent<TextMeshProUGUI>());
        }
    }
}