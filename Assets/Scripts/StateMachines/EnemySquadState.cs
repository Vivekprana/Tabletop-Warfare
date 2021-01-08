using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySquadState 
{
    //Declare ENUM
    public enum ENEMYSQUADSTATE
    {
        WALKING, SHOOTSQUAD, WAIT
    };

    //Declare ENUM
    public enum EVENT
    {
        ENTER, UPDATE, EXIT
    };

    public ENEMYSQUADSTATE name;
    protected EVENT stage;
    protected GameObject Squad;
    protected EnemySquadState nextState;
    protected NavMeshAgent agent;

    //Game Items
    protected GameObject SelectedEnemy;
    protected string EnemyColor;
    protected Vector3 ObjectivePosition;
    protected bool hasObjective = false;
    protected bool ArEnemiesInVicinity = false;

    //Selection
    protected bool thisSquadSelected = false;
    
    

    //Manager
    GameObject GameEnvironment = GameObject.Find("GameEnvironment");

    public EnemySquadState(GameObject _squad, NavMeshAgent _agent, string _EnemyColor)
    {
        Squad = _squad;
        agent = _agent;
        EnemyColor = _EnemyColor;
        stage = EVENT.ENTER;

    }
    //Define each Enter/Update/Exit
    public virtual void Enter() { stage = EVENT.UPDATE; }
    public virtual void Update() { 
        stage = EVENT.UPDATE; 
        //Debug.Log("update");


       // Debug.Log("Updating");

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse Click");
            labelPointer(Input.mousePosition);
        }     
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            Debug.Log("Touch");
            /*labelPointer(Input.touches[0].position);*/
        }

    }
    public virtual void Exit() { stage = EVENT.EXIT; }

    //function for Process
    public EnemySquadState Process()
    {
        if(stage ==  EVENT.ENTER) Enter();
        if(stage == EVENT.UPDATE) Update();
        if(stage == EVENT.EXIT)
        {
            Exit();
            return nextState;
        }
        return this;
    }

    //mouse and selection functions
    void labelPointer(Vector2 selectedPosition)
    {
        Debug.Log("Vector:" + selectedPosition);
        Ray ray = Camera.main.ScreenPointToRay(selectedPosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log(hit.collider.name);
     
            if(hit.collider.name == Squad.name)
            {
                triggerSelection();
                return;
            }
            
            if(thisSquadSelected)
            {
                Vector3 tempVector = hit.point;
                tempVector.y = Squad.transform.GetChild(0).transform.position.y;
                ObjectivePosition = tempVector;
                if(!agent.hasPath)
                {
                    Debug.Log("No Path");
                }
                hasObjective = true;
                triggerSelection();
                return;
            }

        }
    } 
    //Selection Function
    void triggerSelection()
    {
        string squadScript = "AIGray";
        if(thisSquadSelected)
        {
            thisSquadSelected = false;
            Squad.GetComponent<AIGray>().selectionSprite.SetActive(false);
        }
        else
        {
            thisSquadSelected = true;
            Squad.GetComponent<AIGray>().selectionSprite.SetActive(true);
        }
    }
    //Set Objective Position
    public void declareObjective(Vector3 Objposition)
    {
        ObjectivePosition = Objposition;
        hasObjective = true;
    }

    //Various functions for the Finite Machine

    //Find Enemy to chase
    public void DeclareEnemy()
    {
        SelectedEnemy = GameObject.FindWithTag(EnemyColor);
    }

    //Find Shootable Enemies in the vicinity
    public bool EnemiesInVicinity ()
    {
        ArEnemiesInVicinity = Squad.GetComponent<EnemyDetection>().ShootableEnemy;
        if(ArEnemiesInVicinity)
            SelectedEnemy = Squad.GetComponent<EnemyDetection>().ImmediateEnemy;
        return ArEnemiesInVicinity;
    }
    //Did squad reach objective
    public bool ReachedObjective()
    {
        Vector3 direction = Squad.transform.position -  ObjectivePosition;
        //Debug.Log("Magnitude: " + direction.magnitude);
        return direction.magnitude < 0.1;
    }
    /*
    //Find Click Point
    public bool checkClickPoint()
    {
        //GameEnvironment = GameObject.Find("GameEnvironment");
        //GameObject GES = GameEnvironment.GetComponent<GameEnvironment>();
        bool newPoint = GameEnvironment.GetComponent<GameEnvironment>().pointedPosition;
        //Debug.Log("checking: " + newPoint);
        
        if(newPoint)
        {
            ObjectivePosition = GameEnvironment.GetComponent<GameEnvironment>().selectedPosition;
            ObjectivePosition.y = Squad.transform.GetChild(0).transform.position.y;
            hasObjective = true;
           // Squad.GetComponent<SelectionMonitor>().thisSelected = false;
        }

        return newPoint;
    }
    */

    /*
    //Check selected then check clickpoint
    public bool checkMarchingOrders()
    {
        if (thisSelected())
        {
            return checkClickPoint();
        }
        return false;
    }
    */

    //Find Enemy Posiiton
    public void FindEnemyPosition()
    {
        ObjectivePosition = SelectedEnemy.transform.position;
    }
    //turnSquad
    public void turnSquad()
    {
        Transform squadT = Squad.transform;
        int childCount = squadT.childCount;
        for(int i = 0; i < childCount; i++)
        {
            squadT.GetChild(i).LookAt(ObjectivePosition);
        }
    }
    //Animate Squad
    public void animateSquad(string Trigger)
    {
        Transform squadT = Squad.transform;
        int childCount = squadT.childCount;
        for(int i = 0; i < childCount; i++)
        {
            squadT.GetChild(i).GetComponent<Animator>().SetTrigger(Trigger);
        }
    }

    //Finish Animation
    public void endAnimateSquad(string Trigger)
    {
        Transform squadT = Squad.transform;
        int childCount = squadT.childCount;
        for(int i = 0; i < childCount; i++)
        {
            squadT.GetChild(i).GetComponent<Animator>().ResetTrigger(Trigger);
        }
    }

    /*
    public bool thisSelected()
    {
        return false;
    }
    */


}





