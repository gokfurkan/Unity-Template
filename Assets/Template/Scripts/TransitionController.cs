using UnityEngine;

namespace Template.Scripts
{
    public class TransitionController : PersistentSingleton<TransitionController>
    {
        public Animator fadeAnimator;
        public Animator doorAnimator;

        [Space(10)]
        public GameObject fadePanel;
        public GameObject doorPanel;
        
        private static readonly int StartSequence = Animator.StringToHash("StartSequence");

        protected override void Initialize()
        {
            base.Initialize();
            
            InitPanels();
        }

        private void InitPanels()
        {
            fadePanel.SetActive(false);
            doorPanel.SetActive(false);
        }
        public void StartTransition(TransitionType type)
        {
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
    }
}