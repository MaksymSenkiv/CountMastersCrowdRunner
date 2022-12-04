using System.Collections.Generic;
using UnityEngine;

namespace CMCR
{
    public class UIRoot : MonoBehaviour
    {
        public List<UIPanel> UIPanels;

        public TPanel GetUIPanel<TPanel>() where TPanel : UIPanel
        {
            return UIPanels.Find(panel => panel is TPanel) as TPanel;
        }
    }
}