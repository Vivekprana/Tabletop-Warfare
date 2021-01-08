using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepTransitions : MonoBehaviour
{
    public GameObject scanStep;
    public GameObject setUp1;
    public GameObject placementStep;

    private void Awake()
    {
        setUp1.SetActive(false);
        placementStep.SetActive(false);

    }
    public void boxFound()
    {
        if (scanStep.activeSelf)
        {
            scanStep.SetActive(false);
            setUp1.SetActive(true);
        }
    }

    public void tapped()
    {
        if (setUp1.activeSelf)
        {
            setUp1.SetActive(false);
            placementStep.SetActive(true);
        }

    }

    public void Confirmed()
    {
        if(placementStep.activeSelf)
        {
            placementStep.SetActive(false);
        }
    }
}
