using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Customer
{
    public class Customer : MonoBehaviour
    {
        private CustomerPlatePool platePool; // Reference to the CustomerPlatePool.

        public Plate currentPlate { get; private set; } // To keep track of the plate the customer currently has.
        public Dictionary<string, int> flavors = new Dictionary<string, int>();
        public Dictionary<string, int> ingredientTypes = new Dictionary<string, int>();

        // Start is called before the first frame update
        void Start()
        {
            RequestPlate();
            GenerateFlavors();
            GenerateIngredientTypes();
        }

        void Update() {

        }

        // Method to request a random plate from the pool.
        private void RequestPlate()
        {
            if (platePool != null)
            {
                if (currentPlate != null)
                {
                    currentPlate = platePool.GetRandomPlate();
                    if (currentPlate != null)
                    {
                        Debug.Log($"Customer got a plate with prefab named: {currentPlate.prefabName}");
                    }
                    else
                    {
                        Debug.LogWarning("No plates available in the pool!");
                    }
                }
                else
                {
                    Debug.LogWarning("Customer already has a plate!");
                }
            }
            else
            {
                Debug.LogError("Plate Pool not assigned to the customer.");
            }
        }
        public void GenerateFlavors()
        {
            //string flavor = FlavorsManager.Instance.GetRandomFlavor();
            // Use the flavor as needed
            // ...
        }

        public void GenerateIngredientTypes()
        {
            //string ingredients = IngredientsManager.Instance.GetRandomIngredients();
            // Use the flavor as needed
            // ...
        }
    }
}
