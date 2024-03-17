using System;
using TMPro;
using UnityEngine;

namespace Template.Scripts
{
    public class Text : MonoBehaviour
    {
        private void Start()
        {
            gameObject.GetComponent<TextMeshProUGUI>().font = InfrastructureManager.Instance.gameSettings.uiOptions.textFont;
        }
    }
}