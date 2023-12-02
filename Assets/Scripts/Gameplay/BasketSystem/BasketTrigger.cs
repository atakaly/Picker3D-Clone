using Picker3D.Gameplay.PickerSystem;
using System.Collections;
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

        public void OnTriggerEnter(Collider other)
        {
            if (m_IsTriggered) return;

            Picker picker = other.GetComponentInParent<Picker>();
            if (picker == null) return;

            m_Picker = picker;
            m_IsTriggered = true;
            picker.OnBasketReached(m_Basket);
            m_Basket.OnSuccess += Basket_OnSuccess;

            StartCoroutine(BasketRoutine());
        }

        private IEnumerator BasketRoutine()
        {
            while (m_Picker.CollectibleItems.Count > 0)
            {
                yield return null;
            }

            yield return new WaitForSeconds(1f);

            if(!m_Basket.IsSufficent)
            {
                GameManager.instance.LevelFailed();
            }
        }

        private void Basket_OnSuccess()
        {
            if (m_Picker == null) return;

            m_Picker.OnBasketSuccess();
            m_Basket.OnSuccess -= Basket_OnSuccess;
        }
    }
}