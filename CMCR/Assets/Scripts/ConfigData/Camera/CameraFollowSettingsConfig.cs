using UnityEngine;

namespace CMCR
{
    [CreateAssetMenu(fileName = "CameraFollowSettingsConfig", menuName = "Configs/Camera/CameraFollowSettingsConfig")]
    public class CameraFollowSettingsConfig : ScriptableObject
    {
        public float RotationAngleX;
        public float Distance;
        public float OffsetY;
        public float OffsetZ;
        public float Borders;
        public float DelayAxisX;
    }
}