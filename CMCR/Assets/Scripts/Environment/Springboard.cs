using UnityEngine;

namespace CMCR
{
    public class Springboard : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Ally ally) && ally.State == UnitStates.Run) {
                ally.Jump();
            }
        }
    }
}