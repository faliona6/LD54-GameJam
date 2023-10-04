using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CompactFlavorPanel : MonoBehaviour {
    [SerializeField] TextMeshProUGUI saltRequestText;
    [SerializeField] TextMeshProUGUI sweetRequestText;
    [SerializeField] TextMeshProUGUI sourRequestText;

    public void DisplayRequest(int saltRequestNum, int sweetRequestNum, int sourRequestNum) {
        saltRequestText.text = "x" + saltRequestNum.ToString();
        sweetRequestText.text = "x" + sweetRequestNum.ToString();
        sourRequestText.text = "x" + sourRequestNum.ToString();
    }
}
