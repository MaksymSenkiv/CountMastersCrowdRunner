using System;
using UnityEngine;
using UnityEngine.UI;

namespace CMCR
{
    public class HudUIPanel : UIPanel
    {
        [SerializeField] private Button _reloadButton;

        public event Action ReloadButtonPressed;

        protected void OnEnable()
        {
            _reloadButton.onClick.AddListener(OnReloadButtonPressed);
        }

        protected void OnDisable()
        {
            _reloadButton.onClick.RemoveListener(OnReloadButtonPressed);
        }

        private void OnReloadButtonPressed()
        {
            ReloadButtonPressed?.Invoke();
        }
    }
}