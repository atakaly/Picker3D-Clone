using UnityEngine;

namespace Picker3D.Gameplay.PickerSystem
{
    [RequireComponent(typeof(Rigidbody))]
    public class MovementController : MonoBehaviour
    {
        [SerializeField] private float m_ForwardSpeed;
        [SerializeField] private float m_ControlSpeed;

        [SerializeField] private float m_AxisXThreshold;

        private Rigidbody m_Rigidbody;

        private bool isDragging;

        private bool m_ShouldMove = true;

        private void Awake()
        {
            m_Rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                isDragging = true;
                m_Rigidbody.velocity = Vector3.zero;
            }

            if (Input.GetMouseButtonUp(0))
            {
                isDragging = false;
                m_Rigidbody.velocity = Vector3.zero;
            }

            if (isDragging)
            {
                float mouseX = Input.GetAxis("Mouse X");
                float mouseY = Input.GetAxis("Mouse Y");

                MoveXAxis(new Vector2(mouseX, mouseY));
            }

            MoveForward();
        }

        public void MoveXAxis(Vector3 inputDelta)
        {
            Vector3 velocity = new Vector3(inputDelta.x * m_ControlSpeed, m_Rigidbody.velocity.y, m_Rigidbody.velocity.z);
            m_Rigidbody.velocity = velocity;

            float clampX = Mathf.Clamp(transform.position.x, -m_AxisXThreshold, m_AxisXThreshold);
            transform.position = new Vector3(clampX, transform.position.y, transform.position.z);
        }

        public void MoveForward()
        {
            if (!m_ShouldMove) return;

            m_Rigidbody.velocity = new Vector3(m_Rigidbody.velocity.x, m_Rigidbody.velocity.y, m_ForwardSpeed);
        }

        public void Stop()
        {
            m_ShouldMove = false;
            m_Rigidbody.velocity = Vector3.zero;
        }

        public void Move()
        {
            m_ShouldMove = true;
        }
    }
}
