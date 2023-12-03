using System;

namespace Picker3D.UI
{
    public class GameSuccessPanel : UIPanel
    {
        public event Action OnPlayNextLevelClicked;

        private void Start()
        {
            Hide();
        }

        public void OnPlayNextLevelClick()
        {
            OnPlayNextLevelClicked?.Invoke();
            Hide();
        }
    }
}