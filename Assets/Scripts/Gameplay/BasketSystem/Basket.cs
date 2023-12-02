using Picker3D.Gameplay.Collectibles;
using TMPro;
using UnityEngine;

namespace Picker3D.Gameplay.BasketSystem
{
    public class Basket : MonoBehaviour
    {
        [SerializeField] private TextMeshPro m_TextMesh;

        private int m_CurrentCollectedBallCount = 0;
        private int m_RequiredBallCount = 10;

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
                Debug.Log("Success");
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