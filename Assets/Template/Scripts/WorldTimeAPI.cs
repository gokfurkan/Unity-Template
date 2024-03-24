using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Template.Scripts
{
    public class WorldTimeAPI : Singleton<WorldTimeAPI>
    {
        private const string API_URL = "http://worldtimeapi.org/api/ip";
        private DateTime currentDateTime;

        protected override void Initialize()
        {
            base.Initialize();
            
            StartCoroutine(GetRealDateTimeFromAPI());
        }

        private IEnumerator GetRealDateTimeFromAPI()
        {
            using UnityWebRequest webRequest = UnityWebRequest.Get(API_URL);
            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Failed to fetch real-time data from the API.");
                yield break;
            }

            string jsonText = webRequest.downloadHandler.text;
            TimeData timeData = JsonUtility.FromJson<TimeData>(jsonText);

            currentDateTime = DateTime.Parse(timeData.datetime);
        }

        public DateTime GetCurrentDateTime()
        {
            return currentDateTime.AddSeconds(Time.realtimeSinceStartup);
        }

        [Serializable]
        private class TimeData
        {
            public string datetime;
        }
    }
}