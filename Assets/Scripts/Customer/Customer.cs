using System;
using System.Collections.Generic;
using Food;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Customer {
    public class Customer : MonoBehaviour {
        public GameObject currentPlate; // To keep track of the plate the customer currently has.

        public Dictionary<FoodFlavors, int> flavorThreshold = new Dictionary<FoodFlavors, int>();
        public Dictionary<FoodType, int> ingredientTypesThreshold = new Dictionary<FoodType, int>();
        
        CustomerPlatePool platePool;

        public UnityEvent OnPlateSuccess = new UnityEvent();
        public UnityEvent OnPlateFail = new UnityEvent();

        // Start is called before the first frame update
        void Start() {
            platePool = GameManager.Instance.CustomerManager.platePool;
            
            RequestPlate();

            foreach (FoodFlavors flavor in Enum.GetValues(typeof(FoodFlavors))) {
                flavorThreshold[flavor] = 0;
            }

            GenerateFlavors();

            foreach (FoodType ingredientType in Enum.GetValues(typeof(FoodType))) {
                ingredientTypesThreshold[ingredientType] = 0;
            }

            GenerateIngredientTypes();
            
            foreach (KeyValuePair<Vector2Int, Slot> slot in currentPlate.GetComponent<Plate>().slotGrid.slotGrid) {
                slot.Value.OnSlotPlaced.AddListener(CheckIngredients);
            }
        }

        public void CheckIngredients() {
            int totalSalty = 0, totalSweet = 0, totalSour = 0;
            List<Ingredient> ingredients = currentPlate.GetComponent<Plate>().slotGrid.GetIngredients();
            foreach (Ingredient ingredient in ingredients) {
                totalSalty += ingredient.salty;
                totalSweet += ingredient.sweet;
                totalSour += ingredient.sour;
            }

            if (totalSalty >= flavorThreshold[FoodFlavors.Salty] &&
                totalSweet >= flavorThreshold[FoodFlavors.Sweet] &&
                totalSour >= flavorThreshold[FoodFlavors.Sour]) {
                OnPlateSuccess.Invoke();
            } else {
                OnPlateFail.Invoke();
            }
        }

        // Method to request a random plate from the pool.
        public void RequestPlate()
        {
            if (platePool != null)
            {
                currentPlate = platePool.GetRandomPlate();
                currentPlate.transform.SetParent(transform);
                currentPlate.transform.transform.localPosition = Vector3.zero;
            }
            else
            {
                Debug.LogError("Plate Pool not assigned to the customer.");
            }
        }

        private int GetNumberOfTiles() {
            int numberOfTilesAccum = 0;
            for (int i = 0; i < currentPlate.GetComponent<Plate>().container.matrix.Count; i++)
            {
                for (int j = 0; j < currentPlate.GetComponent<Plate>().container.matrix[i].columns.Count; j++)
                {
                    if (currentPlate.GetComponent<Plate>().container.matrix[i].columns[j] == 1)
                    {
                        numberOfTilesAccum++;
                    }
                }
            }

            return numberOfTilesAccum;
        }

        public void GenerateFlavors() {
            // Get Max Flavors based on size of panels
            int numberOfTiles = GetNumberOfTiles();
            int maxFlavors = numberOfTiles / 2;
            int numberOfFlavors = Random.Range(1, maxFlavors + 1);

            System.Random random = new System.Random();
            Array enumValues = Enum.GetValues(typeof(FoodFlavors));
            for (int i = 0; i < numberOfFlavors; i++) {
                FoodFlavors randomFlavor = (FoodFlavors) enumValues.GetValue(random.Next(enumValues.Length));
                flavorThreshold[randomFlavor]++;
            }
        }

        public void GenerateIngredientTypes() {
            // TODO: Get Max IngredientTypes based on size of panels
            int numberOfTiles = GetNumberOfTiles();
            int maxIngredientTypes = numberOfTiles / 3;

            // Max will be 1 be default if it's 0
            if (maxIngredientTypes == 0) {
                maxIngredientTypes = 1;
            }

            int numberOfIngredientTypes = Random.Range(1, maxIngredientTypes + 1);

            System.Random random = new System.Random();
            Array enumValues = Enum.GetValues(typeof(FoodType));
            for (int i = 0; i < numberOfIngredientTypes; i++) {
                FoodType randomIngredientType = (FoodType) enumValues.GetValue(random.Next(enumValues.Length));
                ingredientTypesThreshold[randomIngredientType]++;
            }
        }
        
        void OnDestroy()
        {
            // Release the plate back to the pool
            if (platePool != null && currentPlate != null)
            {
                platePool.ReleasePlate(currentPlate);
                Destroy(currentPlate);
                currentPlate = null;

            }
        }
    }
}