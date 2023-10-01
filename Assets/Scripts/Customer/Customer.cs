using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Customer
{
    public class Customer : MonoBehaviour
    {
        public CustomerPlatePool platePool; // Reference to the CustomerPlatePool.

        public Plate currentPlatePrefab; // To keep track of the plate the customer currently has.
        public Plate currentPlate; // To keep track of the plate the customer currently has.
        public Dictionary<string, int> flavors = new Dictionary<string, int>();
        public Dictionary<string, int> ingredientTypes = new Dictionary<string, int>();

        void OnDestroy()
        {
            // Release the plate back to the pool
            if (platePool != null && currentPlate != null)
            {
                platePool.ReleasePlate(currentPlatePrefab);
                currentPlatePrefab = null;
            }

            // Destroy the current plate GameObject if it exists
            if (currentPlate != null)
            {
                Debug.Log("Destroying Plate");
                Destroy(currentPlate.gameObject);
                currentPlate = null;
            }
        }

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
        public void RequestPlate()
        {
            if (platePool != null)
            {
                currentPlatePrefab = platePool.GetRandomPlate();
                if (currentPlatePrefab != null)
                {
                    Plate currentPlateObj = Instantiate(currentPlatePrefab);
                    currentPlate = currentPlateObj.GetComponent<Plate>();
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
