using Picker3D.Gameplay;
using System;
using System.Collections.Generic;
using Zenject;

namespace Picker3D.LevelManagement
{
    public class LevelObjectPool : IInitializable
    {
        private Dictionary<Type, Queue<LevelObjectBase>> objectPool;

        public void Initialize()
        {
            objectPool = new Dictionary<Type, Queue<LevelObjectBase>>();
        }

        public void AddObjectToPool(Type type, LevelObjectBase obj)
        {
            if (!objectPool.ContainsKey(type))
            {
                objectPool[type] = new Queue<LevelObjectBase>();
            }

            objectPool[type].Enqueue(obj);
            obj.OnSpawn();
        }

        public LevelObjectBase GetObjectFromPool(Type type)
        {
            if (objectPool.ContainsKey(type) && objectPool[type].Count > 0)
            {
                var objQueue = objectPool[type];
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

        public void ReturnObjectToPool(Type type, LevelObjectBase obj)
        {
            obj.OnDespawn(); 
            obj.gameObject.SetActive(false);

            if (!objectPool.ContainsKey(type))
            {
                objectPool[type] = new Queue<LevelObjectBase>();
            }

            objectPool[type].Enqueue(obj);
        }
    }
}