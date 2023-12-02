﻿using Picker3D.Gameplay.PickerSystem;
using Picker3D.LevelManagement;
using Picker3D.UI;
using UnityEngine;
using Zenject;

namespace Picker3D.Gameplay
{
    public class GameManager : MonoSingleton<GameManager>
    {
        public UIManager m_UIManager { get; private set; }

        public Picker m_Picker;

        [Inject]
        public void Construct(UIManager uiManager, Picker picker)
        {
            m_UIManager = uiManager;
            m_Picker = picker;
        }

        public void LevelSucceed()
        {
            m_UIManager.SuccessPanel.Show();

            int newLevelIndex = LevelManager.GetCurrentLevelIndex() + 1;
            PlayerPrefs.SetInt(LevelManager.CURRENT_LEVEL_PREF_NAME, newLevelIndex);
        }

        public void LevelFailed()
        {
            m_UIManager.FailPanel.Show();
        }

        public void StartLevel()
        {
            m_Picker.MovementController.Move();
        }

        public void StartNextLevel()
        {

        }

        public void RestartLevel()
        {

        }
    }
}