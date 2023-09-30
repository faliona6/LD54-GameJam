using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField] LayerMask _moveableLayer;

    IMoveable _heldMoveable;
    Camera _mainCamera;

    void Awake() {
        _mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            // Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            // RaycastHit hit;
            Vector2 mousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, 100.0f, _moveableLayer);


            if (hit) {
                if (hit.collider != null) {
                    if (hit.collider.TryGetComponent(out IMoveable moveable)) {
                        Transform moveableTrans = moveable.PickUp();
                        if (moveableTrans) {
                            _heldMoveable = moveable;
                            StartCoroutine(FollowMouse(moveableTrans));
                        }
                    }
                }
            }
        }
        if (Input.GetMouseButtonUp(0)) {
            if (_heldMoveable != null) {
                Transform dropPos = _heldMoveable.Drop();
                StartCoroutine(Utils.MoveObjToPoint(_heldMoveable.Transform, dropPos.position));
                
                _heldMoveable = null;
            }
        }
    }

    ////////////////////////////////    Holding Objects    ///////////////////////////////

    IEnumerator FollowMouse(Transform objTrans) {
        while (_heldMoveable != null) {
            Vector3 targetPos = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            targetPos.z = objTrans.position.z;

            objTrans.position = Vector3.Lerp(objTrans.position, targetPos, Constants.ObjSnapSpeed * Time.deltaTime);

            yield return null;
        }
    }
}