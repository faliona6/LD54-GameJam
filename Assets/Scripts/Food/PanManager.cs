using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Food
{
    public class PanManager : MonoBehaviour
    {
        private List<Slot> _cookingSlots = new();
        private SlotGrid _slotGrid;

        void Start()
        {
            Player instance = Player.Instance;
            _slotGrid = instance.PlayerInventory.slotGrid;

            _cookingSlots = _slotGrid.GetSlotsOfType(Slot.SlotType.Cooking);
        }

        public void HandleStartCooking()
        {
            List<Ingredient> ingredients = GetIngredientsToCook();
            ingredients.ForEach(ingredient => ingredient.Cook());
        }
        
        private List<Ingredient> GetIngredientsToCook()
        {
            List<Ingredient> ingredientsToCook = new List<Ingredient>();

            foreach (Slot slot in _cookingSlots)
            {
                // Check if the slot's ingredient is not already in the list before adding.
                if (slot.Ingredient != null && !ingredientsToCook.Contains(slot.Ingredient))
                {
                    ingredientsToCook.Add(slot.Ingredient);
                }
                //TODO: check to make sure entire ingredient is in pan
                
            }

            return ingredientsToCook;
        }
    }
}