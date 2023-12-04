using DG.Tweening;
using Picker3D.Gameplay.BasketSystem;
using Picker3D.Gameplay.Collectibles;
using System.Collections.Generic;
using UnityEngine;

namespace Picker3D.Gameplay.PickerSystem
{
    public class Picker : MonoBehaviour
    {
        public MovementController MovementController { get; private set; }

        public List<CollectibleItem> CollectibleItems { get; private set; }

        private void Awake()
        {
            MovementController = GetComponent<MovementController>();
            CollectibleItems = new List<CollectibleItem>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent(out CollectibleItem collectible))
            {
                if (CollectibleItems.Contains(collectible)) return;

                CollectibleItems.Add(collectible);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out CollectibleItem collectible))
            {
                if (!CollectibleItems.Contains(collectible)) return;

                CollectibleItems.Remove(collectible);
            }
        }

        public void OnBasketReached(Basket basket)
        {
            for (int i = 0; i < CollectibleItems.Count; i++)
            {
                CollectibleItems[i].MoveToBasket(basket);
            }

            MovementController.Stop();
        }

        public void OnBasketSuccess()
        {
            MovementController.Move();
        }
    }
}