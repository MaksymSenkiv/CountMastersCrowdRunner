using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CMCR
{
    public class Level : MonoBehaviour
    {
        public event Action LevelStarted; 
        
        public void StartLevel()
        {
            LevelStarted?.Invoke();
        }

        public void ReloadLevel()
        {
            SceneManager.LoadScene(0);
        }
    }
}