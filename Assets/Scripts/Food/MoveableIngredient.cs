using System.Collections.Generic;
using UnityEngine;

namespace Food
{
    [RequireComponent(typeof(Ingredient))]
    public class MoveableIngredient : MonoBehaviour, IMoveable {
        public List<Transform> nearestSnappableObjs = new List<Transform>();
        public bool isLocked = false;

        Ingredient _ingredient;

        Slot _orignalSlot;

        public Transform Transform => transform;

        void Awake() { _ingredient = GetComponent<Ingredient>(); }

        public Transform Drop() {
            // Snap to nearest Slot
            float minDistance = int.MaxValue;
            Slot snapSlot = null;
            for (int i = 0; i < nearestSnappableObjs.Count; i++) {
                Transform near = nearestSnappableObjs[i];
                // // For keepCard recipes, a nearest card could be destroyed, but ref to it remains in nearestSnappableObjs.
                // // This cleans up those refs... Other solution could be moving object to be destroyed far away to trigger OnTriggerExit?
                // if (near == null) {
                //     nearestSnappableObjs.Remove(near);
                //     continue;
                // }

                float d = Vector3.Distance(transform.position, near.transform.position);
                if (near.TryGetComponent(out Slot slot)) {
                    if (slot.IsEmpty() && d < minDistance) {
                        minDistance = d;
                        snapSlot = slot;
                    }
                }
            }

            // Snap to Slot
            if (snapSlot && snapSlot.Place(_ingredient)) { // Try placing in slot grid
                // If valid new position, move to slot
                return snapSlot.transform;
            }

            return _orignalSlot == null ? transform :
                // Place in original slot
                _orignalSlot.transform;
        }

        public Transform PickUp() {
            Transform slot = transform.parent;
            if (slot != null) {
                if (isLocked || !slot.GetComponent<Slot>().PickUpHeld()) { // failed to pickup from slot, such as when slot is locked
                    return null;
                }
                _orignalSlot = slot.GetComponent<Slot>();
            }

            return transform;
        }

        void OnTriggerEnter2D(Collider2D col) {
            if (col.gameObject.layer == LayerMask.NameToLayer("Slot") && !nearestSnappableObjs.Contains(col.transform)) {
                nearestSnappableObjs.Add(col.transform);
            }
        }
        void OnTriggerExit2D(Collider2D col) {
            if (col.gameObject.layer == LayerMask.NameToLayer("Slot") && nearestSnappableObjs.Contains(col.transform)) {
                nearestSnappableObjs.Remove(col.transform);
            }
        }
    }
}