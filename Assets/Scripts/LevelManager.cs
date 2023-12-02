using Picker3D.Installers;
using Picker3D.LevelEditor;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Picker3D.LevelManagement
{
    public class LevelManager : IInitializable
    {
        public const string CURRENT_LEVEL_PREF_NAME = "current_level_index";

        private GlobalGameData m_GlobalGameData;
        private LevelObjectPool m_LevelObjectPool;

        [Inject]
        public LevelManager(GlobalGameData globalGameData, LevelObjectPool levelObjectPool)
        {
            m_GlobalGameData = globalGameData;
            m_LevelObjectPool = levelObjectPool;
        }

        public void Initialize()
        {
            LoadCurrentLevel();
        }

        public void LoadCurrentLevel()
        {
            int currentLevelIndex = GetCurrentLevelIndex();
            int levelsCount = m_GlobalGameData.AllLevelDatas.Count;
            var levelDataIndex = currentLevelIndex >= levelsCount ? Random.Range(0, levelsCount) : currentLevelIndex;

            var levelData = m_GlobalGameData.AllLevelDatas[levelDataIndex];

            CreateLevel(levelData);
        }

        private void CreateLevel(LevelDataSO levelData, int positionOffsetZ = 0)
        {
            for (int i = 0; i < levelData.ObjectsInLevel.Count; i++)
            {
                var prefab = levelData.ObjectsInLevel[i].LevelObject;
                var newPosition = levelData.ObjectsInLevel[i].Position + Vector3.forward * positionOffsetZ;
                var newRotation = Quaternion.Euler(levelData.ObjectsInLevel[i].Rotation);

                var newObject = m_LevelObjectPool.GetObjectFromPool(prefab);
                if (newObject == null)
                {
                    newObject = Object.Instantiate(prefab);
                }

                newObject.transform.SetPositionAndRotation(newPosition, newRotation);
            }
        }

        public static int GetCurrentLevelIndex()
        {
            return PlayerPrefs.GetInt(CURRENT_LEVEL_PREF_NAME, 0);
        }
    }
}
