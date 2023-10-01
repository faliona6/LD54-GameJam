using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Customer
{
    public class CustomerManager : MonoBehaviour
    {
        public CustomerPlatePool platePool;
        public CustomerPool customerPool;
        private Customer activeCustomerPrefab;
        private Customer activeCustomer;

        // Start is called before the first frame update
        void Start()
        {
            CreateCustomer();
        }

        void Update()
        {

        }

        // Create a new Customer and set its plate pool reference
        public Customer CreateCustomer()
        {
            Customer customerPrefab = customerPool.GetRandomCustomer();
            activeCustomerPrefab = customerPrefab;
            Customer customerObj = Instantiate(customerPrefab, transform);
            Customer newCustomer = customerObj.GetComponent<Customer>();

            if (newCustomer != null)
            {
                newCustomer.platePool = platePool; // Assigning the plate pool to the customer
                activeCustomer = newCustomer;
                return newCustomer;
            }

            return null; // Returns null if the customer creation fails for some reason
        }

        // If you need a method to remove a customer:
        public void RemoveCustomer(Customer customer)
        {
            activeCustomer = null;

            // Release the customer back to the pool
            if (customerPool != null && customer != null)
            {
                customerPool.ReleaseCustomer(activeCustomerPrefab);
                activeCustomerPrefab = null;
            }

            // Destroy the current plate GameObject if it exists
            if (customer != null)
            {
                Debug.Log("Destroying Customer");
                Destroy(customer.gameObject);
            }
        }

        // If you need a method to remove a customer:
        public void ResetCustomer()
        {
            Customer customer = activeCustomer;
            RemoveCustomer(customer);
            CreateCustomer();
        }
    }
}
