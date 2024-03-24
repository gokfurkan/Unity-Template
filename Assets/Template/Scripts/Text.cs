using TMPro;
using UnityEngine;

namespace Template.Scripts
{
    public class Text : MonoBehaviour
    {
        private void Start()
        {
            TextManager.Instance.SetFont(GetComponent<TextMeshProUGUI>());
        }
    }
}