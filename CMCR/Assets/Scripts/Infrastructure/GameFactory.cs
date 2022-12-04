using UnityEngine;

namespace CMCR
{
    public class GameFactory
    {
        private readonly Assets _assets;

        public GameFactory(Assets assets)
        {
            _assets = assets;
        }

        public Ally CreateUnit(Ally gameObject, Transform parent)
        {
            return _assets.Instantiate(gameObject, parent);
        }

        public Enemy CreateEnemy(Enemy gameObject, Transform parent)
        {
            return _assets.Instantiate(gameObject, parent);
        }

        public ParticleSystem CreateParticle(ParticleSystem gameObject, Transform parent)
        {
            return _assets.Instantiate(gameObject, parent);
        }
        
        public Stain CreateStain(Stain gameObject, Transform parent, Sprite sprite)
        {
            Stain stain = _assets.Instantiate(gameObject, parent);
            stain.Init(sprite);
            return stain;
        }
    }
}