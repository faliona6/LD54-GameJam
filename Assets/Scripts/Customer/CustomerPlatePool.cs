using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomGrid;

namespace Customer
{
    public class CustomerPlatePool : MonoBehaviour
    {
        [SerializeField] public Container[] plateSO; // Drag your Plate ScriptableObjects here.
        public Plate PlatePrefab;
        private List<Container> pooledPlates = new List<Container>();

        // Start is called before the first frame update
        void Start()
        {
            // Initialize the pool with plates from the assets list.
            for (int i = 0; i < plateSO.Length; i++)
            {
                pooledPlates.Add(plateSO[i]);
            }
        }

        public Plate GetRandomPlate()
        {
            if (pooledPlates.Count == 0)
            {
                Debug.LogWarning("Pool is empty! Consider increasing the pool size or reusing plates.");
                return null;
            }

            int index = Random.Range(0, pooledPlates.Count);
            Container container = pooledPlates[index];
            pooledPlates.RemoveAt(index); // Remove the plate from the pool to ensure it's not reused unless returned.
            Plate plate = Instantiate(PlatePrefab);
            plate.container = container;
            return plate;
        }
        public void ReleasePlate(Plate plate)
        {
            if (plate != null)
            {
                pooledPlates.Add(plate.container); // Assuming availablePlates is a list or collection storing available plates
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
