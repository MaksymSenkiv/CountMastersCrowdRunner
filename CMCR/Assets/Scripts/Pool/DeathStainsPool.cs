using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Random = System.Random;

namespace CMCR
{
    [Serializable]
    public class DeathStainsPool
    {
        private static Random _random = new();

        public ObjectPool<Stain> Pool;

        private List<Stain> _stains;
        private List<Sprite> _stainSprites;
        private GameFactory _gameFactory;

        [SerializeField] private Stain _stainPrefab;
        [SerializeField] private Transform _stainsRootTransform;

        [SerializeField] private int _defaultSize;
        [SerializeField] private int _maxSize;

        public void Init(GameFactory gameFactory, List<Sprite> stainSprites)
        {
            _gameFactory = gameFactory;
            _stainSprites = stainSprites;
            
            CreatePool();
        }
        
        private void CreatePool()
        {
            Pool = new ObjectPool<Stain>(
                CreatePooledObject, 
                GetPooledObject,
                ReleasePooledObject,
                DestroyPooledObject,
                false, _defaultSize, _maxSize);
            _stains = new List<Stain>();
            for (int i = 0; i < _defaultSize; i++)
            {
                Stain stain = CreateStain();
                stain.BecameInvisible += ReleasePooledObject;
                _stains.Add(stain);
                stain.gameObject.SetActive(false);
            }
        }

        private Stain CreatePooledObject()
        {
            Stain stain;
            if (_stains.Count == 0) {
                stain = CreateStain();
                _stains.Add(stain);
            }
            else {
                stain = _stains[0];
            }
            
            return stain;
        }

        private void GetPooledObject(Stain pooledObject)
        {
            if (pooledObject != null) {
                pooledObject.gameObject.SetActive(true);
                _stains.Remove(pooledObject);
            }
        }

        private void ReleasePooledObject(Stain pooledObject)
        {
            pooledObject.gameObject.SetActive(false);
            _stains.Add(pooledObject);
        }

        private void DestroyPooledObject(Stain pooledObject)
        {
        }

        private Stain CreateStain()
        {
            Sprite sprite = _stainSprites[_random.Next(0, _stainSprites.Count)];
            return _gameFactory.CreateStain(_stainPrefab, _stainsRootTransform, sprite);
        }
    }
}