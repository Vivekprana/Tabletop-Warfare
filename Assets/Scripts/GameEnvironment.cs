using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnvironment : MonoBehaviour
{
    // Start is called before the first frame update
    private static GameEnvironment _instance;

    
    public static GameEnvironment Instance
    {
        get
        {
            //create logic to create the Instance
            if (_instance == null)
            {
                GameObject go = new GameObject("GameEnvironment");
                go.AddComponent<GameEnvironment>();
            }

            return _instance;
        }
    }

    //Plane info
    private float PlaneYValue;

    public GameObject selectedEnemy;
    public Vector3 selectedPosition;
    public bool pointedPosition = false;
    public GameObject squadSelector;

    void Start()
    {
        //PlaneYValue = GameObject.Find("Plane").transform.position.y;
        squadSelector = GameObject.Find("Selection Sprite");
    }/*
    void Update()
    {
        
#if UNITY_EDITOR

        if (Input.GetMouseButtonDown(0))
        {
            SetPointer(Input.mousePosition);
            Debug.Log(Input.mousePosition);
        }
#endif

    }
    void SetPointer(Vector2 SelectPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(SelectPosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            //Debug.Log(hit.transform.position);
            //Debug.Log(hit.point);
            Debug.Log(hit.collider.name);
            //
            if(hit.collider.name != "Plane")
            {
                hit.transform.gameObject.GetComponent<SelectionMonitor>().triggerSelection();
            }
            Vector3 tempVector = hit.point;
            tempVector.y = PlaneYValue;

            selectedPosition = tempVector;
            pointedPosition = true;

            
        }
    }*/

}
