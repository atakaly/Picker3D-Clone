using Picker3D.Gameplay;
using Picker3D.Gameplay.BasketSystem;
using Picker3D.Gameplay.Collectibles;
using Picker3D.LevelEditor;
using System.Collections.Generic;
using UnityEngine;

namespace Picker3D.LevelManagement
{
    public class Level : MonoBehaviour
    {
        private LevelDataSO m_LevelData;
        public List<LevelObjectBase> levelObjects;

        private LevelObjectPool m_LevelObjectPool;

        public void Initialize(LevelDataSO levelData, LevelObjectPool levelObjectPool)
        {
            m_LevelData = levelData;
            m_LevelObjectPool = levelObjectPool;

            levelObjects = new List<LevelObjectBase>();

            CreateLevel();
        }

        private void CreateLevel()
        {
            for (int i = 0; i < m_LevelData.ObjectsInLevel.Count; i++)
            {
                var prefab = m_LevelData.ObjectsInLevel[i].LevelObject;
                var newPosition = m_LevelData.ObjectsInLevel[i].Position;
                var newRotation = Quaternion.Euler(m_LevelData.ObjectsInLevel[i].Rotation);
                var requiredBallCount = m_LevelData.ObjectsInLevel[i].RequiredBallCount;
                var meshType = m_LevelData.ObjectsInLevel[i].meshType;

                System.Type objectType = prefab.GetType();

                var newObject = m_LevelObjectPool.GetObjectFromPool(objectType);

                if (newObject == null)
                {
                    newObject = Instantiate(prefab);
                    m_LevelObjectPool.AddObjectToPool(objectType, newObject);
                }

                if(newObject is Basket basket)
                {
                    basket.RequiredBallCount = requiredBallCount;
                }

                if (newObject is CollectibleSet collectibleSet)
                {
                    collectibleSet.MeshType = meshType;
                }

                newObject.transform.SetPositionAndRotation(newPosition, newRotation);
                newObject.transform.SetParent(transform);
                levelObjects.Add(newObject);
            }
        }

        public void ResetLevel()
        {
            foreach (var obj in levelObjects)
            {
                obj.OnSpawn();
            }
        }

        public void ClearLevel()
        {
            for (int i = 0; i < levelObjects.Count; i++)
            {
                var levelObjectBase = levelObjects[i];

                System.Type objectType = levelObjectBase.GetType();
                GameManager.instance.LevelObjectPool.ReturnObjectToPool(objectType, levelObjectBase);
                levelObjectBase.transform.SetParent(null);
            }

            levelObjects.Clear();
        }
    }
}