using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using Random = System.Random;

namespace CMCR
{
    [RequireComponent(typeof(AllyCollision)), RequireComponent(typeof(Collider))]
    public class Ally : Unit
    {
        private static readonly Random _random = new();
        
        [SerializeField] private AllyAnimationConfig _animationConfig;
        [SerializeField] private float _attackBossCooldown;

        private AllyCollision _collision;
        private Collider _collider;

        public event Action<Ally> Died;
        public event Action<Ally> Destroyed;

        private new void Awake()
        {
            base.Awake();

            _collision = GetComponent<AllyCollision>();
            _collider = GetComponent<Collider>();
        }

        private void OnEnable()
        {
            _collision.ObstacleCollided += Die;
            _collision.AllyFell += Fall;
            _collision.EnemyCollided += Die;
            _collision.BossCollided += AttackBoss;
            _collision.BossMovedAway += RunToBoss;
        }

        private void OnDisable()
        {
            _collision.ObstacleCollided -= Die;
            _collision.AllyFell -= Fall;
            _collision.EnemyCollided -= Die;
            _collision.BossCollided -= AttackBoss;
            _collision.BossMovedAway -= RunToBoss;
        }

        private void AttackBoss(Boss boss)
        {
            _state = UnitStates.Attack;
            _animator.Play(AnimationClips.AllyAttack);
            StartCoroutine(AttackBossRoutine(boss));
        }

        private IEnumerator AttackBossRoutine(Boss boss)
        {
            while (boss.State != UnitStates.Dead && State != UnitStates.Dead)
            {
                transform.LookAt(boss.transform);
                boss.GetDamage();
                yield return new WaitForSeconds(_attackBossCooldown);
            }
        }

        private void RunToBoss()
        {
            _state = UnitStates.Run;
        }

        private void Fall()
        {
            _state = UnitStates.Fall;
            _collider.isTrigger = true;
            FallingEffect();
            Died?.Invoke(this);
            transform
                .DOMoveY(-_animationConfig.Fall.Deep,
                    _animationConfig.Fall.Duration)
                .SetEase(_animationConfig.Fall.Ease)
                .OnComplete(() => Destroyed?.Invoke(this));
        }

        public void Jump()
        {
            _state = UnitStates.Jump;
            transform
                .DOLocalJump(transform.localPosition, _animationConfig.Jump.Height, 1,
                    _animationConfig.Jump.Duration)
                .SetEase(_animationConfig.Jump.Ease)
                .OnComplete(() => _state = UnitStates.Run);
        }

        private void Die()
        {
            _state = UnitStates.Dead;
            Died?.Invoke(this);
            _animator.Play(AnimationClips.Idle);
            Destroy(true);
        }

        private void Destroy(bool spawnStain)
        {
            PlayDestroyEffect(UnitType.Ally, spawnStain);
            Destroyed?.Invoke(this);
        }

        public void AttackedByBoss(Vector3 bossPosition)
        {
            _state = UnitStates.Dead;
            _collider.enabled = false;
            Died?.Invoke(this);
            transform
                .DOJump(transform.position + (transform.position - bossPosition).normalized * _animationConfig.ShockWave.Distance, 
                    _animationConfig.ShockWave.Height, 1, _animationConfig.ShockWave.Duration)
                .SetEase(_animationConfig.ShockWave.Ease)
                .OnComplete(() => Destroy(false));
            FallingEffect();
        }

        private void FallingEffect()
        {
            _animator.Play(AnimationClips.AllyFall);
            transform
                .DORotate(new Vector3(_random.Next(0, 180), _random.Next(0, 180), _random.Next(0, 180)),
                    _animationConfig.ShockWave.Duration)
                .SetEase(_animationConfig.ShockWave.Ease);
        }
    }
}