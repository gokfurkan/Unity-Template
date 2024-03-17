using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Template.Scripts.Scriptables
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "ScriptableObjects/GameSettings", order = 0)]
    public class GameSettings : ScriptableObject
    {
        public LoadOptions loadOptions;
        public GamePlayOptions gamePlayOptions;
        public EconomyOptions economyOptions;
        public UIOptions uiOptions;
        public LogOptions logOptions;
    }

    [Serializable]
    public class LoadOptions
    {
        public SceneType nextSceneAfterLoad;
        public List<LoadFillOptions> loadFillOption;
    }

    [Serializable]
    public class GamePlayOptions
    {
        public bool vSyncEnabled;
        public int targetFPS;
    }

    [Serializable]
    public class EconomyOptions
    {
        public bool useMoneyAnimation;
        [ShowIf(nameof(useMoneyAnimation))]
        public float moneyAnimationDuration;

        [Space(10)] 
        public int winIncome;
        public int loseIncome;
        
        [Space(10)] 
        public int spawnMoneyAmount;
    }

    [Serializable]
    public class UIOptions
    {
        public float winPanelDelay;
        public float losePanelDelay;
        public float endContinueDelay;
        
        [Space(10)]
        public string levelText;

        [Space(10)]
        public bool hasEndPanelLevel;
        public string levelCompletedText;
        public string levelFailedText;

        [Space(10)] 
        public TMP_FontAsset  textFont;
    }

    [Serializable]
    public class LogOptions
    {
        public LogLevel logLevelToSave = LogLevel.All;
    }
}