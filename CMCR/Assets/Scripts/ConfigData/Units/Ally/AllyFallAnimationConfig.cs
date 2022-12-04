using DG.Tweening;
using UnityEngine;

namespace CMCR
{
    [CreateAssetMenu(fileName = "AllyFallAnimationConfig", menuName = "Configs/Ally/AllyFallAnimationConfig")]
    public class AllyFallAnimationConfig : ScriptableObject
    {
        public float Deep = 20;
        public float Duration = 1f;
        public Ease Ease = Ease.Linear;
    }
}