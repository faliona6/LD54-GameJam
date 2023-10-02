using UnityEngine;

namespace UI
{
    public class Window : MonoBehaviour 
    {
        [SerializeField] private bool showOnStart = true;

        private bool _toggle = true;

        private void Start()
        {
            ShowHideWindow(showOnStart);
        }

        public void Toggle()
        {
            ShowHideWindow(_toggle);
            _toggle = !_toggle;
        }

        private void ShowHideWindow(bool show)
        {
            if (show) {
                Show();
            } else {
                Hide();
            }
        }
    
        public void Show() { gameObject.SetActive(true); }

        public void Hide() { gameObject.SetActive(false); }
    }
}