using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Unity.VisualScripting;
using Timer = UI.Timer;

namespace Customer {
    public class CustomerManager : MonoBehaviour {
        public CustomerPlatePool platePool;
        public CustomerPool customerPool;

        [SerializeField] Customer activeCustomer;
        [SerializeField] Transform activeCustomerPos;
        [SerializeField] Customer nextCustomer;
        [SerializeField] Transform nextCustomerPos;
        [SerializeField] Transform waitingPos; // off screen space to put customers beyond the next customer

        public List<Customer> customers = new List<Customer>();

        [CanBeNull] public Action OnCustomersDone;
        [CanBeNull] public Action OnCustomersChanged;

        // call from GameManager
        int _customersLeft = 0;
        public void SummonCustomers(int numCustomers) {
            if (_customersLeft > 0) {
                Debug.LogError("Still have customers left!!!");
                return;
            }
            
            Debug.Log("Summoning customers");

            _customersLeft = numCustomers;
            CallNextCustomers();
        }

        public void PlateSuccess(int money) {
            GameManager.Instance.ModifyMoney(money);
            CallNextCustomers();
        }
        public void PlateFail() {
            GameManager.Instance.LostGame();
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
            OnCustomersChanged?.Invoke();
        }

        public Customer SetActiveCustomer(int customersLeft) {
            if (customersLeft <= 0) return null;

            // clean up old active customer
            RemoveCustomer(activeCustomer);

            if (nextCustomer) { // set next customer to active if exists
                activeCustomer = nextCustomer;
            } else { // generate new customer
                activeCustomer = CreateCustomer();
            }

            activeCustomer.transform.position = activeCustomerPos.position;
            activeCustomer.OnPlateSuccess.AddListener(PlateSuccess);
            activeCustomer.OnPlateFail.AddListener(PlateFail);
            Timer timer = activeCustomer.gameObject.GetComponent<Timer>();
            timer.progressBar = FindSlider("TimerProgressBar");
            timer.StartTimer();

            return activeCustomer;
        }

        public Customer SetNextCustomer(int customersLeft) {
            if (customersLeft <= 1) return null;

            nextCustomer = CreateCustomer();
            nextCustomer.transform.position = nextCustomerPos.position;

            return nextCustomer;
        }

        // Create a new Customer and set its plate pool reference
        public Customer CreateCustomer() {
            Customer customer = customerPool.GetRandomCustomer().GetComponent<Customer>();

            if (customer != null) {
                return customer;
            }

            return null; // Returns null if the customer creation fails for some reason
        }

        // If you need a method to remove a customer:
        public void RemoveCustomer(Customer customer) {
            if (customer == null) return;

            // Release the customer back to the pool
            if (customerPool != null) {
                customerPool.ReleaseCustomer(customer.gameObject);
            }

            customer.OnPlateSuccess.RemoveListener(PlateSuccess);
            customer.OnPlateFail.RemoveListener(PlateFail);
            Debug.Log("Destroying Customer");
            Destroy(customer.gameObject);
        }
        Slider FindSlider(string name)
        {
            foreach (Slider slider in UnityEngine.Object.FindObjectsOfType<Slider>())
            {
                if (slider.name == name)
                {
                    return slider;
                }
            }

            return null;
        }

        public Customer GetActiveCustomer() => activeCustomer;
    }
}