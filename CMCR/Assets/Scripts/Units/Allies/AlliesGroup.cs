using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Zenject;

namespace CMCR
{
    [RequireComponent(typeof(AlliesGroupBounds), typeof(AlliesGroupMovement))]
    public class AlliesGroup : UnitsGroup
    {
        [SerializeField] private TMP_Text _addedAlliesText;
        [SerializeField] private AddedAlliesAnimationConfig _addedAlliesAnimationConfig;
        [SerializeField] private AlliesGroupCollision _collision;
        
        private AlliesGroupBounds _bounds;
        private AlliesGroupMovement _movement;
        private Level _level;
        private Boss _boss;

        public event Action AllAlliesDied;
        public event Action AttackStopped;

        [Inject]
        private void Construct(Level level, Boss boss, ParticlePools particlePools)
        {
            _level = level;
            _boss = boss;
            DeathParticlesPool = particlePools.AllyParticlesPool;
        }

        private new void Awake()
        {
            base.Awake();
            
            _bounds = GetComponent<AlliesGroupBounds>();
            _movement = GetComponent<AlliesGroupMovement>();
        }
        
        private new void Start()
        {
            base.Start();
            
            SetUIRotation(_addedAlliesText.transform);
        }

        private void OnEnable()
        {
            _level.LevelStarted += Run;
            _level.LevelStarted += UnitsStartRunning;
            _boss.Died += Celebrate;
            _collision.SpringboardCollided += Jump;
            _collision.RoadCollided += Land;
        }

        private void OnDisable()
        {
            _level.LevelStarted -= Run;
            _level.LevelStarted -= UnitsStartRunning;
            _boss.Died -= Celebrate;
            _collision.SpringboardCollided -= Jump;
            _collision.RoadCollided -= Land;
        }

        protected override void SpawnUnits(int newUnitsCount, UnitType type, UnitStates startState = UnitStates.Idle)
        {
            base.SpawnUnits(newUnitsCount, type, startState);
            StartCoroutine(CalculateUnitsBoundsRoutine());
        }

        private IEnumerator CalculateUnitsBoundsRoutine()
        {
            Bounds oldBounds;
            Vector3 oldCenter;
            do
            {
                oldBounds = _bounds.CalculateUnitsBounds();
                oldCenter = CenterTransform.position;
                yield return null;
            } while (oldBounds.size != _bounds.CalculateUnitsBounds().size &&
                     oldCenter != CenterTransform.position);
        }

        public void IncreaseAlliesAmount(GateData gateData)
        {
            int addedAlliesCount = gateData.IncreaseType switch
            {
                IncreaseType.Add => gateData.IncreaseValue,
                IncreaseType.Multiply => (gateData.IncreaseValue - 1) * Units.Count,
                _ => 0
            };

            NewAlliesAmountAnimate(addedAlliesCount);
            SpawnUnits(addedAlliesCount, UnitType.Ally, UnitStates.Run);
        }

        private void NewAlliesAmountAnimate(int newAlliesAmount)
        {
            _addedAlliesText.text = '+' + newAlliesAmount.ToString();
            _addedAlliesText.transform.DOComplete();
            _addedAlliesText.DOComplete();
            _addedAlliesAnimationConfig.StartPositionY = _addedAlliesText.transform.position.y;
            _addedAlliesText.transform.DOMoveY(
                    _addedAlliesText.transform.position.y + _addedAlliesAnimationConfig.MovingHeight,
                    _addedAlliesAnimationConfig.Duration)
                .SetEase(_addedAlliesAnimationConfig.MoveEase)
                .OnComplete(() => _addedAlliesText.transform.localPosition = Vector3.up * _addedAlliesAnimationConfig.StartPositionY);
            
            _addedAlliesText.alpha = 1;
            _addedAlliesText.DOFade(0, _addedAlliesAnimationConfig.Duration)
                .SetEase(_addedAlliesAnimationConfig.FadeEase);
        }

        protected override void AddUnit(Unit unit)
        {
            base.AddUnit(unit);
            Ally ally = unit.GetComponent<Ally>();
            SubscribeAlly(ally);
            _bounds.CalculateUnitsBounds();
        }

        private void SubscribeAlly(Ally ally)
        {
            ally.Died += OnAllyDied;
            AllyCollision allyCollision = ally.GetComponent<AllyCollision>();
            allyCollision.EnemyGroupCollided += Attack;
            allyCollision.BossAreaCollided += Attack;
            allyCollision.LastEnemyIsDefeated += EndAttack;
        }

        private void OnAllyDied(Ally ally)
        {
            Units.Remove(ally);
            if (Units.Count == 0) {
                Die();
            }
            UnSubscribeAlly(ally);
            ChangeUnitsAmountText();
            _bounds.CalculateUnitsBounds();
        }

        private void Die()
        {
            State = UnitsGroupState.Dead;
            DisableUI();
            AllAlliesDied?.Invoke();
        }

        private void UnSubscribeAlly(Ally ally)
        {
            ally.Died -= OnAllyDied;
            AllyCollision allyCollision = ally.GetComponent<AllyCollision>();
            allyCollision.EnemyGroupCollided -= Attack;
            allyCollision.BossAreaCollided -= Attack;
            allyCollision.LastEnemyIsDefeated -= EndAttack;
        }

        private void Attack(Transform target)
        {
            State = UnitsGroupState.Attack;
            _movement.SetTarget(target);
        }

        private void EndAttack()
        {
            State = UnitsGroupState.Run;
            AttackStopped?.Invoke();
        }

        private void Land()
        {
            if (State == UnitsGroupState.Jump) {
                Run();
            }
        }

        private void Run() 
        {
            State = UnitsGroupState.Run;
        }

        private void Jump()
        {
            State = UnitsGroupState.Jump;
        }
    }
}