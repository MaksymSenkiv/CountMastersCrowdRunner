using UnityEngine;

namespace CMCR
{
    [CreateAssetMenu(fileName = "CameraShakeConfig", menuName = "Configs/Camera/CameraShakeConfig")]
    public class CameraShakeConfig : ScriptableObject
    {
        public float Duration;
        public float Strength;
        public int Vibrato;
    }
}