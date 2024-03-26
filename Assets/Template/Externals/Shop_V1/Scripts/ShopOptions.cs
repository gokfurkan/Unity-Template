using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Shop_V1.Scripts
{
    [CreateAssetMenu(fileName = "ShopOptions", menuName = "ScriptableObjects/ShopOptions")]
    public class ShopOptions : ScriptableObject
    {
        [Header("Shop")] 
        public float markControlDelay;
        public List<ShopRarityOptions> rarityOptions;
        
        [Header("Page Swiper")]
        public int maxPage;
        public float pageStep;
        public float dragThreshHold;
        public float pageChangeSpeed;
        public Ease pageChangeEase;
    }
}