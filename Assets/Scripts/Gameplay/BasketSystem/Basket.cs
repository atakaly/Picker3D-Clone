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

        private int m_CurrentCollectedBallCount = 0;
        private int m_RequiredBallCount = 10;

        private void Start()
        {
            UpdateTextMesh();
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
            if(m_CurrentCollectedBallCount >= m_RequiredBallCount)
            {
                platformObject.DOLocalMove(platformEndPosition, 1.2f)
                    .OnComplete(() =>
                    {
                        OnSuccess?.Invoke();
                    });
            }

            UpdateTextMesh();
        }

        private void UpdateTextMesh()
        {
            string text = string.Format("{0}/{1}", m_CurrentCollectedBallCount, m_RequiredBallCount);
            m_TextMesh.text = text;
        }
    }
}