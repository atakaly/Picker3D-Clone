using Picker3D.Gameplay;

namespace Picker3D.UI
{
    public class GameSuccessPanel : UIPanel
    {
        private void Start()
        {
            Hide();
        }

        public void OnPlayNextLevelClick()
        {
            GameManager.instance.StartNextLevel();
            Hide();
        }
    }
}