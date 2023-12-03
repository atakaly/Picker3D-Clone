using System;

namespace Picker3D.UI
{
    public class GameFailPanel : UIPanel
    {
        public event Action OnRestartLevelClicked;

        private void Start()
        {
            Hide();
        }

        public void OnRestartLevelClick()
        {
            OnRestartLevelClicked?.Invoke();
            Hide();
        }
    }
}