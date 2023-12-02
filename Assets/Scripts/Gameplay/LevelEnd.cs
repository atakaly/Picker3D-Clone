using Picker3D.Gameplay.PickerSystem;
using UnityEngine;

namespace Picker3D.Gameplay
{
    public class LevelEnd : LevelObjectBase
    {
        public void OnTriggerEnter(Collider other)
        {
            Picker picker = other.GetComponentInParent<Picker>();
            if (picker == null) return;

            picker.MovementController.Stop();
            GameManager.instance.LevelSucceed();
        }
    }
}