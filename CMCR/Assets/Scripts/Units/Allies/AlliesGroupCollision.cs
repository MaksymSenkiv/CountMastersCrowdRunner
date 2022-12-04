using System;
using UnityEngine;

namespace CMCR
{
    public class AlliesGroupCollision: MonoBehaviour
    {
        public event Action ObstacleAvoided;
        public event Action SpringboardCollided;
        public event Action RoadCollided;

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Obstacle _))
            {
                ObstacleAvoided?.Invoke();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Springboard _))
            {
                SpringboardCollided?.Invoke();
            }
            else if (other.TryGetComponent(out Road _))
            {
                RoadCollided?.Invoke();
            }
        }
    }
}