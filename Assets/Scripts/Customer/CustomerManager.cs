using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Customer {
    public class CustomerManager : MonoBehaviour {
        public CustomerPlatePool platePool;
        public CustomerPool customerPool;
        private Customer activeCustomerPrefab;

        [SerializeField] Customer activeCustomer;
        [SerializeField] Transform activeCustomerPos;
        [SerializeField] Customer nextCustomer;
        [SerializeField] Transform nextCustomerPos;

        public List<Customer> customers = new List<Customer>();
        
        public void GenerateNextCustomers(bool last) {
            if (last) {
                if (nextCustomer) {
                    activeCustomer = nextCustomer;
                    activeCustomer.transform.position = activeCustomerPos.position;
                }

                return;
            }
            if (nextCustomer) { // set next customer to active if exists
                activeCustomer = nextCustomer;
                activeCustomer.transform.position = activeCustomerPos.position;
                nextCustomer = CreateCustomer();
                nextCustomer.transform.position = nextCustomerPos.position;
            } else {            // generate new customers (first time)
                activeCustomer = CreateCustomer();
                activeCustomer.transform.position = activeCustomerPos.position;
                nextCustomer = CreateCustomer();
                nextCustomer.transform.position = nextCustomerPos.position;
            }
        }

        // Create a new Customer and set its plate pool reference
        public Customer CreateCustomer() {
            Customer customerPrefab = customerPool.GetRandomCustomer();
            activeCustomerPrefab = customerPrefab;
            Customer customerObj = Instantiate(customerPrefab, transform);
            Customer newCustomer = customerObj.GetComponent<Customer>();

            if (newCustomer != null) {
                newCustomer.platePool = platePool; // Assigning the plate pool to the customer
                activeCustomer = newCustomer;
                return newCustomer;
            }

            return null; // Returns null if the customer creation fails for some reason
        }

        // If you need a method to remove a customer:
        public void RemoveCustomer(Customer customer) {
            activeCustomer = null;

            // Release the customer back to the pool
            if (customerPool != null && customer != null) {
                customerPool.ReleaseCustomer(activeCustomerPrefab);
                activeCustomerPrefab = null;
            }

            // Destroy the current plate GameObject if it exists
            if (customer != null) {
                Debug.Log("Destroying Customer");
                Destroy(customer.gameObject);
            }
        }
        
        

        // If you need a method to remove a customer:
        public void ResetCustomer() {
            Customer customer = activeCustomer;
            RemoveCustomer(customer);
            CreateCustomer();
        }
    }
}