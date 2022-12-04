using DG.Tweening;
using UnityEngine;

namespace CMCR
{
    [CreateAssetMenu(fileName = "AllyJumpAnimationConfig", menuName = "Configs/Ally/AllyJumpAnimationConfig")]
    public class AllyJumpAnimationConfig : ScriptableObject
    {
        public float Height = 3;
        public float Duration = 2;
        public Ease Ease = Ease.Linear;
    }
}