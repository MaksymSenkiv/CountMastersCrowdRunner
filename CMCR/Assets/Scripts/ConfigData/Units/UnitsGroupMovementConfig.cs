using UnityEngine;

namespace CMCR
{
    [CreateAssetMenu(fileName = "UnitsGroupMovementConfig", menuName = "Configs/Unit/UnitsGroupMovementConfig")]
    public class UnitsGroupMovementConfig : ScriptableObject
    {
        public float GroupSpeed;
        public float UnitSpeed;
        public float GatheringSpeed;
        public float GatheringTime;
    }
}