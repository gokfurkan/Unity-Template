using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Template.Scripts
{
    [DisallowMultipleComponent]
    public class ButtonClickController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
    {
        [SerializeField] private bool singleClick;
        [SerializeField] private bool callOnHold;
        [SerializeField] private bool canAnimate = true;
        public UnityEvent onClick;

        private bool _clickedOnce;
        private bool startAnimate;
        private bool animateUp;
        private bool firstDown;

        private Transform tr;
        private Vector3 currentScale;

        private void Start()
        {
            tr = transform;
        }

        private void OnEnable()
        {
            firstDown = false;
        }
    
        private void LateUpdate()
        {
            if (!startAnimate)
            {
                return;
            }

            if (!animateUp)
            {
                tr.localScale = Vector3.MoveTowards(tr.localScale, currentScale * 0.875f, Time.unscaledDeltaTime * 3);
            }
            else
            {
                tr.localScale = Vector3.MoveTowards(tr.localScale, currentScale, Time.unscaledDeltaTime * 3);
                if (tr.localScale == currentScale)
                {
                    animateUp = false;
                    startAnimate = false;
                }
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (callOnHold)
            {
                return;
            }

            CallFunction();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!firstDown)
            {
                currentScale = transform.localScale;
                firstDown = true;
            }

            if (canAnimate)
            {
                startAnimate = true;
            }
           
            animateUp = false;

            if (!callOnHold)
            {
                return;
            }

            CallFunction();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            animateUp = true;
        }

        private void CallFunction()
        {
            if (singleClick && !_clickedOnce)
            {
                _clickedOnce = true;
                onClick.Invoke();
            }
            else if (!singleClick)
            {
                onClick.Invoke();
            }
        }
    }
}