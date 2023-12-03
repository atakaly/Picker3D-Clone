﻿using DG.Tweening;
using Picker3D.Gameplay.BasketSystem;
using UnityEngine;

namespace Picker3D.Gameplay.Collectibles
{
    public class CollectibleItem : MonoBehaviour, ICollectible
    {
        private CollectibleMeshChanger m_CollectibleMeshChanger;
        private Rigidbody m_Rigidbody;

        private bool m_IsCollectible = true;
        public bool IsCollectible => m_IsCollectible;

        public CollectibleMeshChanger CollectibleMeshChanger => m_CollectibleMeshChanger;

        private void Awake()
        {
            m_CollectibleMeshChanger = GetComponent<CollectibleMeshChanger>();
            m_Rigidbody = GetComponent<Rigidbody>();
        }

        public void OnCollected()
        {
            m_IsCollectible = false;
        }

        public void MoveToBasket(Basket basket)
        {
            m_Rigidbody.DOMoveZ(basket.transform.position.z, 0.7f);
        }

        public void ResetItem()
        {
            m_IsCollectible = true;
            m_Rigidbody.velocity = Vector3.zero;
        }
    }
}