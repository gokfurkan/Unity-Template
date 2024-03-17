using System;
using System.Collections;
using Template.Scripts.Scriptables;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Template.Scripts
{
    public class LoadScreenController : MonoBehaviour
    {
        [SerializeField] private GameSettings gameSettings;
        [SerializeField] private Image loadingBar;
        
        private int fillOptionIndex;
        private float fillAmount = 0f;

        private void Start()
        {
            StartCoroutine(RunLoadingStages());
        }

        private IEnumerator RunLoadingStages()
        {
            for (int i = 0; i < gameSettings.loadOptions.loadFillOption.Count; i++)
            {
                yield return FillBarToValue(gameSettings.loadOptions.loadFillOption[i].stageAmount, gameSettings.loadOptions.loadFillOption[i].stageDuration);
                yield return new WaitForSeconds(gameSettings.loadOptions.loadFillOption[i].nextStageDelay);
            }

            SceneManager.LoadScene((int)gameSettings.loadOptions.nextSceneAfterLoad);
        }

        private IEnumerator FillBarToValue(float targetValue, float duration)
        {
            float elapsedTime = 0f;
            float startValue = fillAmount;

            while (elapsedTime < duration)
            {
                fillAmount = Mathf.Lerp(startValue, targetValue, elapsedTime / duration);
                loadingBar.fillAmount = fillAmount;

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            fillAmount = targetValue;
            loadingBar.fillAmount = fillAmount;
        }
    }

    [Serializable]
    public class LoadFillOptions
    {
        public float stageAmount;
        public float stageDuration;
        public float nextStageDelay;
    }
}