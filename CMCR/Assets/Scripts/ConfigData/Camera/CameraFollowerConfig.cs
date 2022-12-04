using UnityEngine;

namespace CMCR
{
    [CreateAssetMenu(fileName = "CameraFollowerConfig", menuName = "Configs/Camera/CameraFollowerConfig", order = 0)]
    public class CameraFollowerConfig : ScriptableObject
    {
        public CameraFollowSettingsConfig FollowSettings;
        public CameraShakeConfig Shake;
    }
}