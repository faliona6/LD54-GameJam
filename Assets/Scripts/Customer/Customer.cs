using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Customer
{
    public class Customer : MonoBehaviour
    {
        public CustomerPlatePool platePool; // Reference to the CustomerPlatePool.

        public Plate currentPlate; // To keep track of the plate the customer currently has.
        public Dictionary<string, int> flavors = new Dictionary<string, int>();
        public Dictionary<string, int> ingredientTypes = new Dictionary<string, int>();

        // Start is called before the first frame update
        void Start()
        {
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
                currentPlate = platePool.GetRandomPlate();
                if (currentPlate != null)
                {
                    Debug.Log($"Customer got a plate with prefab named: {currentPlate.container.prefabName}");
                    Instantiate(currentPlate, new Vector3(0, 0, 0), Quaternion.identity);
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
