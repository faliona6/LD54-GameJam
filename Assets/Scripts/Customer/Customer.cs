using System;
using System.Collections;
using System.Collections.Generic;
using Food;
using Unity.VisualScripting;
using UnityEngine;
using Food;

namespace Customer
{
    public class Customer : MonoBehaviour
    {
        public CustomerPlatePool platePool; // Reference to the CustomerPlatePool.

        public Plate currentPlateObj; // To keep track of the plate the customer currently has.
        public Plate currentPlate; // To keep track of the plate the customer currently has.

        public Dictionary<FoodFlavors, int> flavorThreshold = new Dictionary<FoodFlavors, int>();
        public Dictionary<FoodType, int> ingredientTypesThreshold = new Dictionary<FoodType, int>();

        public void CheckIngredients() {
            Dictionary<string, int> curFlavors = new Dictionary<string, int>();
            List<Ingredient> ingredients = currentPlate.slotGrid.GetIngredients();
            foreach (Ingredient ingredient in ingredients) {
                // curFlavors[ingredient.]
            }
        }

        void OnDestroy()
        {
            // Release the plate back to the pool
            if (platePool != null && currentPlate != null)
            {
                platePool.ReleasePlate(currentPlateObj);
                currentPlateObj = null;
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

            foreach (FoodFlavors flavor in Enum.GetValues(typeof(FoodFlavors)))
            {
                flavorThreshold[flavor] = 0;
            }
            GenerateFlavors();

            foreach (FoodType ingredientType in Enum.GetValues(typeof(FoodType)))
            {
                ingredientTypesThreshold[ingredientType] = 0;
            }
            GenerateIngredientTypes();
        }

        void Update() {
            foreach (KeyValuePair<Vector2Int, Slot> slot in currentPlate.slotGrid.slotGrid) {
                slot.Value.OnSlotPlaced.AddListener(CheckIngredients);
            }
        }

        // Method to request a random plate from the pool.
        public void RequestPlate()
        {
            if (platePool != null)
            {
                currentPlateObj = platePool.GetRandomPlate();
                if (currentPlateObj != null)
                {
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
        
        private int GetNumberOfTiles()
        {
            int numberOfTilesAccum = 0;
            
            for (int i = 0; i < currentPlateObj.container.matrix.Count; i++)
            {
                for (int j = 0; j < currentPlateObj.container.matrix[i].columns.Count; j++)
                {
                    if (currentPlateObj.container.matrix[i].columns[j] == 1)
                    {
                        numberOfTilesAccum++;
                    }
                }
            }

            return numberOfTilesAccum;
        }

        public void GenerateFlavors()
        {
            // Get Max Flavors based on size of panels
            int numberOfTiles = GetNumberOfTiles();
            int maxFlavors = numberOfTiles / 2;
            int numberOfFlavors = UnityEngine.Random.Range(1, maxFlavors + 1);

            System.Random random = new System.Random();
            Array enumValues = Enum.GetValues(typeof(FoodFlavors));
            for (int i = 0;i < numberOfFlavors;i++)
            {
                FoodFlavors randomFlavor = (FoodFlavors) enumValues.GetValue(random.Next(enumValues.Length));
                flavorThreshold[randomFlavor]++;
            }
        }

        public void GenerateIngredientTypes()
        {
            // TODO: Get Max IngredientTypes based on size of panels
            int numberOfTiles = GetNumberOfTiles();
            int maxIngredientTypes = numberOfTiles / 3;

            // Max will be 1 be default if it's 0
            if (maxIngredientTypes == 0)
            {
                maxIngredientTypes = 1;
            }

            int numberOfIngredientTypes = UnityEngine.Random.Range(1, maxIngredientTypes + 1);
            
            System.Random random = new System.Random();
            Array enumValues = Enum.GetValues(typeof(FoodType));
            for (int i = 0; i < numberOfIngredientTypes; i++)
            {
                FoodType randomIngredientType = (FoodType)enumValues.GetValue(random.Next(enumValues.Length));
                ingredientTypesThreshold[randomIngredientType]++;
            }
        }
    }
}
