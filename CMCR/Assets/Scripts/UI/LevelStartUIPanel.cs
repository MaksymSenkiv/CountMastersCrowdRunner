using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CMCR
{
    public class LevelStartUIPanel : UIPanel
    {
        [SerializeField] private Button _levelStartButton;

        public event Action LevelStartButtonPressed;

        protected void OnEnable()
        {
            _levelStartButton.onClick.AddListener(OnStartLevelButtonPressed);
        }

        protected void OnDisable()
        {
            _levelStartButton.onClick.RemoveListener(OnStartLevelButtonPressed);
        }

        private void OnStartLevelButtonPressed()
        {
            Hide();
            LevelStartButtonPressed?.Invoke();
        }
    }
}