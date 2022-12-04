using UnityEngine;
using UnityEngine.Pool;
using Zenject;

namespace CMCR
{
    public class AlliesPool : MonoBehaviour
    {
        public ObjectPool<Ally> Pool;
        
        [SerializeField] private Ally _allyPrefab;
        [SerializeField] private int _defaultSize = 100;
        [SerializeField] private int _maxSize = 200;

        private GameFactory _gameFactory;

        [Inject]
        private void Construct(GameFactory gameFactory)
        {
            _gameFactory = gameFactory;
        }
        
        private void Awake()
        {
            Pool = new ObjectPool<Ally>(
                CreatePooledObject, 
                GetPooledObject,
                ReleasePooledObject,
                DestroyPooledObject,
                false, _defaultSize, _maxSize);
            
            for (int i = 0; i < _defaultSize; i++)
            {
                Ally ally = _gameFactory.CreateUnit(_allyPrefab, transform);
                ally.Destroyed += ReleasePooledObject;
                ally.gameObject.SetActive(false);
            }
        }

        private Ally CreatePooledObject()
        {
            if (transform.childCount != 0) {
                return transform.GetChild(0).GetComponent<Ally>();
            }

            Ally ally = _gameFactory.CreateUnit(_allyPrefab, transform);
            ally.Died += ReleasePooledObject;
            ally.gameObject.SetActive(false);
            return ally;
        }

        private void GetPooledObject(Ally pooledObject)
        {
            pooledObject.gameObject.SetActive(true);
        }

        private void ReleasePooledObject(Ally pooledObject)
        {
            pooledObject.transform.parent = transform;
            pooledObject.gameObject.SetActive(false);
        }

        private void DestroyPooledObject(Ally pooledObject)
        {
        }
    }
}