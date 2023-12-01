using Picker3D.Gameplay.PickerSystem;
using UnityEngine;

namespace Picker3D.Gameplay.BasketSystem
{
    public class BasketTrigger : MonoBehaviour
    {
        private bool m_IsTriggered = false;

        private void OnTriggerEnter(Collider other)
        {
            if (m_IsTriggered) return;

            Picker picker = other.GetComponentInParent<Picker>();
            if (picker == null) return;

            picker.OnBasketReached();
            m_IsTriggered = true;
        }
    }
}