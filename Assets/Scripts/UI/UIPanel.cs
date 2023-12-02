using UnityEngine;

namespace Picker3D.UI
{
    public class UIPanel : MonoBehaviour
    {
        [SerializeField] private GameObject panel;

        private void Start()
        {
            Hide();
        }

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