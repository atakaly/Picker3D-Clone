using Picker3D.Gameplay.PickerSystem;
using Picker3D.LevelManagement;
using Picker3D.UI;
using UnityEngine;
using Zenject;

namespace Picker3D.Gameplay
{
    public class GameManager : MonoSingleton<GameManager>
    {
        public UIManager UIManager { get; private set; }
        public LevelObjectPool LevelObjectPool { get; private set; }

        public Picker m_Picker;

        private LevelManager m_LevelManager;

        [Inject]
        public void Construct(UIManager uiManager, Picker picker, LevelManager levelManager, LevelObjectPool levelObjectPool)
        { 
            UIManager = uiManager;
            m_Picker = picker;
            m_LevelManager = levelManager;
            LevelObjectPool = levelObjectPool;
        }

        private void Awake()
        {
            UIManager.SuccessPanel.OnPlayNextLevelClicked += StartNextLevel;
            UIManager.FailPanel.OnRestartLevelClicked += RestartLevel;
            UIManager.StartPanel.OnDragged += StartLevel;

            UIManager.GameplayPanel.UpdateLevelText(LevelManager.GetCurrentLevelIndex() + 1);
        }

        public void LevelSucceed()
        {
            UIManager.SuccessPanel.Show();

            int newLevelIndex = LevelManager.GetCurrentLevelIndex() + 1;
            PlayerPrefs.SetInt(LevelManager.CURRENT_LEVEL_PREF_NAME, newLevelIndex);
        }

        public void LevelFailed()
        {
            UIManager.FailPanel.Show();
        }

        public void StartLevel()
        {
            m_Picker.MovementController.Move();
        }

        public void StartNextLevel()
        {
            m_Picker.MovementController.Move();
            m_LevelManager.ClearPreviousAndLoadNextLevel();

            UIManager.GameplayPanel.UpdateLevelText(LevelManager.GetCurrentLevelIndex() + 1);
        }

        public void RestartLevel()
        {
            m_Picker.transform.position = m_LevelManager.GetCurrentLevelStartPosition();
            m_LevelManager.ReinitializeLevels();
            UIManager.StartPanel.Show();
        }

        private void OnDestroy()
        {
            UIManager.SuccessPanel.OnPlayNextLevelClicked -= StartNextLevel;
            UIManager.FailPanel.OnRestartLevelClicked -= RestartLevel;
            UIManager.StartPanel.OnDragged -= StartLevel;
        }
    }
}