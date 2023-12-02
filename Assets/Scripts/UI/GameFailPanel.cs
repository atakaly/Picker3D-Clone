using Picker3D.Gameplay;

namespace Picker3D.UI
{
    public class GameFailPanel : UIPanel
    {
        private void Start()
        {
            Hide();
        }

        public void OnRestartLevelClick()
        {
            GameManager.instance.RestartLevel();
            Hide();
        }
    }
}