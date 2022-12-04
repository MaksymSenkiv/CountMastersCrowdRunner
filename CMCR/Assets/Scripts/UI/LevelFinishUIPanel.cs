using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CMCR
{
    public class LevelFinishUIPanel : UIPanel
    {
        [SerializeField] private Button _nextLevelButton;

        public event Action NextLevelButtonPressed;

        private void OnEnable()
        {
            _nextLevelButton.onClick.AddListener(OnNextLevelButtonPressed);
        }

        private void OnDisable()
        {
            _nextLevelButton.onClick.RemoveListener(OnNextLevelButtonPressed);
        }
        
        private void OnNextLevelButtonPressed()
        {
            Hide();
            NextLevelButtonPressed?.Invoke();
        }
    }
}