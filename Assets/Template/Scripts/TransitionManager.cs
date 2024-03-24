using UnityEngine;

namespace Template.Scripts
{
    public class TransitionManager : PersistentSingleton<TransitionManager>
    {
        [SerializeField] private Animator fadeAnimator;
        [SerializeField] private Animator doorAnimator;

        [Space(10)]
        [SerializeField] private GameObject fadePanel;
        [SerializeField] private GameObject doorPanel;
        
        private static readonly int StartSequence = Animator.StringToHash("StartSequence");

        protected override void Initialize()
        {
            base.Initialize();
            
            InitPanels();
        }

        private void InitPanels()
        {
            DisableAllTransitions();
        }
        
        public void StartTransition(TransitionType type)
        {
            DisableAllTransitions();
            
            switch (type)
            {
                case TransitionType.Fade:
                    fadePanel.SetActive(true);
                    fadeAnimator.SetTrigger(StartSequence);
                    
                    break;
                case TransitionType.Door:
                    doorPanel.SetActive(true);
                    doorAnimator.SetTrigger(StartSequence);
                    break;
            }
        }

        private void DisableAllTransitions()
        {
            fadePanel.SetActive(false);
            doorPanel.SetActive(false);
        }
    }
}