using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMushroomMovement : MonoBehaviour
{
    [SerializeField] private float distanceScalar = 0.025f;

    private Vector3 centerScreen; // Center of the screen
    private float distanceFromCenter; // Distance from the center to the mouse position
    private Vector3 originalPosition;

    private void Start()
    {
        // Calculate the center of the screen
        centerScreen = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);
        var localPosition = transform.localPosition;
        originalPosition = new Vector3(localPosition.x, localPosition.y, 0);
    }

    private void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 distanceScaled = (mousePosition - centerScreen) * distanceScalar;
        transform.localPosition = originalPosition + distanceScaled;
    }
}
