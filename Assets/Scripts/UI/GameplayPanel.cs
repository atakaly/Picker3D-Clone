using TMPro;
using UnityEngine;

namespace Picker3D.UI
{
    public class GameplayPanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI currentLevelTextMesh;

        public void UpdateLevelText(int level)
        {
            currentLevelTextMesh.text = "Level " + level.ToString();
        }
    }
}