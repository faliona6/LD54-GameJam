using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public static Player Instance { get; private set; }

    [SerializeField] LayerMask _moveableLayer;

    public PlayerInventory PlayerInventory;

    IMoveable _heldMoveable;
    Camera _mainCamera;

    void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        } else {
            Instance = this;
        }

        _mainCamera = Camera.main;
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
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
    
    public void PlaceInHand(GameObject obj) {
        _heldMoveable = obj.GetComponent<IMoveable>();
        StartCoroutine(FollowMouse(_heldMoveable.Transform));
    }
}