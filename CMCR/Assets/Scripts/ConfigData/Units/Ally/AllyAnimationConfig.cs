using UnityEngine;

namespace CMCR
{
    [CreateAssetMenu(fileName = "AllyAnimationConfig", menuName = "Configs/Ally/AllyAnimationConfig", order = 0)]
    public class AllyAnimationConfig : ScriptableObject
    {
        public AllyFallAnimationConfig Fall;
        public AllyJumpAnimationConfig Jump;
        public AllyShockWaveAnimationConfig ShockWave;
    }
}