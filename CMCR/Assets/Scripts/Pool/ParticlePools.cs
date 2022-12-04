using UnityEngine;
using Zenject;

namespace CMCR
{
    public class ParticlePools : MonoBehaviour
    {
        public DeathParticlesPool AllyParticlesPool;
        public DeathParticlesPool EnemyParticlesPool;
        
        private GameFactory _gameFactory;

        [Inject]
        private void Construct(GameFactory gameFactory)
        {
            _gameFactory = gameFactory;
        }
        
        private void Awake()
        {
            AllyParticlesPool.Init(_gameFactory);
            EnemyParticlesPool.Init(_gameFactory);
        }
    }
}