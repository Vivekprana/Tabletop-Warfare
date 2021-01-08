using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pinchZoom : MonoBehaviour
{
    //zoom
    float previousDistance;
    float zoomSpeed = 0.5f;

    //Rotate
    const float pinchTurnRatio = Mathf.PI/2;
    const float minTurnAngle = 0;
    const float pinchRatio = 1;

    //Delta of turn angle
    private float turnAngleDelta;

    //angle between two points
    private float turnAngle;


    GameObject Gameplane;
    Vector3 originalScale;

    void Start() {
        
        Gameplane = GameObject.Find("Game Surface Folder(Clone)");
        originalScale = Gameplane.transform.localScale;

    }

    //update
    void Update() {

        if (Input.touchCount == 2 && (Input.GetTouch(0).phase == TouchPhase.Began || 
            Input.GetTouch(1).phase == TouchPhase.Began) ) {
                print("Touch Phase has begun!");
                //Calibrate Distance
                previousDistance = Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);

        }

        else if (Input.touchCount == 2 && 
            (Input.GetTouch(0).phase == TouchPhase.Moved || 
                Input.GetTouch(1).phase == TouchPhase.Moved) ) {

                    /***SET THE ZOOM**/
                    float distance;
                    Touch touch1 = Input.GetTouch(0);
                    Touch touch2 = Input.GetTouch(1);

                    distance = Vector2.Distance(touch1.position, touch2.position);

                    //camera based on pinch/zoom
                    //print(Time.deltaTime);
                    //float pinchAmount = (previousDistance - distance) * zoomSpeed * Time.deltaTime;
                    float pinchAmount = (distance/previousDistance);
                    print(pinchAmount);
                    previousDistance = distance;
                    Vector3 adjustScale = new Vector3(pinchAmount, pinchAmount, pinchAmount);
                    //originalScale
                    if (originalScale.x > 0.01 && pinchAmount < 1)
                    {
                        originalScale = Vector3.Scale(adjustScale, originalScale);
                    }
                    else if (pinchAmount > 1 && originalScale.x < 2)
                    {
                        originalScale = Vector3.Scale(adjustScale, originalScale);
                    }
                    
                    Gameplane.transform.localScale = originalScale;
                    print("previous Distance: " + previousDistance + " localScale: " + Gameplane.transform.localScale);

                    /*** SET THE ROTATION ***/
                    //Rotate Variables
                    //float pinchAmountR = 0;
                    Quaternion desiredRotation = Gameplane.transform.rotation;

                    //Calculate process
                    print("Start Turning process");
                    turnAngle = Angle(touch1.position, touch2.position);
                    float prevTurn = Angle(touch1.position - touch1.deltaPosition, touch2.position - touch2.deltaPosition);
                    turnAngleDelta = Mathf.DeltaAngle(prevTurn, turnAngle);

                    if (Mathf.Abs(turnAngleDelta) > minTurnAngle)
                    {
                        turnAngleDelta *= pinchTurnRatio;
                    }
                    else
                    {
                        turnAngle = turnAngleDelta = 0;
                    }

                    //Next Step
                    if (Mathf.Abs(turnAngleDelta) > 0)
                    {
                        Vector3 rotationDeg = Vector3.zero;
                        rotationDeg.y = -turnAngleDelta;
                        desiredRotation *= Quaternion.Euler(rotationDeg);
                    }
                    
                    //FInal set
                    Gameplane.transform.rotation = desiredRotation;

                }    
            
        
    }
    private float Angle(Vector2 pos1, Vector2 pos2)
    {
        Vector2 from = pos2 - pos1;
        Vector2 to = new Vector2(1, 0);
        float result = Vector2.Angle( from, to);
        Vector3 cross = Vector3.Cross( from, to);

        if (cross.z > 0)
        {
            result = 360f - result;
        }
        return result;
    }


}
