using UnityEngine;
using UnityEngine.Serialization;

namespace Template.Externals.DailyRewards_V1.Scripts
{
    [CreateAssetMenu(fileName = "DailyRewardOptions", menuName = "ScriptableObjects/DailyRewardOptions")]
    public class DailyRewardOptions : ScriptableObject
    {
        public float rewardLoopHours;
        public float controlRewardLeftHours;
        public float rewardSpawnUISize;
    }
}