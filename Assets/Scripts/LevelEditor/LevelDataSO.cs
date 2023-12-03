using Picker3D.Gameplay;
using Picker3D.Gameplay.Collectibles;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Picker3D.LevelEditor
{
    [CreateAssetMenu(menuName = "Levels/New Level", fileName = "New Level")]
    public class LevelDataSO : ScriptableObject
    {
        [Serializable]
        public struct InLevelObjectData
        {
            public LevelObjectBase LevelObject;
            public Vector3 Position;
            public Vector3 Rotation;
        }

        [Serializable]
        public struct LevelCollectiblesData
        {
            public InLevelObjectData inLevelObjectData;
            public MeshType meshType;
        }
        
        public int RequiredObjectCount = 10;
        public List<InLevelObjectData> ObjectsInLevel;
        public List<LevelCollectiblesData> CollectiblesInLevel;

        public float GetLevelZDistance()
        {
            for (int i = 0; i < ObjectsInLevel.Count; i++)
            {
                if (ObjectsInLevel[i].LevelObject is LevelEnd)
                {
                    return ObjectsInLevel[i].Position.z;
                }
            }

            return 0;
        }

    }
}