using Picker3D.Gameplay.Collectibles;
using System.Collections.Generic;
using UnityEngine;

namespace Picker3D.Gameplay.PickerSystem
{
    public class Picker : MonoBehaviour
    {
        private MovementController m_MovementController;

        private List<CollectibleItem> m_CollectibleItems;

        private void Awake()
        {
            m_MovementController = GetComponent<MovementController>();
            m_CollectibleItems = new List<CollectibleItem>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent(out CollectibleItem collectible))
            {
                if (m_CollectibleItems.Contains(collectible)) return;

                m_CollectibleItems.Add(collectible);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out CollectibleItem collectible))
            {
                if (!m_CollectibleItems.Contains(collectible)) return;

                m_CollectibleItems.Add(collectible);
            }
        }

        public void OnBasketReached()
        {
            m_MovementController.Stop();

            for (int i = 0; i < m_CollectibleItems.Count; i++)
            {
                m_CollectibleItems[i].AddForce();
            }
        }
    }
}