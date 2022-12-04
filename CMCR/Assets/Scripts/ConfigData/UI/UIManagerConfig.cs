using UnityEngine;

namespace CMCR
{
    [CreateAssetMenu(fileName = "UIManagerConfig", menuName = "Configs/UI/UIManagerConfig", order = 0)]
    public class UIManagerConfig : ScriptableObject
    {
        public float GameOverUIPanelDelay;
        public float LevelFinishUIPanelDelay;
    }
}