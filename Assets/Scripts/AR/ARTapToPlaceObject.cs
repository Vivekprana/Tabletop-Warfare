using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


[RequireComponent(typeof(ARRaycastManager))]
public class ARTapToPlaceObject : MonoBehaviour
{
    public GameObject GameObjectToInstantiate;
    
    private GameObject spawnedObject;
    private ARRaycastManager _arRaycastManager;
    private Vector2 touchPosition;

    //Instructions for SetUp Objects
    //public GameObject scanStep;
    
    //public GameObject SetUp2;
    GameObject Canvas;
    GameObject mainCanvas;


    bool objectPlaced = false;
    bool allowRePlacement = true;

    // Camera
    public GameObject Camera1;

    //Gesture
    public GameObject pinchGestureObject;


    static List<ARRaycastHit> hits = new List<ARRaycastHit>();
    // Start is called before the first frame update
    private void Awake()
    {
        _arRaycastManager = GetComponent<ARRaycastManager>();
        
        //SetUp2.SetActive(false);
        pinchGestureObject.SetActive(false);

        mainCanvas = GameObject.Find("Canvas");//.GetComponent<StepTransitions>().tapped();
        //Instantiate(GameObjectToInstantiate);
        
    }

    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        if(Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;

        }
        touchPosition = default;
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!objectPlaced)
        {
            if(!TryGetTouchPosition(out Vector2 touchPosition))
                return;
            if(_arRaycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
            {
                var hitPose = hits[0].pose;

                if(spawnedObject == null)
                {
                    spawnedObject = Instantiate(GameObjectToInstantiate, hitPose.position, hitPose.rotation);
                    pinchGestureObject.SetActive(true);
                    //SetUp1.SetActive(false);
                    //SetUp2.SetActive(true);
                    mainCanvas.GetComponent<StepTransitions>().tapped();

                    Canvas = GameObject.Find("Plane(Clone)/Canvas");
        
                    if (Canvas != null)

                    {
                        Canvas.SetActive(false);

                    }
                }
                else 
                {
                    //Vector3 position = hits[0].pose;
                    //Ray2D ray = Camera.main.ScreenPointToRay(touchPosition);
                    //RaycastHit hit;
                    if (allowRePlacement && Input.touchCount < 2)
                    {
                        //print(hit.transform.name);
                        //if (hit.transform.name != "ConfirmButton");
                        spawnedObject.transform.position = hitPose.position;
                    }
                    
                }
            }
                                
        }
    }

    public void DisallowRePlacement() {
        
        allowRePlacement = false;

        //return allowRePlacement;

    }

    public void allowReplacement() {
    
        allowRePlacement = true;

        //return allowRePlacement;

    }

    public void hitConfirm()
    {
        objectPlaced = true;
        mainCanvas.GetComponent<StepTransitions>().Confirmed();
        Camera1.GetComponent<ARPlaneViewToggle>().TogglePlaneDetection();
        pinchGestureObject.SetActive(false);
        if (Canvas != null)
        {
            Canvas.SetActive(true);
        }
        GameObject.Find("Game Surface Folder(Clone)").GetComponent<GameSurfaceParent>().onHitConfirm();

        
    }

    


}
