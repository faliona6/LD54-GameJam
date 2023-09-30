using UnityEngine;

public class Slot : MonoBehaviour {
    public int x, y;
    public bool canPlace;
    public bool canPickUp;

    public Ingredient Ingredient => ingredient;
    [SerializeField] protected Ingredient ingredient;

    public SlotGrid SlotGrid => SlotGrid;
    [SerializeField] protected SlotGrid mSlotGrid;
    
    public void A() {
    }
    
    
    // PlaceAndMove handles registering an Ingredient with the slot and physically moving stack's location to this Slot
    // Slots only allow stacks of one card (for now?)
    // public virtual bool PlaceAndMove(Ingredient ingredient, bool isPlayerCalled = false) {
    //     if (!IsEmpty()) { return false; }
    //
    //     // Set slot fields
    //     this.ingredient = ingredient;
    //
    //     // Set ingredient fields
    //     // ingredient.
    //
    //     // Move card to slot (negative StackDepthOffset in Z bc child of slot when placed, slot is rotated)
    //     StartCoroutine(Utils.MoveStackToPoint(stack, new Vector3(0,0,-Constants.StackDepthOffset)));
    //     
    //     // Send event
    //     EventManager.Invoke(gameObject, EventID.SlotPlaced, this);
    //     // EventManager.Invoke(card.gameObject, EventID.CardPlaced);
    //
    //     return true;
    // }
    //
    // public bool PickUpCondition(bool isPlayerCalled = false) { return IsEmpty() || (isPlayerCalled && !canPickUp); }
    //
    // public virtual Transform PickUpHeld(bool isPlayerCalled = false, bool endCombatState = false, bool doEventInvoke = true) {
    //     if (PickUpCondition(isPlayerCalled)) return null;
    //     
    //     // Set card fields
    //     card.mSlot = null;
    //     card.mStack.transform.SetParent(null);
    //     if (card.TryGetComponent(out MoveableCard m)) {
    //         m.IsStackable = cardWasStackable;
    //         cardWasStackable = false;
    //     }
    //
    //     // Send event
    //     if (doEventInvoke) {
    //         EventManager.Invoke(card.gameObject, EventID.CardPickedUp);
    //         EventManager.Invoke(gameObject, EventID.SlotPickedUp, this);
    //     }
    //
    //     // Set slot fields
    //     Stack s = stack;
    //     stack = null;
    //     card = null;
    //     
    //     return s.transform;
    // }
    //
    // public virtual Stack SpawnCard(SO_Card cardData) {
    //     if (!IsEmpty()) return null;
    //     
    //     Stack s = CardFactory.CreateStack(transform.position, cardData);
    //     PlaceAndMove(s);
    //
    //     return s;
    // }
    //
    // public bool IsEmpty() {
    //     return card == null;
    // }
}