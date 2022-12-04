using UnityEngine;

namespace CMCR
{
    [CreateAssetMenu(fileName = "UIPanelAppearanceConfig", menuName = "Configs/UI/UIPanelAppearanceConfig", order = 1)]
    public class UIPanelAppearanceConfig : ScriptableObject
    {
        public float ShowDuration;
        public float HideDuration;
    }
}