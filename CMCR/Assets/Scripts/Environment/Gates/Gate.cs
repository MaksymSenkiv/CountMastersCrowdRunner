using TMPro;
using UnityEngine;

namespace CMCR
{
    [RequireComponent(typeof(BoxCollider))]
    public class Gate : MonoBehaviour
    {
        [SerializeField] private TMP_Text _operationText;
        [SerializeField] private Collider _linkedCollider;
        [SerializeField] private GateData _gateData;
        
        private Collider _collider;

        private void OnValidate()
        {
            SetOperationText();
        }

        private void SetOperationText()
        {
            string operationText = IncreaseTypeToString(_gateData.IncreaseType);
            _operationText.text = $"{operationText}{_gateData.IncreaseValue}";
        }

        private string IncreaseTypeToString(IncreaseType increaseType)
        {
            return increaseType switch
            {
                IncreaseType.Multiply => "x",
                IncreaseType.Add => "+",
                _ => ""
            };
        }

        private void Awake()
        {
            _collider = GetComponent<BoxCollider>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Ally ally))
            {
                _collider.enabled = false;

                if (_linkedCollider != null) {
                    _linkedCollider.enabled = false;
                }

                OnAllyCollided(ally);
            }
        }

        private void OnAllyCollided(Ally ally)
        {
            AlliesGroup alliesGroup = (AlliesGroup)ally.Group;
            alliesGroup.IncreaseAlliesAmount(_gateData);
        }
    }
}