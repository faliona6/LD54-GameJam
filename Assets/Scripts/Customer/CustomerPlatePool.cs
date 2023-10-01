using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Customer
{
    public class CustomerPlatePool : MonoBehaviour
    {
        [SerializeField] public Plate[] plateAssets; // Drag your Plate ScriptableObjects here.
        private List<Plate> pooledPlates = new List<Plate>();

        // Start is called before the first frame update
        void Start()
        {
            // Initialize the pool with plates from the assets list.
            for (int i = 0; i < plateAssets.Length; i++)
            {
                pooledPlates.Add(plateAssets[i]);
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
            Plate plate = pooledPlates[index];
            pooledPlates.RemoveAt(index); // Remove the plate from the pool to ensure it's not reused unless returned.
            return plate;
        }
        public void ReleasePlate(Plate plate)
        {
            if (plate != null)
            {
                pooledPlates.Add(plate); // Assuming availablePlates is a list or collection storing available plates
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
