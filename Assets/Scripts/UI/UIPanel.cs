using UnityEngine;

namespace Picker3D.UI
{
    public class UIPanel : MonoBehaviour
    {
        [SerializeField] private GameObject panel;

        public virtual void Show()
        {
            panel.SetActive(true);
        }

        public virtual void Hide() 
        {
            panel.SetActive(false);
        }
    }
}