using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Customer
{

    public class CustomerPool : MonoBehaviour
    {
        [SerializeField] private Sprite[] customerSprites;
        [SerializeField] private GameObject customerContainer;
        [SerializeField] private GameObject customerPrefab;
        
        private List<Sprite> pooledSprites = new List<Sprite>();

        // Start is called before the first frame update
        void Start()
        {
            // Initialize the pool with plates from the assets list.
            for (int i = 0; i < customerSprites.Length; i++)
            {
                pooledSprites.Add(customerSprites[i]);
            }
        }

        public GameObject GetRandomCustomer()
        {
            if (pooledSprites.Count == 0)
            {
                Debug.LogWarning("Pool is empty! Consider increasing the pool size or reusing customers.");
                return null;
            }

            int index = Random.Range(0, pooledSprites.Count);
            Sprite sprite = pooledSprites[index];
            pooledSprites.RemoveAt(index); // Remove the customer from the pool to ensure it's not reused unless returned.
            customerPrefab.GetComponent<SpriteRenderer>().sprite = sprite;
            GameObject customer = Instantiate(customerPrefab, customerContainer.transform);
            return customer;
        }

        public void ReleaseCustomer(GameObject customer)
        {
            if (customer != null)
            {
                pooledSprites.Add(customer.GetComponent<SpriteRenderer>().sprite);
            }
        }
    }
}
