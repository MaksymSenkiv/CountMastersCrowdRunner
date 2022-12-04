using DG.Tweening;
using UnityEngine;

namespace CMCR
{
    [CreateAssetMenu(fileName = "EnemyAuraDisappearAnimationConfig", menuName = "Configs/Enemy/EnemyAuraDisappearAnimationConfig")]
    public class EnemyAuraDisappearAnimationConfig : ScriptableObject
    {
        public float EndScale;
        public float Duration;
        public Ease Ease;
    }
}