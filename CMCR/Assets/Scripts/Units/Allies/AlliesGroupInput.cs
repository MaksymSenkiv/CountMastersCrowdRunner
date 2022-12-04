using UnityEngine;

namespace CMCR
{
    [RequireComponent(typeof(AlliesGroup))]
    public class AlliesGroupInput : MonoBehaviour
    {
        [SerializeField] private float _roadWidth;
        [SerializeField] private float _minDelta = 0.1f;
        [SerializeField] private float _movingDelay;

        private AlliesGroup _alliesGroup;
        private AlliesGroupBounds _bounds;
        
        private Vector3 _lastMousePosition;
        private Vector3 _targetPosition;

        private void Awake()
        {
            _alliesGroup = GetComponent<AlliesGroup>();
            _bounds = GetComponent<AlliesGroupBounds>();
        }

        private void Update()
        {
            if (_alliesGroup.State is UnitsGroupState.Run or UnitsGroupState.Jump) {
                Move();
            }
        }

        private void Move()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _lastMousePosition = Input.mousePosition;
                _targetPosition = transform.position;
            }
            else if (Input.GetMouseButton(0))
            {
                MoveToTarget();
                _lastMousePosition = Input.mousePosition;
            }
        }

        private void MoveToTarget()
        {
            float delta = (Input.mousePosition.x - _lastMousePosition.x) / (Screen.width / _roadWidth);

            if (Mathf.Abs(delta) > _minDelta) {
                CalculateTargetPosition(delta);
            }

            transform.position = new Vector3(Mathf.Lerp(transform.position.x, _targetPosition.x, 1f / _movingDelay), 
                transform.position.y, 
                transform.position.z);
        }

        private void CalculateTargetPosition(float delta)
        {
            _targetPosition.x += delta;
            _targetPosition.x = Mathf.Clamp(_targetPosition.x, 
                -_roadWidth / 2f + (_bounds.UnitsBounds.extents.x - _alliesGroup.CenterTransform.localPosition.x), 
                _roadWidth / 2f - (_bounds.UnitsBounds.extents.x + _alliesGroup.CenterTransform.localPosition.x));
        }
    }
}