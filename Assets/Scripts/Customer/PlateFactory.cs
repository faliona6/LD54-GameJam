using System.ComponentModel;
using Customer;
using UnityEngine;

public class PlateFactory : MonoBehaviour {
    public static PlateFactory Instance { get; private set; }

    public GameObject _plateBase;
    public static GameObject PlateBase { get; private set; }

    void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        } else {
            Instance = this;
        }

        PlateBase = _plateBase;
    }
    
    // TODO: wherever (prob customer generator?) stores possible plate containers and calls this func to create Plate GameObject

    // CreatePlateObj instantiates a Plate GameObject with data from plateContainer at position
    public Plate CreatePlateObj(Container plateContainer, Vector2 pos) {
        Plate p = Instantiate(_plateBase, pos, Quaternion.identity).GetComponent<Plate>();
        // TODO: call plate init code

        return p;
    }
}