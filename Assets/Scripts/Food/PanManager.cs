using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomGrid;
using UnityEngine.UI;

namespace Food
{
    public class PanManager : MonoBehaviour
    {
        public int panCount;
        public Button[] cookButtons;

        void Start()
        {
            cookButtons = GetComponentsInChildren<Button>();
        }
    }
}