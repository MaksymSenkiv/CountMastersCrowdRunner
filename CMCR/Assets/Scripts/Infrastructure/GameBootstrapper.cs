using System;
using UnityEngine;

namespace CMCR
{
    public class GameBootstrapper : MonoBehaviour
    {
        public event Action GameStarted;

        private void Start()
        {
            GameStarted?.Invoke();
        }
    }
}