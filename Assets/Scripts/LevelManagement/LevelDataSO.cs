using Picker3D.Gameplay;
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
        
        public int RequiredObjectCount = 10;
        public List<InLevelObjectData> ObjectsInLevel;
    }
}