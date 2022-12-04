using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

namespace CMCR   
{
    public abstract class UnitsGroup : MonoBehaviour
    {
        [SerializeField] protected int _startCount;
        [SerializeField] protected TMP_Text _countText;
        
        [SerializeField] private UnitType _unitType;
        [SerializeField] private Transform[] _spawnPoints;

        private UnitsFactory _unitsFactory;
        private StainsSpawner _stainsSpawner;
        private Camera _camera;

        [field:SerializeField] public Transform CenterTransform { get; private set; }
        
        public List<Unit> Units { get; } = new();
        [field:SerializeField] public UnitsGroupState State { get; protected set; }
        
        protected DeathParticlesPool DeathParticlesPool { get; set; }

        [Inject]
        private void Construct(UnitsFactory unitsFactory, StainsSpawner stainsSpawner)
        {
            _unitsFactory = unitsFactory;
            _stainsSpawner = stainsSpawner;
        }

        protected void Awake()
        {
            _camera = Camera.main;
        }
        
        protected void Start()
        {
            SpawnUnits(_startCount, _unitType);
            SetUIRotation(_countText.transform.parent);
        }

        private void Update()
        {
            CalculateUnitsCenter();
        }

        private void CalculateUnitsCenter()
        {
            if (Units.Count == 0) {
                return;
            }
            
            CenterTransform.position = Vector3.zero;
            foreach (var unit in Units) {
                CenterTransform.position += unit.transform.position;
            }

            CenterTransform.position /= Units.Count;
        }

        protected virtual void SpawnUnits(int newUnitsCount, UnitType type, UnitStates startState = UnitStates.Idle)
        {
            for (int i = 0; i < newUnitsCount; i++)
            {
                Unit spawnedUnit = _unitsFactory.GetUnit(type); 
                Vector3 spawnPoint = CalculateSpawnPoint();
                spawnedUnit.transform.position = spawnPoint;
                spawnedUnit.transform.parent = transform;
                AddUnit(spawnedUnit);
                spawnedUnit.Init(this, DeathParticlesPool, _stainsSpawner);
                if (startState == UnitStates.Run) {
                    spawnedUnit.StartRunning();
                }
            }
        }

        private Vector3 CalculateSpawnPoint() 
        {
            return _spawnPoints[Units.Count % _spawnPoints.Length].localPosition + CenterTransform.position;
        }

        protected virtual void AddUnit(Unit unit)
        {
            Units.Add(unit);
            ChangeUnitsAmountText();
        }

        protected void ChangeUnitsAmountText() 
        {
            _countText.text = Units.Count.ToString();
        }
        
        protected void UnitsStartRunning()
        {
            foreach (Unit unit in Units) {
                unit.StartRunning();
            }
        }
        
        protected void Celebrate()
        {
            State = UnitsGroupState.Idle;
            foreach (Unit unit in Units) {
                unit.Celebrate();
            }
        }
        
        protected  void SetUIRotation(Transform uiTransform)
        {
            uiTransform.LookAt(transform.position - _camera.transform.position);
        }
        
        protected void DisableUI()
        {
            _countText.transform.parent.gameObject.SetActive(false);
        }
    }
}