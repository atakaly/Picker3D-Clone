using Picker3D.Gameplay;

namespace Picker3D.UI
{
    public class GameStartPanel : UIPanel
    {
        public void StartLevel()
        {
            GameManager.instance.StartLevel();
            Hide();
        }
    }
}