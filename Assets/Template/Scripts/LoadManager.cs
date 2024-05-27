using System.Collections;
using Template.Scripts.SO;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Template.Scripts
{
    public class LoadManager : Singleton<LoadManager>
    {
        [SerializeField] private Animator loadAnimator;

        private LoadOptions loadOptions;
        
        private static readonly int StartSequence = Animator.StringToHash("StartSequence");

        protected override void Initialize()
        {
            base.Initialize();

            InitializeLoad();
        }

        private void InitializeLoad()
        {
            loadOptions = InfrastructureManager.Instance.gameSettings.loadOptions;
            StartCoroutine(RunLoadingSequence());
        }

        private IEnumerator RunLoadingSequence()
        {
            loadAnimator.speed = 1 / loadOptions.loadDuration;
            loadAnimator.SetTrigger(StartSequence);
            
            yield return new WaitForSeconds(loadOptions.loadDuration);

            SceneManager.LoadScene((int)SceneType.Game);
        }
    }
}