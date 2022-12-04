using UnityEngine;
using Zenject;

namespace CMCR
{
    public class UnitsFactory : MonoBehaviour
    {
        [SerializeField] private Enemy _enemyPrefab;
        
        private AlliesPool _alliesPool;
        private GameFactory _gameFactory;

        [Inject]
        private void Construct(AlliesPool alliesPool, GameFactory gameFactory)
        {
            _alliesPool = alliesPool;
            _gameFactory = gameFactory;
        }

        public Unit GetUnit(UnitType type)
        {
            switch (type)
            {
                case UnitType.Ally: {
                    return _alliesPool.Pool.Get();
                }
                case UnitType.Enemy: {
                    return _gameFactory.CreateEnemy(_enemyPrefab, transform);
                }
                default: {
                    return null;
                }
            }
        }
    }
}