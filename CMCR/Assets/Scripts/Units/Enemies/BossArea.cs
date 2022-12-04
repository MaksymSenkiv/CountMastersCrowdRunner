using System;
using UnityEngine;

namespace CMCR 
{
    [RequireComponent(typeof(Collider))]
    public class BossArea : MonoBehaviour
    {
        public event Action AllyCollided;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Ally _)) {
                AllyCollided?.Invoke();
            }
        }
    }
}