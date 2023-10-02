using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Food
{
    public class PanManager : MonoBehaviour
    {
        public Slider progressBar;
        public Button cookButton;
        
        private List<Slot> _cookingSlots = new();
        private SlotGrid _slotGrid;
        private bool isCooking = false;
        private bool isBurning = false;

        private TextMeshProUGUI cookButtonText;

        private Coroutine cookingCoroutine;
        private Coroutine burningCoroutine;

        void Start()
        {
            Player instance = Player.Instance;
            _slotGrid = instance.PlayerInventory.slotGrid;

            _cookingSlots = _slotGrid.GetSlotsOfType(Slot.SlotType.Cooking);
            progressBar.gameObject.SetActive(false);
            cookButtonText = cookButton.GetComponentInChildren<TextMeshProUGUI>();
        }

        public void HandleStartCooking()
        {
            if (isBurning)
            {
                StopBurning();
                return;
            }
            
            if (isCooking)
            {
                return;
            }
            
            cookButtonText.text = "COOKING";
            
            List<Ingredient> ingredients = GetIngredientsInPan();
            if (ingredients.Count == 0)
            {
                return;
            }

            cookButton.interactable = false;
            progressBar.gameObject.SetActive(true);
            isCooking = true;

            SetIngredientsLocked(ingredients, true);
            int totalCookTime = ingredients.Select(i => i.ingredientData.cookTime).Sum();

            cookingCoroutine = StartCoroutine(CookingTimer((int)(totalCookTime / ingredients.Count), ingredients));
        }

        private void FinishCooking(List<Ingredient> ingredients)
        {
            cookButtonText.text = "STOP";

            ingredients.ForEach(ingredient => ingredient.FinishCooking());
            isCooking = false;
            SetIngredientsLocked(ingredients, false);
            cookButton.interactable = true;
        }

        private void BurnFood(List<Ingredient> ingredients)
        {
            ingredients.ForEach(ingredient => ingredient.Burn());
        }

        private void StopBurning()
        {
            isBurning = false;
            StopCoroutine(burningCoroutine);
            progressBar.gameObject.SetActive(false);
            cookButtonText.text = "COOK!";
        }
        
        private List<Ingredient> GetIngredientsInPan()
        {
            List<Ingredient> ingredientsToCook = new List<Ingredient>();

            foreach (Slot slot in _cookingSlots)
            {
                // Check if the slot's ingredient is not already in the list before adding.
                if (slot.Ingredient != null && !ingredientsToCook.Contains(slot.Ingredient))
                {
                    ingredientsToCook.Add(slot.Ingredient);
                }
            }

            return ingredientsToCook;
        }

        private void SetIngredientsLocked(List<Ingredient> ingredients, bool isLocked)
        {
            ingredients.ForEach(ingredient =>
            {
                MoveableIngredient moveableIngredient = ingredient.GetComponent<MoveableIngredient>();
                if (moveableIngredient != null)
                {
                    moveableIngredient.isLocked = isLocked;
                }
            });
        }
        
        private IEnumerator CookingTimer(float totalCookTime, List<Ingredient> ingredientsInPan)
        {
            float timer = 0f;

            while (timer < totalCookTime)
            {
                // Increment the timer.
                timer += Time.deltaTime;
                progressBar.value = timer / totalCookTime;
                yield return null; 
            }

            FinishCooking(ingredientsInPan);
            burningCoroutine = StartCoroutine(BurnTimer(totalCookTime / 2));
        }
        
        private IEnumerator BurnTimer(float burnTime)
        {
            float timer = 0f;
            isBurning = true;

            while (timer < burnTime)
            {
                // Increment the timer.
                timer += Time.deltaTime;
                progressBar.value = timer / burnTime;
                yield return null;
            }

            List<Ingredient> currentIngredients = GetIngredientsInPan();
            BurnFood(currentIngredients);
            StopBurning();
        }
    }
}