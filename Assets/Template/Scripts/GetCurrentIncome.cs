using TMPro;
using UnityEngine;

namespace Template.Scripts
{
    public class GetCurrentIncome : MonoBehaviour
    {
        [SerializeField] private IncomeTextType incomeTextType;

        private void Start()
        {
            InitializeIncomeText();
        }

        private void InitializeIncomeText()
        {
            UIManager.Instance.SetCurrentIncomeText(GetComponent<TextMeshProUGUI>() , incomeTextType);
        }
    }
}