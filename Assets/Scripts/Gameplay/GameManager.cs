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
        public Picker Picker { get; private set; }

        private LevelManager m_LevelManager;

        [Inject]
        public void Construct(UIManager uiManager, Picker picker, LevelManager levelManager)
        { 
            UIManager = uiManager;
            Picker = picker;
            m_LevelManager = levelManager;
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
            Picker.MovementController.Move();
        }

        public void StartNextLevel()
        {
            Picker.MovementController.Move();
            m_LevelManager.ClearPreviousAndLoadNextLevel();

            UIManager.GameplayPanel.UpdateLevelText(LevelManager.GetCurrentLevelIndex() + 1);
        }

        public void RestartLevel()
        {
            Picker.transform.position = m_LevelManager.GetCurrentLevelStartPosition();
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