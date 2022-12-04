using DG.Tweening;
using UnityEngine;
using Zenject;

namespace CMCR
{
    public class CameraFollower : MonoBehaviour
    {
        [SerializeField] private CameraFollowerConfig _config;

        private Transform _following;
        private AlliesGroup _alliesGroup;

        [Inject]
        private void Construct(AlliesGroup alliesGroup)
        {
            _alliesGroup = alliesGroup;
            _following = alliesGroup.CenterTransform;
        }

        private void Awake()
        {
            SetTransform();
        }

        private void OnEnable()
        {
            _alliesGroup.AllAlliesDied += StopFollowing;
        }

        private void OnDisable()
        {
            _alliesGroup.AllAlliesDied -= StopFollowing;
        }

        private void StopFollowing()
        {
            _following = null;
        }

        private void LateUpdate()
        {
            SetTransform();
        }

        private void SetTransform()
        {
            if (_following == null)
            {
                return;
            }

            Quaternion rotation = Quaternion.Euler(_config.FollowSettings.RotationAngleX, 0, 0);
            Vector3 position = rotation * new Vector3(0, 0, -_config.FollowSettings.Distance) + FollowingPointPosition();

            transform.rotation = rotation;
            transform.position = position;
        }

        private Vector3 FollowingPointPosition()
        {
            Vector3 followingPosition = _following.position;
            followingPosition.y = _config.FollowSettings.OffsetY;
            followingPosition.z += _config.FollowSettings.OffsetZ;
            followingPosition.x = CalculateCameraAxisX(followingPosition.x);
            
            return followingPosition;
        }

        private float CalculateCameraAxisX(float followingPositionX)
        {
            float target = Mathf.Clamp(followingPositionX, -_config.FollowSettings.Borders, _config.FollowSettings.Borders);
            target = Mathf.Lerp(transform.position.x, target, 1 / _config.FollowSettings.DelayAxisX);
            
            return target;
        }

        public void Shake()
        {
            transform.DOShakePosition(_config.Shake.Duration, _config.Shake.Strength, _config.Shake.Vibrato, 0);
        }
    }
}