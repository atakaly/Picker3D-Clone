using Picker3D.Gameplay.PickerSystem;
using UnityEngine;

namespace Picker3D.Gameplay.BasketSystem
{
    public class BasketTrigger : MonoBehaviour
    {
        private Basket m_Basket;
        private bool m_IsTriggered = false;

        private Picker m_Picker;

        private void Awake()
        {
            m_Basket = GetComponentInParent<Basket>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (m_IsTriggered) return;

            Picker picker = other.GetComponentInParent<Picker>();
            if (picker == null) return;

            m_Picker = picker;
            picker.OnBasketReached();
            m_Basket.OnSuccess += Basket_OnSuccess;
            m_IsTriggered = true;
        }

        private void Basket_OnSuccess()
        {
            if (m_Picker == null) return;

            m_Picker.OnBasketSuccess();
            m_Basket.OnSuccess -= Basket_OnSuccess;
        }
    }
}