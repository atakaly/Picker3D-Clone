using UnityEngine;

namespace Picker3D.Gameplay
{
    public class LevelObjectBase : MonoBehaviour, IPoolable
    {
        public string UniquePrefabId;

        public virtual void OnDespawn()
        {
        }

        public virtual void OnSpawn()
        {
        }
    }
}