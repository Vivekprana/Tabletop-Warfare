using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject Squad;
    public GameObject InstantiatePoint;
    private int counter = 0;

    public void SpawnSquad(){
        counter++;
        GameObject squadTemp = Instantiate(Squad, InstantiatePoint.transform.position, InstantiatePoint.transform.rotation);
        squadTemp.transform.name = "Squad" + counter;
    }
    
}
