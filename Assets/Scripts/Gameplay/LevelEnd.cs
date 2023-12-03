using Picker3D.Gameplay.PickerSystem;
using UnityEngine;

namespace Picker3D.Gameplay
{
    public class LevelEnd : LevelObjectBase
    {
        private bool m_IsTriggered = false;

        public void OnTriggerEnter(Collider other)
        {
            if (m_IsTriggered) return;
            Picker picker = other.GetComponentInParent<Picker>();
            if (picker == null) return;

            picker.MovementController.Stop();
            GameManager.instance.LevelSucceed();
            m_IsTriggered = true;
        }

        public override void OnSpawn()
        {
            base.OnSpawn();

            m_IsTriggered = false;
        }
    }
}