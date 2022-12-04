using UnityEngine;

namespace CMCR
{
    [RequireComponent(typeof(Animator), typeof(Rigidbody))]
    public abstract class Unit : MonoBehaviour
    {
        [SerializeField] protected UnitStates _state;

        protected Animator _animator;
        
        private DeathParticlesPool _deathParticlesPool;
        private StainsSpawner _stainsSpawner;
        private Rigidbody _rigidbody;

        public UnitsGroup Group { get; private set; }

        public UnitStates State => _state;
        
        protected void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _animator = GetComponent<Animator>();
        }

        public void Init(UnitsGroup unitsGroup, DeathParticlesPool deathParticlesPool, StainsSpawner stainsSpawner)
        {
            Group = unitsGroup;
            _deathParticlesPool = deathParticlesPool;
            _stainsSpawner = stainsSpawner;
        }

        public void Move(Vector3 direction, float speed, float deltaTime) 
        {
            _rigidbody.velocity = direction * (deltaTime * speed);
        }
        
        public void StartRunning()
        {
            _state = UnitStates.Run;
            _animator.Play(AnimationClips.Run);
        }

        public void Celebrate()
        {
            _state = UnitStates.Idle;
            _animator.Play(AnimationClips.Dance);
        }
        
        protected void PlayDestroyEffect(UnitType type, bool spawnStain = true)
        {
            ParticleSystem particle = _deathParticlesPool.Pool.Get();
            particle.transform.position = transform.position + Vector3.up * transform.localScale.y / 2;
            particle.Play();
            if (spawnStain) {
                _stainsSpawner.SpawnStain(transform.position, type);
            }
        }
    }
}