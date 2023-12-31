using System;
using System.Collections.Generic;
using DG.Tweening;
using Food;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;
using UnityEngine.UI;
using UI;
using Unity.VisualScripting;
using UnityEngine.PlayerLoop;

namespace Customer {
    public class Customer : MonoBehaviour {
        public GameObject currentPlate; // To keep track of the plate the customer currently has.
        [SerializeField] private GameObject timerPrefab;
        [SerializeField] private GameObject plateBubble;
        [SerializeField] private Transform activePlateContainer;
        [SerializeField] private Transform previewPlateContainer;
        [SerializeField] private CompactFlavorPanel flavorRequestPanel;

        public Dictionary<FoodFlavors, int> flavorThreshold = new Dictionary<FoodFlavors, int>();
        public Dictionary<FoodType, int> ingredientTypesThreshold = new Dictionary<FoodType, int>();
        public bool isActive = false;
        
        CustomerPlatePool platePool;

        public UnityEvent<int> OnPlateSuccess = new();
        public UnityEvent OnPlateFail = new();
        
        private void Awake()
        {
            platePool = GameManager.Instance.CustomerManager.platePool;
            GameManager.Instance.CustomerManager.OnCustomersChanged += HandleCustomersChanged;
        }

        // Start is called before the first frame update
        void Start() {
            //RequestPlate();

            foreach (FoodFlavors flavor in Enum.GetValues(typeof(FoodFlavors))) {
                flavorThreshold[flavor] = 0;
            }

            GenerateFlavors();

            foreach (FoodType ingredientType in Enum.GetValues(typeof(FoodType))) {
                ingredientTypesThreshold[ingredientType] = 0;
            }

            GenerateIngredientTypes();
        }

        public void CheckIngredients() {
            int totalSalty = 0, totalSweet = 0, totalSour = 0;
            Plate curPlate = currentPlate.GetComponent<Plate>();
            List<Ingredient> ingredients = curPlate.slotGrid.GetIngredients();
            foreach (Ingredient ingredient in ingredients) {
                totalSalty += ingredient.salty;
                totalSweet += ingredient.sweet;
                totalSour += ingredient.sour;
            }
            
            // Check slots empty
            bool allSlotsFilled = true;
            foreach (var (pos, slot) in curPlate.slotGrid.slotGrid) {
                if (slot.IsEmpty()) allSlotsFilled = false;
            }

            if (totalSalty >= flavorThreshold[FoodFlavors.Salty] &&
                totalSweet >= flavorThreshold[FoodFlavors.Sweet] &&
                totalSour >= flavorThreshold[FoodFlavors.Sour] && allSlotsFilled) {
                OnPlateSuccess.Invoke(currentPlate.GetComponent<Plate>().slotGrid.slotGrid.Count * 5);
            }
        }

        // Method to request a random plate from the pool.
        public void RequestPlate(Transform parentTransform)
        {
            if (platePool != null)
            {
                currentPlate = platePool.GetRandomPlate(parentTransform);
                currentPlate.GetComponent<Plate>().Init();
                foreach (KeyValuePair<Vector2Int, Slot> slot in currentPlate.GetComponent<Plate>().slotGrid.slotGrid) {
                    slot.Value.OnSlotPlaced.AddListener(CheckIngredients);
                }
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

        public void HandleCustomersChanged()
        {
            isActive = GameManager.Instance.CustomerManager.GetActiveCustomer().Equals(this);

            Transform current = isActive ? activePlateContainer : previewPlateContainer;
            Transform disabled = isActive ? previewPlateContainer : activePlateContainer;
            
            current.gameObject.SetActive(true);
            disabled.gameObject.SetActive(false);

            RequestPlate(current);
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
            
            flavorRequestPanel.DisplayRequest(flavorThreshold[FoodFlavors.Salty], flavorThreshold[FoodFlavors.Sweet], flavorThreshold[FoodFlavors.Sour]);
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