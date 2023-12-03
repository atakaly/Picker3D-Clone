using Picker3D.Gameplay;
using System;
using System.Collections.Generic;
using Zenject;

namespace Picker3D.LevelManagement
{
    public class LevelObjectPool : IInitializable
    {
        private Dictionary<string, Queue<LevelObjectBase>> objectPool;

        public void Initialize()
        {
            objectPool = new Dictionary<string, Queue<LevelObjectBase>>();
        }

        public void AddObjectToPool(string uniqueId, LevelObjectBase obj)
        {
            if (!objectPool.ContainsKey(uniqueId))
            {
                objectPool[uniqueId] = new Queue<LevelObjectBase>();
            }

            objectPool[uniqueId].Enqueue(obj);
            obj.OnSpawn();
        }

        public LevelObjectBase GetObjectFromPool(string uniqueId)
        {
            if (objectPool.ContainsKey(uniqueId) && objectPool[uniqueId].Count > 0)
            {
                var objQueue = objectPool[uniqueId];
                foreach (var obj in objQueue)
                {
                    if (!obj.gameObject.activeInHierarchy)
                    {
                        obj.gameObject.SetActive(true);
                        obj.OnSpawn();
                        return obj;
                    }
                }
            }

            return null;
        }

        public void ReturnObjectToPool(string uniqueId, LevelObjectBase obj)
        {
            obj.OnDespawn(); 
            obj.gameObject.SetActive(false);

            if (!objectPool.ContainsKey(uniqueId))
            {
                objectPool[uniqueId] = new Queue<LevelObjectBase>();
            }

            objectPool[uniqueId].Enqueue(obj);
        }
    }
}