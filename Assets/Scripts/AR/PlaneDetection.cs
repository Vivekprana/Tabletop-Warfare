﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace UnityEngine.XR.ARFoundation.Samples
{
    /// <summary>
    /// This example demonstrates disabling vertical planes as they are
    /// detected and instantiated by the <c>ARPlaneManager</c>.
    /// </summary>
    [RequireComponent(typeof(ARPlaneManager))]
    public class PlaneDetection : MonoBehaviour
    {
        [Tooltip("The UI Text element used to display log messages.")]
        [SerializeField]
        Text m_LogText;
        bool nextStep = false;

        /// <summary>
        /// The UI Text element used to display log messages.
        /// </summary>
        public Text logText
        {
            get { return m_LogText; }
            set { m_LogText = value; }
        }

        void OnEnable()
        {
            GetComponent<ARPlaneManager>().planesChanged += OnPlaneAdded;
        }

        void OnDisable()
        {
            GetComponent<ARPlaneManager>().planesChanged -= OnPlaneAdded;
        }

        void OnPlaneAdded(ARPlanesChangedEventArgs eventArgs)
        {
            //foreach (var plane in eventArgs.added)
              //  DisableIfVertical(plane);
            GameObject.Find("Canvas").GetComponent<StepTransitions>().boxFound();
            nextStep = true;
            print("planeAdded");
            Destroy(this);
        }
        /*
        void DisableIfVertical(ARPlane plane)
        {
            // Check whether the plane is a vertical plane.
            if (plane.alignment == PlaneAlignment.Vertical)
            {
                // Disable the entire GameObject.
                plane.gameObject.SetActive(false);

                // Add to our log so the user knows something happened.
                if (logText != null)
                    logText.text = string.Format("\n{0}", plane.trackableId);
            }
        }
        */
    }
}