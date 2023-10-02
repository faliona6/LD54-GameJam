using System;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

namespace Food
{

    [RequireComponent(typeof(Ingredient))]
    public class MoveableIngredient : MonoBehaviour, IMoveable {
        public List<Transform> nearestSnappableObjs = new List<Transform>();

        Ingredient _ingredient;

        Slot _orignalSlot;

        public bool isLocked = false;

        public Transform Transform => transform;

        void Awake() { _ingredient = GetComponent<Ingredient>(); }

        private void OnDestroy()
        {
            DOTween.Kill(this);
        }

        public Transform Drop() {
            // Snap to nearest Slot
            float minDistance = int.MaxValue;
            Slot snapSlot = null;
            for (int i = 0; i < nearestSnappableObjs.Count; i++) {
                Transform near = nearestSnappableObjs[i];

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
            } else if (_orignalSlot) { // Place in original slot if available
                return _orignalSlot.transform;
            } else { // Keep in hand
                return null;
            }
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

        public void Trash()
        {
            transform.DOScale(Vector3.zero, 0.3f)
                .SetEase(Ease.OutCubic)
                .SetId(this)
                .OnComplete(() => Destroy(this));
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