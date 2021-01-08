using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TroopManager : MonoBehaviour
{
    public GameObject squadPrefab;
    public GameObject GameScalePoint;
    GameObject start1;
    float scale = 0.5f;

    //Counter for squad naming
    int counter = 0;

    void Awake()
    {
        start1 = GameObject.Find("start1");
 
    }


    public void instantiateSquad()
    {
        GameObject currentSquad = Instantiate(squadPrefab, start1.transform.position, start1.transform.rotation);
        currentSquad.transform.localScale = (GameScalePoint.transform.localScale) * scale;
        currentSquad.transform.name = "Squad " + counter;
        counter++;


        NavMeshAgent curAgent = currentSquad.GetComponent<NavMeshAgent>();
        curAgent.speed *= currentSquad.transform.localScale.x;
    }
    
    



    

}
