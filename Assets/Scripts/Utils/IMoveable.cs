using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveable {
    public Transform Transform { get; }
    
    Transform Drop();
    Transform PickUp();
    void Trash();
}