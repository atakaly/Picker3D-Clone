using Picker3D.Gameplay;
using Picker3D.Installers;
using Picker3D.LevelEditor;
using System.Collections.Generic;
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

        private Transform m_CurrentLevelTransform;

        private List<LevelDataSO> loadedLevels;

        [Inject]
        public LevelManager(GlobalGameData globalGameData, LevelObjectPool levelObjectPool)
        {
            m_GlobalGameData = globalGameData;
            m_LevelObjectPool = levelObjectPool;

            loadedLevels = new List<LevelDataSO>();
        }

        public void Initialize()
        {
            LoadCurrentLevel();
            LoadNextLevel();
        }

        public void LoadCurrentLevel()
        {
            int currentLevelIndex = GetCurrentLevelIndex();
            int levelsCount = m_GlobalGameData.AllLevelDatas.Count;
            var levelDataIndex = currentLevelIndex >= levelsCount ? Random.Range(0, levelsCount) : currentLevelIndex;

            var levelData = m_GlobalGameData.AllLevelDatas[levelDataIndex];

            m_CurrentLevelTransform = CreateLevel(levelData).transform;
        }

        public Transform LoadNextLevel()
        {
            int currentLevelIndex = GetCurrentLevelIndex() + 1;
            int levelsCount = m_GlobalGameData.AllLevelDatas.Count;
            var levelDataIndex = currentLevelIndex >= levelsCount ? Random.Range(0, levelsCount) : currentLevelIndex;

            var levelData = m_GlobalGameData.AllLevelDatas[levelDataIndex];

            loadedLevels.Add(levelData);

            return CreateLevel(levelData).transform;
        }

        public void ClearPreviousLevel()
        {
            for (int i = 0; i < m_CurrentLevelTransform.childCount; i++)
            {
                m_LevelObjectPool.ReturnObjectToPool(m_CurrentLevelTransform.GetChild(i).GetComponent<LevelObjectBase>(), m_CurrentLevelTransform.GetChild(i).GetComponent<LevelObjectBase>());
            }

            m_CurrentLevelTransform = LoadNextLevel();
        }

        private GameObject CreateLevel(LevelDataSO levelData)
        {
            GameObject currentLevelParent = new GameObject(levelData.name);

            for (int i = 0; i < levelData.ObjectsInLevel.Count; i++)
            {
                var prefab = levelData.ObjectsInLevel[i].LevelObject;
                var newPosition = levelData.ObjectsInLevel[i].Position + Vector3.forward * GetCurrentTotalLevelLength();
                var newRotation = Quaternion.Euler(levelData.ObjectsInLevel[i].Rotation);

                var newObject = m_LevelObjectPool.GetObjectFromPool(prefab);
                if (newObject == null)
                {
                    newObject = Object.Instantiate(prefab, currentLevelParent.transform);
                }

                newObject.transform.SetPositionAndRotation(newPosition, newRotation);
            }

            return currentLevelParent;
        }

        public static int GetCurrentLevelIndex()
        {
            return PlayerPrefs.GetInt(CURRENT_LEVEL_PREF_NAME, 0);
        }

        private float GetCurrentTotalLevelLength()
        {
            float totalLength = 0f;

            for (int i = 0; i < loadedLevels.Count; i++)
            {
                totalLength += loadedLevels[i].GetLevelZLength();
            }

            return totalLength;
        }
    }
}
