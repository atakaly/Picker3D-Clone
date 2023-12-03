using Picker3D.Installers;
using Picker3D.LevelEditor;
using System.Collections;
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

        private Level m_CurrentLevel;
        private Level m_NextLevel;

        private List<LevelDataSO> loadedLevels;
        private LevelObjectPool m_LevelObjectPool;

        private List<Level> levelPool;

        [Inject]
        public LevelManager(GlobalGameData globalGameData, LevelObjectPool levelObjectPool)
        {
            m_GlobalGameData = globalGameData;

            loadedLevels = new List<LevelDataSO>();
            m_LevelObjectPool = levelObjectPool;
            levelPool = new List<Level>();
        }

        public void Initialize()
        {
            LoadGame();
        }

        public void LoadGame()
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

            loadedLevels.Add(levelData);

            m_CurrentLevel = CreateLevel(levelData);
        }

        public Level LoadNextLevel()
        {
            int currentLevelIndex = GetCurrentLevelIndex() + 1;
            int levelsCount = m_GlobalGameData.AllLevelDatas.Count;
            var levelDataIndex = currentLevelIndex >= levelsCount ? Random.Range(0, levelsCount) : currentLevelIndex;

            var levelData = m_GlobalGameData.AllLevelDatas[levelDataIndex];

            loadedLevels.Add(levelData);

            m_NextLevel = CreateLevel(levelData);
            m_NextLevel.transform.position = new Vector3(0, 0, GetCurrentTotalLevelLength());

            return m_NextLevel;
        }

        public void ClearPreviousAndLoadNextLevel()
        {
            m_CurrentLevel.ClearLevel();
            ReturnLevelToPool(m_CurrentLevel);

            m_CurrentLevel = m_NextLevel;
            m_NextLevel = LoadNextLevel();
        }

        public void ReinitializeLevels()
        {
            m_CurrentLevel.ResetLevel();
        }

        private Level CreateLevel(LevelDataSO levelData)
        {
            Level currentLevel;

            if (levelPool.Count > 0)
            {
                currentLevel = levelPool[0];
                levelPool.RemoveAt(0);
                currentLevel.gameObject.SetActive(true);
                currentLevel.gameObject.name = levelData.name;
            }
            else
            {
                currentLevel = new GameObject(levelData.name).AddComponent<Level>();
            }

            currentLevel.Initialize(levelData, m_LevelObjectPool);

            return currentLevel;
        }

        public void ReturnLevelToPool(Level level)
        {
            level.ResetLevel();
            level.gameObject.SetActive(false);
            levelPool.Add(level);
        }

        public static int GetCurrentLevelIndex()
        {
            return PlayerPrefs.GetInt(CURRENT_LEVEL_PREF_NAME, 0);
        }

        public Vector3 GetCurrentLevelStartPosition()
        {
            float totalZLength = GetCurrentTotalLevelLength();
            float currentLevelZStartPosition = totalZLength - loadedLevels[loadedLevels.Count - 1].GetLevelZDistance();

            return new Vector3(0f, 0f, currentLevelZStartPosition);
        }

        private float GetCurrentTotalLevelLength()
        {
            float totalLength = 0f;

            for (int i = 1; i < loadedLevels.Count; i++)
            {
                totalLength += loadedLevels[i].GetLevelZDistance();
            }

            return totalLength;
        }
    }
}
