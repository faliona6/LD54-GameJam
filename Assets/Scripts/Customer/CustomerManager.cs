using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UI;

namespace Customer {
    public class CustomerManager : MonoBehaviour {
        public CustomerPlatePool platePool;
        public CustomerPool customerPool;

        [SerializeField] Customer activeCustomer;
        [SerializeField] Transform activeCustomerPos;
        [SerializeField] Customer nextCustomer;
        [SerializeField] Transform nextCustomerPos;
        [SerializeField] Transform waitingPos; // off screen space to put customers beyond the next customer

        public GameObject timer;

        public List<Customer> customers = new List<Customer>();

        public UnityEvent OnCustomersDone = new UnityEvent();

        void Start() {
            Timer timerComp = timer.GetComponent<Timer>();
            timerComp.progressBar = FindSlider("TimerProgressBar");
            CreateCustomer();
            Vector3 customerPosition = activeCustomer.transform.position;
            timerComp.progressBar.GetComponent<RectTransform>().anchoredPosition =
                new Vector2(customerPosition.x, customerPosition.y + 105);
        }

        // call from GameManager
        int _customersLeft = 0;
        public void SummonCustomers(int numCustomers) {
            if (_customersLeft > 0) {
                Debug.LogError("Still have customers left!!!");
                return;
            }

            _customersLeft = numCustomers;
            CallNextCustomers();
        }

        public void PlateSuccess() {
            // TODO: add plate completion reward
            CallNextCustomers();
        }
        public void PlateFail() {
            // TODO: add plate failure penalty
            CallNextCustomers();
        }

        public void CallNextCustomers() {
            if (_customersLeft == 0) { // done with all customers for day
                RemoveCustomer(activeCustomer);
                activeCustomer = null;
                nextCustomer = null;
                customers.Clear();
                OnCustomersDone.Invoke();
            }

            SetActiveCustomer(_customersLeft);
            SetNextCustomer(_customersLeft);
            _customersLeft--;
        }

        public Customer SetActiveCustomer(int customersLeft) {
            if (customersLeft <= 0) return null;

            // clean up old active customer
            RemoveCustomer(activeCustomer);

            if (nextCustomer) { // set next customer to active if exists
                activeCustomer = nextCustomer;
            } else { // generate new customer
                activeCustomer = CreateCustomer();
                activeCustomer.OnPlateSuccess.AddListener(PlateSuccess);
                activeCustomer.OnPlateFail.AddListener(PlateFail);
            }

            activeCustomer.transform.position = activeCustomerPos.position;

            return activeCustomer;
        }

        public Customer SetNextCustomer(int customersLeft) {
            if (customersLeft <= 1) return null;

            nextCustomer = CreateCustomer();
            nextCustomer.transform.position = nextCustomerPos.position;

            return nextCustomer;
        }
        Slider FindSlider(string name) {
            foreach (Slider slider in UnityEngine.Object.FindObjectsOfType<Slider>()) {
                if (slider.name == name) {
                    return slider;
                }
            }

            return null;
        }

        // Create a new Customer and set its plate pool reference
        public Customer CreateCustomer() {
            activeCustomer = customerPool.GetRandomCustomer().GetComponent<Customer>();

            if (activeCustomer != null) {
                return activeCustomer;
            }

            return null; // Returns null if the customer creation fails for some reason
        }

        // If you need a method to remove a customer:
        public void RemoveCustomer(Customer customer) {
            if (customer == null) return;

            // Release the customer back to the pool
            if (customerPool != null && customer != null) {
                customerPool.ReleaseCustomer(customer.gameObject);
                activeCustomer = null;
            }

            customer.OnPlateSuccess.RemoveListener(PlateSuccess);
            customer.OnPlateFail.RemoveListener(PlateFail);

            // Destroy the current plate GameObject if it exists
            if (customer != null) {
                Debug.Log("Destroying Customer");
                Destroy(customer);
            }
        }
    }
}