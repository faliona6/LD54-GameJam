using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Customer;

namespace UI
{
    public class Timer : MonoBehaviour
    {
        public Slider progressBar;
        public float startTimeInSeconds = 45;

        private float timeRemaining;
        private bool timerStarted = false;
        private bool timerEnded = false;

        void Start()
        {
            timeRemaining = startTimeInSeconds;
        }

        void Update()
        {
            if (timerStarted)
            {
                if (timeRemaining > 0)
                {
                    timeRemaining -= Time.deltaTime;

                    // Update progress bar
                    progressBar.value = timeRemaining / startTimeInSeconds;
                }
                else if (!timerEnded)
                {
                    timerEnded = true; // Ensure we only trigger the end function once
                    TimerFinished();
                }
            }
        }

        void TimerFinished()
        {
            Customer.Customer customer = gameObject.GetComponent<Customer.Customer>();
            customer.OnPlateFail.Invoke();
        }

        public void StartTimer()
        {
            timerStarted = true;
        }
    }
}