using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CMCR
{
    public class Boss : MonoBehaviour
    {
        [SerializeField] private UnitStates _state;
        [SerializeField] private Image _healthBar;
        [SerializeField] private BossArea _bossArea;

        [SerializeField] private uint _maxHealth = 10;
        [SerializeField] private uint _currentHealth;
        [SerializeField] private uint _damage;
        [SerializeField] private float _attackDistance;

        private Animator _animator;
        private AlliesGroup _alliesGroup;
        private CameraFollower _cameraFollower;

        public event Action Died;

        public UnitStates State => _state;

        [Inject]
        private void Construct(AlliesGroup alliesGroup)
        {
            _alliesGroup = alliesGroup;
        }

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _cameraFollower = Camera.main.GetComponent<CameraFollower>();
        }

        private void OnEnable()
        {
            _alliesGroup.AllAlliesDied += Celebrate;
        }

        private void OnDisable()
        {
            _alliesGroup.AllAlliesDied -= Celebrate;
        }

        private void Start()
        {
            _currentHealth = _maxHealth;
            _bossArea.AllyCollided += StartAttack;
        }

        private void OnDestroy()
        {
            _bossArea.AllyCollided -= StartAttack;
        }

        private void Celebrate()
        {
            _state = UnitStates.Idle;
            _animator.Play(AnimationClips.Dance);
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _attackDistance);
        }

        private void StartAttack()
        {
            if (_state == UnitStates.Idle)
            {
                _state = UnitStates.Attack;
                _animator.Play(AnimationClips.BossAttack);
            }
        }
        
        private void Attack() // used by Animator
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, _attackDistance);

            uint deadUnits = 0; 
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.gameObject.TryGetComponent(out Ally ally) && deadUnits < _damage)
                {
                    deadUnits++;
                    ally.AttackedByBoss(transform.position);
                }
            }
            
            _cameraFollower.Shake();
        }

        public void GetDamage()
        {
            _currentHealth--;
            _healthBar.fillAmount = (float) _currentHealth / _maxHealth;
            if (_currentHealth == 0)
            {
                Die();
            }
        }

        private void Die()
        {
            _state = UnitStates.Dead;
            Died?.Invoke();
            _animator.Play(AnimationClips.BossDie);
        }
    }
}