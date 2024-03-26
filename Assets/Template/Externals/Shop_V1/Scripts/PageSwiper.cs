using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Template.Scripts;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Shop_V1.Scripts
{
    public class PageSwiper : MonoBehaviour , IEndDragHandler
    {
        public ShopOptions shopOptions;
        public RectTransform levelPagesRect;
        public GameObject previousPageChanger;
        public GameObject nextPageChanger;
        public int currentPage = 1;
        public List<GameObject> pagePoints;

        private void Start()
        {
            SetPageForSelectedSkin();
        }
        
        public void OnEndDrag(PointerEventData eventData)
        {
            if (MathF.Abs(eventData.position.x - eventData.pressPosition.x) > shopOptions.dragThreshHold)
            {
                if (eventData.position.x > eventData.pressPosition.x)
                {
                    PreviousPage();
                }
                else
                {
                    NextPage();
                }
            }
            else
            {
                MovePage();
            }
        }
        
        public void NextPage()
        {
            if (currentPage < shopOptions.maxPage)
            {
                currentPage++;
                MovePage();
            }
        }

        public void PreviousPage()
        {
            if (currentPage > 1)
            {
                currentPage--;
                MovePage();
            }
        }

        private void MovePage()
        {
            levelPagesRect.DOAnchorPosX((currentPage - 1) * shopOptions.pageStep, shopOptions.pageChangeSpeed)
                .SetEase(shopOptions.pageChangeEase);
            
            pagePoints.ActivateAtIndex(currentPage - 1);

            SetPageChangerEnabled();
            
            ShopManager.CallChangeShopPanelPage();
        }

        private void SetPageChangerEnabled()
        {
            previousPageChanger.SetActive(true);
            nextPageChanger.SetActive(true);
            
            switch (currentPage)
            {
                case 1:
                    previousPageChanger.SetActive(false);
                    break;
                case 3:
                    nextPageChanger.SetActive(false);
                    break;
            }
        }
        
        private void SetPageForSelectedSkin()
        {
            var currentSkin = SaveManager.Instance.saveData.currentSkin;
            
            var pageEndAmounts = shopOptions.rarityOptions.Select((option, index) =>
                shopOptions.rarityOptions.Take(index + 1).Sum(opt => opt.buttonAmount)).ToArray();
            
            currentPage = pageEndAmounts.TakeWhile(endAmount => currentSkin >= endAmount).Count() + 1;
            
            MovePage();
        }
    }
}