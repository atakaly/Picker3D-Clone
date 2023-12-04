using DG.Tweening;
using Picker3D.Gameplay.Collectibles;
using System;
using TMPro;
using UnityEngine;

namespace Picker3D.Gameplay.BasketSystem
{
    public class Basket : LevelObjectBase
    {
        public Action OnSuccess;

        [SerializeField] private TextMeshPro m_TextMesh;

        [Space]

        [SerializeField] private Transform platformObject;
        [SerializeField] private Vector3 platformEndPosition;

        private BasketTrigger basketTrigger;

        private Vector3 platformStartPosition;

        [SerializeField] private int m_RequiredBallCount = 10;

        public int RequiredBallCount
        {
            get
            {
                return m_RequiredBallCount;
            }
            set
            {
                m_RequiredBallCount = value;
                UpdateTextMesh();
            }
        }

        private int m_CurrentCollectedBallCount = 0;

        public bool IsSufficent { get => m_CurrentCollectedBallCount >= m_RequiredBallCount; }

        private void Awake()
        {
            platformStartPosition = platformObject.localPosition;
            basketTrigger = GetComponentInChildren<BasketTrigger>();
        }

        public void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent(out ICollectible collectible))
            {
                if (!collectible.IsCollectible) return;

                collectible.OnCollected();
                OnCollectibleCollect();
            }
        }

        private void OnCollectibleCollect()
        {
            m_CurrentCollectedBallCount++;
            UpdateTextMesh();
            if (IsSufficent)
            {
                platformObject.DOLocalMove(platformEndPosition, 1f)
                    .SetDelay(0.5f)
                    .SetEase(Ease.OutBack)
                    .OnComplete(() =>
                    {
                        OnSuccess?.Invoke();
                    });
            }
        }

        public override void OnSpawn()
        {
            base.OnSpawn();

            platformObject.localPosition = platformStartPosition;
            basketTrigger.IsTriggered = false;

            m_CurrentCollectedBallCount = 0;

            UpdateTextMesh();
        }

        public override void OnDespawn()
        {
            base.OnDespawn();
        }

        private void UpdateTextMesh()
        {
            string text = string.Format("{0}/{1}", m_CurrentCollectedBallCount, m_RequiredBallCount);
            m_TextMesh.text = text;
        }
    }
}