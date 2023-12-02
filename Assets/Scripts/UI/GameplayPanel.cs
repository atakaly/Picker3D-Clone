using Picker3D.LevelManagement;
using TMPro;
using UnityEngine;

namespace Picker3D.UI
{
    public class GameplayPanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI currentLevelTextMesh;

        public void UpdateLevelText()
        {
            currentLevelTextMesh.text = "Level " + PlayerPrefs.GetInt(LevelManager.CURRENT_LEVEL_PREF_NAME);
        }
    }
}