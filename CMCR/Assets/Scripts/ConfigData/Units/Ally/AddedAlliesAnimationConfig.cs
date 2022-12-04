using System;
using DG.Tweening;
using UnityEngine;

namespace CMCR
{
    [CreateAssetMenu(fileName = "AddedAlliesAnimationConfig", menuName = "Configs/Ally/AddedAlliesAnimationConfig")]
    public class AddedAlliesAnimationConfig : ScriptableObject
    {
        [NonSerialized]
        public float StartPositionY;
        public float MovingHeight = 5;
        public float Duration = 1f;

        public Ease MoveEase = Ease.Linear;
        public Ease FadeEase = Ease.InQuint;
    }
}