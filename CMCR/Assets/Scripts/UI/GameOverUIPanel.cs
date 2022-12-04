using System;
using UnityEngine;
using UnityEngine.UI;

namespace CMCR
{
    public class GameOverUIPanel : UIPanel
    {
        [SerializeField] private Button _playAgainButton;
        
        public event Action PlayAgainButtonPressed;

        private void OnEnable()
        {
            _playAgainButton.onClick.AddListener(OnPlayAgainButtonPressed);
        }

        private void OnDisable()
        {
            _playAgainButton.onClick.RemoveListener(OnPlayAgainButtonPressed);
        }

        private void OnPlayAgainButtonPressed()
        {
            Hide();
            PlayAgainButtonPressed?.Invoke();
        }
    }
}