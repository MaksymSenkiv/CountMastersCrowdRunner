using System;
using UnityEngine;

namespace CMCR
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Stain : MonoBehaviour
    {
        public float StainHeight;

        private SpriteRenderer _spriteRenderer;
        
        public event Action<Stain> BecameInvisible;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void Init(Sprite sprite)
        {
            _spriteRenderer.sprite = sprite;
        }

        private void OnBecameInvisible()
        {
            BecameInvisible?.Invoke(this);
        }
    }
}