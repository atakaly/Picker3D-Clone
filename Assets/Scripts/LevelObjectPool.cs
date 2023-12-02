using Picker3D.Gameplay;
using System.Collections.Generic;
using Zenject;

namespace Picker3D.LevelManagement
{
    public class LevelObjectPool : IInitializable
    {
        private Dictionary<LevelObjectBase, Queue<LevelObjectBase>> objectPool;

        public void Initialize()
        {
            objectPool = new Dictionary<LevelObjectBase, Queue<LevelObjectBase>>();
        }

        public void AddObjectToPool(LevelObjectBase prefab, LevelObjectBase obj)
        {
            if (!objectPool.ContainsKey(prefab))
            {
                objectPool[prefab] = new Queue<LevelObjectBase>();
            }

            objectPool[prefab].Enqueue(obj);
        }

        public LevelObjectBase GetObjectFromPool(LevelObjectBase prefab)
        {
            if (objectPool.ContainsKey(prefab) && objectPool[prefab].Count > 0)
            {
                var obj = objectPool[prefab].Dequeue();
                obj.gameObject.SetActive(true);
                return obj;
            }
            return null;
        }

        public void ReturnObjectToPool(LevelObjectBase prefab, LevelObjectBase obj)
        {
            obj.gameObject.SetActive(false);

            if (!objectPool.ContainsKey(prefab))
            {
                objectPool[prefab] = new Queue<LevelObjectBase>();
            }

            objectPool[prefab].Enqueue(obj);
        }
    }
}
