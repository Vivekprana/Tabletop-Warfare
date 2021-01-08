using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionMonitor : MonoBehaviour
{
    public bool thisSelected = false;
    public GameObject selectionSprite;

    void awake()
    {
       selectionSprite.SetActive(false);
    }

    public void triggerSelection()
    {
        if(!thisSelected)
        {
            thisSelected = true;
            selectionSprite.SetActive(true);
            
        }
        else
        {
            thisSelected = false;
            selectionSprite.SetActive(false);
        }
    }
}
