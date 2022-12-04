using System;
using UnityEngine.Events;

namespace CMCR
{
    public class Enemy : Unit
    {
        public event Action<Enemy, UnityAction> Died;

        public void Die(UnityAction allEnemiesDiedCallback)
        {
            _state = UnitStates.Dead;
            Died?.Invoke(this, allEnemiesDiedCallback);
            PlayDestroyEffect(UnitType.Enemy);
            gameObject.SetActive(false);
        }
    }
}