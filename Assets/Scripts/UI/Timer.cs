using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
            // What you want to do when the timer ends
            Debug.Log("Time's up!");
        }

        public void StartTimer()
        {
            timerStarted = true;
        }
    }
}