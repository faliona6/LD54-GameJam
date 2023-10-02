using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UI;

namespace Customer
{
    public class CustomerManager : MonoBehaviour
    {
        public CustomerPlatePool platePool;
        public CustomerPool customerPool;
        public GameObject timer;
        private GameObject activeCustomer;

        // Start is called before the first frame update
        void Start()
        {
            Timer timerComp = timer.GetComponent<Timer>();
            timerComp.progressBar = FindSlider("TimerProgressBar");
            CreateCustomer();
            Vector3 customerPosition = activeCustomer.transform.position;
            timerComp.progressBar.GetComponent<RectTransform>().anchoredPosition = new Vector2(customerPosition.x, customerPosition.y + 105);
        }

        void Update()
        {

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

        // Create a new Customer and set its plate pool reference
        public GameObject CreateCustomer()
        {
            activeCustomer = customerPool.GetRandomCustomer();

            if (activeCustomer != null)
            {
                return activeCustomer;
            }

            return null; // Returns null if the customer creation fails for some reason
        }

        // If you need a method to remove a customer:
        public void RemoveCustomer(GameObject customer)
        {
            // Release the customer back to the pool
            if (customerPool != null && customer != null)
            {
                customerPool.ReleaseCustomer(customer);
                activeCustomer = null;
            }

            // Destroy the current plate GameObject if it exists
            if (customer != null)
            {
                Debug.Log("Destroying Customer");
                Destroy(customer);
            }
        }

        // If you need a method to remove a customer:
        public void ResetCustomer()
        {
            RemoveCustomer(activeCustomer);
            CreateCustomer();
        }
    }
}
