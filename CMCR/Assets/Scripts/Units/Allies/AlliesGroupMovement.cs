using System.Collections;
using UnityEngine;

namespace CMCR
{
    [RequireComponent(typeof(AlliesGroup), typeof(AlliesGroupBounds))]
    public class AlliesGroupMovement : MonoBehaviour
    {
        [SerializeField] private UnitsGroupMovementConfig _movementConfig;
        [SerializeField] private AlliesGroupCollision _collision;

        private Transform _attackTarget;
        private AlliesGroup _alliesGroup;
        private AlliesGroupBounds _bounds;

        private void Awake()
        {
            _alliesGroup = GetComponent<AlliesGroup>();
            _bounds = GetComponent<AlliesGroupBounds>();
        }

        private void OnEnable()
        {
            _alliesGroup.AttackStopped += GatherUnits;
            _collision.ObstacleAvoided += GatherUnitsAfterObstacle;
        }

        private void OnDisable()
        {
            _alliesGroup.AttackStopped -= GatherUnits;
            _collision.ObstacleAvoided -= GatherUnitsAfterObstacle;
        }

        public void SetTarget(Transform target)
        {
            _attackTarget = target;
        }

        private void Update()
        {
            switch (_alliesGroup.State)
            {
                case UnitsGroupState.Run or UnitsGroupState.Jump:
                {
                    MoveGroup(Vector3.forward, Time.deltaTime);
                    break;
                }
            }
        }

        private void FixedUpdate()
        {
            switch (_alliesGroup.State)
            {
                case UnitsGroupState.Attack:
                {
                    foreach (var unit in _alliesGroup.Units)
                    {
                        if (unit.State == UnitStates.Run)
                        {
                            unit.Move((_attackTarget.position - unit.transform.position).normalized,
                                _movementConfig.UnitSpeed, Time.fixedDeltaTime);
                        }
                    }

                    break;
                }
            }
        }

        private void MoveGroup(Vector3 direction, float deltaTime)
        {
            transform.Translate(direction * (_movementConfig.GroupSpeed * deltaTime));
        }

        private void GatherUnitsAfterObstacle()
        {
            StartCoroutine(GatherUnitsAfterObstacleRoutine());
        }

        private IEnumerator GatherUnitsAfterObstacleRoutine()
        {
            while (Physics.CheckBox(_bounds.UnitsBounds.center, _bounds.UnitsBounds.extents,
                       Quaternion.identity, 1 << LayerMask.NameToLayer(LayerNames.Obstacle)))
            {
                yield return null;
            }

            GatherUnits();
        }

        private void GatherUnits() 
        {
            StartCoroutine(GatherUnitsRoutine());
        }

        private IEnumerator GatherUnitsRoutine()
        {
            for (float i = 0; i < _movementConfig.GatheringTime;)
            {
                if (_alliesGroup.State == UnitsGroupState.Attack) {
                    yield break;
                }
                if (_alliesGroup.State == UnitsGroupState.Jump) {
                    yield return null;
                }
                else
                {
                    i += Time.fixedDeltaTime;
                    foreach (var unit in _alliesGroup.Units)
                    {
                        unit.Move((_alliesGroup.CenterTransform.position - unit.transform.position).normalized,
                            _movementConfig.GatheringSpeed, Time.fixedDeltaTime);
                    }

                    yield return new WaitForFixedUpdate();
                }
            }
            _bounds.CalculateUnitsBounds();
        }
    }
}