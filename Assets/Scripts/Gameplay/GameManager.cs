using Picker3D.LevelManagement;
using Picker3D.UI;
using UnityEngine;
using Zenject;

namespace Picker3D.Gameplay
{
    public class GameManager : MonoSingleton<GameManager>
    {
        public UIManager m_UIManager { get; private set; }

        [Inject]
        public void Construct(UIManager uiManager)
        {
            m_UIManager = uiManager;
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
    }
}