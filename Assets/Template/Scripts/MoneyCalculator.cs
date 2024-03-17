using System;
using System.Collections.Generic;
using UnityEngine;

namespace Template.Scripts
{
    public class MoneyCalculator : MonoBehaviour
    {
        private static readonly int charA = Convert.ToInt32('a');
        private static readonly Dictionary<int, string> units = new Dictionary<int, string> {
        
            {0, ""},
            {1, "K"},
            {2, "M"},
            {3, "B"},
            {4, "T"},
            {5, "AA"},
            {6, "AB"},
            {7, "AC"},
            {8, "AD"},
            {9, "AE"},
            {10, "AF"},
            {11, "AG"},
            {12, "BA"},
            {13, "BB"},
            {14, "BC"},
            {15, "BD"},
            {16, "BE"},
            {17, "BF"},
            {18, "BG"},
            {19, "CA"},
            {20, "CB"},
            {21, "CC"},
            {22, "CD"},
            {23, "CE"},
            {24, "CF"},
            {25, "CG"}
        };
    
        public static string NumberToStringFormatter(float value)
        {
            if (value < 1d)
            {
                return "0";
            }
 
            var n = (int) Mathf.Log(value, 1000);
            var m = value / Mathf.Pow(1000, n);
            var unit = "";
 
            if (n < units.Count)
            {
                unit = units[n];
            }
            else
            {
                var unitInt = n - units.Count;
                var secondUnit = unitInt % 26;
                var firstUnit = unitInt / 26;           
                unit = Convert.ToChar(firstUnit + charA).ToString() + Convert.ToChar(secondUnit + charA).ToString();
            }
 
            //Math.Floor(m * 100) / 100)  fixes rounding errors 

           string calculateValue;
            
            if (value >= 1000)
            {
                calculateValue = $"{m:F1}K";
                calculateValue = ExtensionsMethods.ConvertToDotDecimal(calculateValue);
            }
            else
            {
                calculateValue = Mathf.FloorToInt(m) + unit;
            }
            
            return calculateValue;
        }

        public static int UpgradeCostCalculator(float level)
        {
            var upgradeCost = 20 * ( (Mathf.Pow(1.07f, 10) - Mathf.Pow(1.07f, 10 + level)) / (1 - 1.07f) );
            var intUpgradeCost = Mathf.CeilToInt(upgradeCost);
            return intUpgradeCost;
        }
    }
}