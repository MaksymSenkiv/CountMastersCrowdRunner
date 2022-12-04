using System;
using UnityEngine;

namespace CMCR
{
    [RequireComponent(typeof(Ally))]
    public class AllyCollision : MonoBehaviour
    {
        [SerializeField] private float _maxDistanceToRoad;
        
        private Ally _ally;

        public event Action ObstacleCollided;
        public event Action<Transform> EnemyGroupCollided;
        public event Action EnemyCollided;
        public event Action AllyFell;
        public event Action LastEnemyIsDefeated;
        public event Action<Transform> BossAreaCollided;
        public event Action<Boss> BossCollided;
        public event Action BossMovedAway;

        private void Awake()
        {
            _ally = GetComponent<Ally>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out Obstacle _)) {
                ObstacleCollided?.Invoke();
            }
            else if (collision.gameObject.TryGetComponent(out Enemy enemy) 
            && _ally.State != UnitStates.Dead 
            && enemy.State != UnitStates.Dead) 
            {
                enemy.Die(LastEnemyIsDefeatedCallback);
                
                EnemyCollided?.Invoke();
            }
            else if (collision.gameObject.TryGetComponent(out Boss boss)) {
                BossCollided?.Invoke(boss);
            }
        }

        private void LastEnemyIsDefeatedCallback()
        {
            LastEnemyIsDefeated?.Invoke();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out EnemyGroup enemyGroup)) {
                EnemyGroupCollided?.Invoke(enemyGroup.CenterTransform);
            }
            else if (other.TryGetComponent(out BossArea bossArea)) {
                BossAreaCollided?.Invoke(bossArea.transform);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (_ally.State == UnitStates.Attack && other.TryGetComponent(out Boss _)) {
                BossMovedAway?.Invoke();
            }
        }

        private void Update()
        {
            if(_ally.State == UnitStates.Run && !IsGrounded()) {
                AllyFell?.Invoke();
            }
        }

        private bool IsGrounded()
        {
            return Physics.CheckSphere(transform.position, _maxDistanceToRoad,
                1 << LayerMask.NameToLayer(LayerNames.Ground));
        }
    }
}