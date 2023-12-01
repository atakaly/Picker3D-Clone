using UnityEngine;

namespace Picker3D.Gameplay.Collectibles
{
    public class CollectibleItem : MonoBehaviour, ICollectible
    {
        [SerializeField] private float m_ForceMultiplier;

        private Rigidbody m_Rigidbody;

        private bool m_IsCollectible = true;
        public bool IsCollectible => m_IsCollectible;

        private void Awake()
        {
            m_Rigidbody = GetComponent<Rigidbody>();
        }

        public void OnCollected()
        {
            m_IsCollectible = false;
        }

        public void AddForce()
        {
            m_Rigidbody.AddForce(m_ForceMultiplier * (Vector3.forward + Vector3.up));
        }
    }
}