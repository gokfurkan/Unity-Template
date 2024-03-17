using System;
using System.IO;
using Template.Scripts.Scriptables;
using UnityEngine;

namespace Template.Scripts
{
    public class LogManager : Singleton<LogManager>
    {
        private static StreamWriter logStreamWriter;
        private GameSettings gameSettings;

        private void OnEnable()
        {
            Application.logMessageReceived += HandleLog;
        }

        private void OnDisable()
        {
            Application.logMessageReceived -= HandleLog;
        }

        protected override void Initialize()
        {
            base.Initialize();

            gameSettings = InfrastructureManager.Instance.gameSettings;
        }

        private void HandleLog(string logString, string stackTrace, LogType type)
        {
            LogLevel level = ConvertLogTypeToLogLevel(type);

            if ((gameSettings.logOptions.logLevelToSave & level) == level)
            {
                LogData logData = new LogData
                {
                    timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    level = level.ToString(),
                    message = logString,
                    stackTrace = stackTrace
                };
                
                SaveManager.Instance.saveData.logs.Add(logData);
                SaveManager.Instance.Save();
            }
        }
        
        private LogLevel ConvertLogTypeToLogLevel(LogType type)
        {
            switch (type)
            {
                case LogType.Error:
                    return LogLevel.Error;
                case LogType.Assert:
                    return LogLevel.Assert;
                case LogType.Warning:
                    return LogLevel.Warning;
                case LogType.Log:
                    return LogLevel.Log;
                case LogType.Exception:
                    return LogLevel.Exception;
                default:
                    return LogLevel.Log;
            }
        }
    }
    
    [Serializable]
    public class LogData
    {
        public string timestamp;
        public string level;
        public string message;
        public string stackTrace;
    }
}