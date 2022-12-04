using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CMCR
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] private Image _fillImage;

        private Transform _alliesGroupCenterTransform;
        private Boss _boss;
        private float _maxDistance;

        [Inject]
        private void Construct(AlliesGroup alliesGroup, Boss boss)
        {
            _alliesGroupCenterTransform = alliesGroup.CenterTransform;
            _boss = boss;
        }

        private void Start()
        {
            _maxDistance = _boss.transform.position.z - _alliesGroupCenterTransform.position.z;
        }

        private void Update()
        {
            SetProgress();
        }

        private void SetProgress()
        {
            float fill = 1 - (_boss.transform.position.z - _alliesGroupCenterTransform.position.z)/_maxDistance;
            _fillImage.fillAmount = fill;
        }
    }
}