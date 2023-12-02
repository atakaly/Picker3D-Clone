using Picker3D.Gameplay.Collectibles;
using System.Collections.Generic;
using UnityEngine;

namespace Picker3D.Gameplay.PickerSystem
{
    public class Picker : MonoBehaviour
    {
        public MovementController MovementController { get; private set; }

        private List<CollectibleItem> m_CollectibleItems;

        private void Awake()
        {
            MovementController = GetComponent<MovementController>();
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
            MovementController.Stop();

            for (int i = 0; i < m_CollectibleItems.Count; i++)
            {
                m_CollectibleItems[i].AddForce();
            }
        }

        public void OnBasketSuccess()
        {
            MovementController.Move();
        }
    }
}