using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Template.Scripts
{
    [DisallowMultipleComponent]
    public class ButtonClickController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
    {
        [SerializeField] private bool allowSingleClick = true;
        [SerializeField] private bool triggerOnHold = false;
        [SerializeField] private bool enableAnimation = true;
        [SerializeField] private float pressedScaleMultiplier = 0.875f;
        [SerializeField] private float animationDuration = 0.2f;
        public UnityEvent onClick;

        private bool clickedOnce;
        private bool isPointerDown;

        private Transform buttonTransform;
        private Vector3 initialScale;
        
        private void Start()
        {
            buttonTransform = transform;
            initialScale = buttonTransform.localScale;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (triggerOnHold || (isPointerDown && enableAnimation))
                return;

            InvokeButtonClick();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            isPointerDown = true;
            if (enableAnimation)
                StartPressAnimation();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            isPointerDown = false;
            if (enableAnimation)
                EndPressAnimation();
        }

        private void StartPressAnimation()
        {
            buttonTransform.DOScale(initialScale * pressedScaleMultiplier, animationDuration)
                .SetEase(Ease.OutQuad);
        }

        private void EndPressAnimation()
        {
            buttonTransform.DOScale(initialScale, animationDuration)
                .SetEase(Ease.OutQuad);
        }

        private void InvokeButtonClick()
        {
            if (allowSingleClick && !clickedOnce)
            {
                clickedOnce = true;
                onClick.Invoke();
            }
            else if (!allowSingleClick)
            {
                onClick.Invoke();
            }
            
            clickedOnce = false;
        }
    }
}