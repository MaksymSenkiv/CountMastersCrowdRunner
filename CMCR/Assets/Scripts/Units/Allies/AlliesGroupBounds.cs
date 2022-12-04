using UnityEngine;

namespace CMCR
{
    [RequireComponent(typeof(AlliesGroup))]
    public class AlliesGroupBounds : MonoBehaviour
    {
        [SerializeField] private BoxCollider _collider;
        
        private AlliesGroup _alliesGroup;
        private Bounds _unitsBounds;

        public Bounds UnitsBounds => _unitsBounds;

        private void Awake()
        {
            _alliesGroup = GetComponent<AlliesGroup>();
        }

        private void Update()
        {
            _unitsBounds.center = _alliesGroup.CenterTransform.position;
            _collider.size = _unitsBounds.size + Vector3.up;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(_unitsBounds.center, 0.25f);
            Gizmos.DrawWireCube(_unitsBounds.center, _unitsBounds.size);
        }
        
        public Bounds CalculateUnitsBounds()
        {
            Bounds bounds = new Bounds();
            if (_alliesGroup.Units.Count > 0)
            {
                bounds = new Bounds(VectorWithoutY(_alliesGroup.Units[0].transform.position), Vector3.zero);
                foreach (Unit unit in _alliesGroup.Units)
                {
                    bounds.Encapsulate(VectorWithoutY(unit.transform.position));
                }
            }

            _unitsBounds = bounds;
            return bounds;
        }

        private Vector3 VectorWithoutY(Vector3 vector3)
        {
            return new Vector3(vector3.x, 0, vector3.z);
        }
    }
}