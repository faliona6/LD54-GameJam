using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Customer
{

    public class CustomerPool : MonoBehaviour
    {
        [SerializeField] public Customer[] customerPrefabs;
        private List<Customer> pooledCustomers = new List<Customer>();

        // Start is called before the first frame update
        void Start()
        {
            // Initialize the pool with plates from the assets list.
            for (int i = 0; i < customerPrefabs.Length; i++)
            {
                pooledCustomers.Add(customerPrefabs[i]);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        public Customer GetRandomCustomer()
        {
            if (pooledCustomers.Count == 0)
            {
                Debug.LogWarning("Pool is empty! Consider increasing the pool size or reusing customers.");
                return null;
            }

            int index = Random.Range(0, pooledCustomers.Count);
            Customer customer = pooledCustomers[index];
            pooledCustomers.RemoveAt(index); // Remove the customer from the pool to ensure it's not reused unless returned.
            return customer;
        }

        public void ReleaseCustomer(Customer customer)
        {
            if (customer != null)
            {
                pooledCustomers.Add(customer);
            }
        }
    }
}
