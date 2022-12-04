using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace CMCR
{
    public class StainsSpawner : MonoBehaviour
    {
        [SerializeField] private DeathStainsPool _allyStainsPool;
        [SerializeField] private DeathStainsPool _enemyStainsPool;

        [SerializeField] private List<Sprite> _stainSprites;
        [SerializeField] private float _minStainsDistance;

        private GameFactory _gameFactory;

        [Inject]
        private void Construct(GameFactory gameFactory)
        {
            _gameFactory = gameFactory;
        }

        private void Awake()
        {
            _allyStainsPool.Init(_gameFactory, _stainSprites);
            _enemyStainsPool.Init(_gameFactory, _stainSprites);
        }

        public void SpawnStain(Vector3 position, UnitType type)
        {
            switch (type)
            {
                case UnitType.Ally:
                    if (!CheckStainsNearby(position, LayerNames.AllyDeathStain))
                    {
                        Stain stain = _allyStainsPool.Pool.Get();
                        stain.transform.position = position + Vector3.up * stain.StainHeight;
                    }
                    break;
                case UnitType.Enemy:
                    if (!CheckStainsNearby(position, LayerNames.EnemyDeathStain))
                    {
                        Stain stain = _enemyStainsPool.Pool.Get();
                        stain.transform.position = position + Vector3.up * stain.StainHeight;
                    }
                    break;
            }
        }

        private bool CheckStainsNearby(Vector3 position, string layerName)
        {
            return Physics.CheckSphere(position, _minStainsDistance,
                1 << LayerMask.NameToLayer(layerName));
        }
    }
}