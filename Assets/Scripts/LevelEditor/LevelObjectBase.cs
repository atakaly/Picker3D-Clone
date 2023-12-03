using UnityEngine;

namespace Picker3D.Gameplay
{
    public class LevelObjectBase : MonoBehaviour, IPoolable
    {
        public virtual void OnDespawn()
        {
        }

        public virtual void OnSpawn()
        {
        }
    }
}