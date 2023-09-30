using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Customer
{
    public class Customer : MonoBehaviour
    {
        public CustomerPlatePool platePool; // Reference to the CustomerPlatePool.

        public Plate CurrentPlate { get; private set; } // To keep track of the plate the customer currently has.

        // Method to request a random plate from the pool.
        public void RequestPlate()
        {
            if (platePool != null)
            {
                CurrentPlate = platePool.GetRandomPlate();
                if (CurrentPlate != null)
                {
                    Debug.Log($"Customer got a plate with prefab named: {CurrentPlate.prefabName}");
                }
                else
                {
                    Debug.LogWarning("No plates available in the pool!");
                }
            }
            else
            {
                Debug.LogError("Plate Pool not assigned to the customer.");
            }
        }
    }
}
