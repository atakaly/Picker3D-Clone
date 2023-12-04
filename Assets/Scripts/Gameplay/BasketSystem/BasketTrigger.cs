using Picker3D.Gameplay.Collectibles;
using Picker3D.Gameplay.PickerSystem;
using System.Collections;
using UnityEngine;

namespace Picker3D.Gameplay.BasketSystem
{
    public class BasketTrigger : MonoBehaviour
    {
        private Basket m_Basket;
        public bool IsTriggered { get; set; } = false;

        private Picker m_Picker;

        private void Awake()
        {
            m_Basket = GetComponentInParent<Basket>();
        }

        public void OnTriggerEnter(Collider other)
        {
            if (IsTriggered) return;

            Picker picker = other.GetComponentInParent<Picker>();
            if (picker == null) return;

            m_Picker = picker;
            IsTriggered = true;
            picker.OnBasketReached(m_Basket);
            m_Basket.OnSuccess += Basket_OnSuccess;

            StartCoroutine(BasketRoutine());
        }

        private IEnumerator BasketRoutine()
        {
            yield return new WaitUntil(() => m_Picker.CollectibleItems.Count == 0);
            yield return new WaitForSeconds(CollectibleItem.BASKET_MOVE_DURATION + 0.5f);

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