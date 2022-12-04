using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace CMCR
{
    public class EnemyGroup : UnitsGroup
    {
        [SerializeField] private UnitsGroupMovementConfig _movementConfig;
        [SerializeField] private SpriteRenderer _aura;
        [SerializeField] private EnemyAuraDisappearAnimationConfig _auraDisappearAnimationConfig;

        private Collider _collider;
        private Transform _attackTargetPosition;
        private AlliesGroup _alliesGroup;

        [Inject]
        private void Construct(AlliesGroup alliesGroup, ParticlePools particlePools)
        {
            _alliesGroup = alliesGroup;
            DeathParticlesPool = particlePools.EnemyParticlesPool;
        }

        private new void Awake()
        {
            base.Awake();
            
            _collider = GetComponent<Collider>();
        }

        private void OnEnable()
        {
            _alliesGroup.AllAlliesDied += Celebrate;
        }

        private void OnDisable()
        {
            _alliesGroup.AllAlliesDied -= Celebrate;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Ally ally))
            {
                State = UnitsGroupState.Attack;
                _attackTargetPosition = ally.Group.CenterTransform;
                UnitsStartRunning();
            }
        }

        private void FixedUpdate()
        {
            if (State == UnitsGroupState.Attack) {
                foreach (var unit in Units) 
                {
                    unit.Move((_attackTargetPosition.position - unit.transform.position).normalized, 
                        _movementConfig.UnitSpeed, Time.fixedDeltaTime);
                }
            }
        }

        protected override void AddUnit(Unit enemy)
        {
            base.AddUnit(enemy);
            enemy.GetComponent<Enemy>().Died += EnemyDied;
        }

        private void EnemyDied(Enemy enemy, UnityAction allEnemiesDiedCallback)
        {
            Units.Remove(enemy);
            enemy.Died -= EnemyDied;
            ChangeUnitsAmountText();
            if (Units.Count == 0)
            {
                allEnemiesDiedCallback?.Invoke();
                Die();
            }
        }

        private void Die()
        {
            _collider.enabled = false;
            DisableUI();
            _aura.transform.DOScale(_auraDisappearAnimationConfig.EndScale, _auraDisappearAnimationConfig.Duration)
                .SetEase(_auraDisappearAnimationConfig.Ease)
                .OnComplete(() => { gameObject.SetActive(false); });
            _aura.DOFade(0f, _auraDisappearAnimationConfig.Duration)
                .SetEase(_auraDisappearAnimationConfig.Ease);
        }
    }
}