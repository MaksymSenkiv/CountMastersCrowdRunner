using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace CMCR
{
    [Serializable]
    public class DeathParticlesPool 
    {
        public ObjectPool<ParticleSystem> Pool;
        
        private Queue<ParticleSystem> _particlesQueue;
        private GameFactory _gameFactory;

        [SerializeField] private ParticleSystem _particleSystemPrefab;
        [SerializeField] private Transform _particlesRootTransform;

        [SerializeField] private int _defaultSize;
        [SerializeField] private int _maxSize;


        public void Init(GameFactory gameFactory)
        {
            _gameFactory = gameFactory;

            CreatePool();
        }
        
        private void CreatePool()
        {
            Pool = new ObjectPool<ParticleSystem>(
                CreatePooledObject, 
                GetPooledObject,
                ReleasePooledObject,
                DestroyPooledObject,
                false, _defaultSize, _maxSize);
            _particlesQueue = new Queue<ParticleSystem>();
            for (int i = 0; i < _defaultSize; i++)
            {
                ParticleSystem particle = _gameFactory.CreateParticle(_particleSystemPrefab, _particlesRootTransform);
                _particlesQueue.Enqueue(particle);
            }
        }

        private ParticleSystem CreatePooledObject()
        {
            ParticleSystem particle = !_particlesQueue.Peek().gameObject.activeInHierarchy
                ? _particlesQueue.Dequeue()
                : _gameFactory.CreateParticle(_particleSystemPrefab, _particlesRootTransform);

            _particlesQueue.Enqueue(particle);
            return particle;
        }

        private void GetPooledObject(ParticleSystem pooledObject)
        {
            pooledObject.gameObject.SetActive(true);
        }

        private void ReleasePooledObject(ParticleSystem pooledObject)
        {
            pooledObject.gameObject.SetActive(false);
        }

        private void DestroyPooledObject(ParticleSystem pooledObject)
        {
        }
    }

}