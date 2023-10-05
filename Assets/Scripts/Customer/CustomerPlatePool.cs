using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using CustomGrid;

namespace Customer
{
    public class CustomerPlatePool : MonoBehaviour
    {
        [SerializeField] public Container[] plateSO; // Drag your Plate ScriptableObjects here.
        public GameObject platePrefab;
        private List<Container> pooledPlates = new List<Container>();

        // Start is called before the first frame update
        void Awake()
        {
            // Initialize the pool with plates from the assets list.
            for (int i = 0; i < plateSO.Length; i++)
            {
                pooledPlates.Add(plateSO[i]);
            }
        }

        public GameObject GetRandomPlate(Transform parent)
        {
            if (pooledPlates.Count == 0)
            {
                Debug.LogWarning("Pool is empty! Consider increasing the pool size or reusing plates.");
                return null;
            }

            int index = Random.Range(0, pooledPlates.Count);
            Container container = pooledPlates[index];
            pooledPlates.RemoveAt(index); // Remove the plate from the pool to ensure it's not reused unless returned.

            GameObject plate = parent == null ? Instantiate(platePrefab) : Instantiate(platePrefab, parent);
            Plate plateComp = plate.GetComponent<Plate>();
            plateComp.container = container;

            int columns = plateComp.container.matrix.FirstOrDefault()?.columns.Count ?? 0;
            Transform plateTransform = plate.transform.transform;
            
            // Center plate based on matrix width
            const float plateYOffset = 7.6f;
            plateTransform.localPosition = 
                new Vector3((0.5f - plateComp.container.matrix.Count / 2.0f) * plateTransform.localScale.x, 
                    -columns / 2.0f + plateYOffset, 0);
            return plate;
        }
        public void ReleasePlate(GameObject plate)
        {
            if (plate != null)
            {
                pooledPlates.Add(plate.GetComponent<Plate>().container); // Assuming availablePlates is a list or collection storing available plates
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
