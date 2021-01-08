using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyTroopManager : MonoBehaviour
{
    public GameObject GameScalePoint;
    public GameObject enemyTrooper;
    GameObject start2;

    float scale = 0.5f;
    int counter = 0;


    //Instantiate new Troop
    float timer = 0.0f;
    float timeThresh = 10.0f;
    private bool timerStarted = false;
    


    void Awake()
    {
        start2 = GameObject.Find("start2");
    }


    void Update()
    {
        if(timerStarted)
        {
            runEnemyClock();
        }
        
    }

    public void instantiateEnemy()
    {
        GameObject currentSquad = Instantiate(enemyTrooper, start2.transform.position, start2.transform.rotation);
        currentSquad.transform.localScale = (GameScalePoint.transform.localScale) * scale;
        currentSquad.transform.name = "ESquad " + counter;
        counter++;

        NavMeshAgent curAgent = currentSquad.GetComponent<NavMeshAgent>();
        curAgent.speed *= currentSquad.transform.localScale.x;
    }

    //Start Timer
    public void startTimer()
    {
        timerStarted = true;
    }

    private void runEnemyClock()
    {
        timer += Time.deltaTime;
        if(timer > timeThresh)
        {
            instantiateEnemy();     
            timer = 0;   
        }   
    }
    
}
