using UnityEngine;

namespace CMCR
{
    public class Assets
    {
        public Ally Instantiate(Ally gameObject, Transform parent)
        {
            return Object.Instantiate(gameObject, parent);
        }

        public Enemy Instantiate(Enemy gameObject, Transform parent)
        {
            return Object.Instantiate(gameObject, parent);
        }

        public ParticleSystem Instantiate(ParticleSystem gameObject, Transform parent)
        {
            return Object.Instantiate(gameObject, parent);
        }

        public Stain Instantiate(Stain gameObject, Transform parent)
        {
            return Object.Instantiate(gameObject, parent);
        }
    }
}