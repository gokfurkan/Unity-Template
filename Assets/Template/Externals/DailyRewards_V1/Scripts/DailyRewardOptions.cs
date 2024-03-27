using UnityEngine;

namespace Template.Externals.DailyRewards_V1.Scripts
{
    [CreateAssetMenu(fileName = "DailyRewardOptions", menuName = "ScriptableObjects/DailyRewardOptions")]
    public class DailyRewardOptions : ScriptableObject
    {
        public float perCheckClaimRewardLeft;
        public float rewardLoopHours;
        public float rewardSpawnUISize;
    }
}