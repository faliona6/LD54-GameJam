using UnityEngine;

namespace UI
{
    public class Window : MonoBehaviour 
    {
        [SerializeField] private bool showOnStart = true;

        private bool _toggle = true;

        private void Start()
        {
            if (showOnStart)
            {
                Show();
            }
            else 
            {
                Hide();
            }
        }

        public void Toggle() {
            if (_toggle) {
                Show();
            } else {
                Hide();
            }

            _toggle = !_toggle;
        }
    
        public void Show() { gameObject.SetActive(true); }

        public void Hide() { gameObject.SetActive(false); }
    }
}