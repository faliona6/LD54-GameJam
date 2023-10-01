using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : MonoBehaviour {
    bool _toggle = true;
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