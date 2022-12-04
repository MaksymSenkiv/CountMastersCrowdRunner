using DG.Tweening;
using UnityEngine;

namespace CMCR
{
    [CreateAssetMenu(fileName = "AllyShockWaveAnimationConfig", menuName = "Configs/Ally/AllyShockWaveAnimationConfig")]
    public class AllyShockWaveAnimationConfig : ScriptableObject
    {
        public float Distance = 1.5f;
        public float Height = 1.5f;
        public float Duration = 1;
        public Ease Ease = Ease.Linear;
    }
}