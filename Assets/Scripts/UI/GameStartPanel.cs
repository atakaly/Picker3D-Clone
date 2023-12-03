using System;

namespace Picker3D.UI
{
    public class GameStartPanel : UIPanel
    {
        public event Action OnDragged;

        public void StartLevel()
        {
            OnDragged?.Invoke();
            Hide();
        }
    }
}