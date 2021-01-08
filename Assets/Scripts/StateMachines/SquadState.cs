using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//using mscorlib.dll;
using System;
//using System.NullReferenceException;

public class SquadState 
{
    //Declare ENUM
    public enum SQUADSTATE
    {
        WALKING, SHOOTSQUAD, WAIT
    };

    //Declare ENUM
    public enum EVENT
    {
        ENTER, UPDATE, EXIT
    };

    public SQUADSTATE name;
    protected EVENT stage;
    protected GameObject Squad;
    protected SquadState nextState;
    protected NavMeshAgent agent;

    //Game Items
    
    protected GameObject SelectedEnemy;
    protected string color;
    protected string EnemyColor;
    protected Vector3 ObjectivePosition;
    protected bool hasObjective = false;
    protected bool ArEnemiesInVicinity = false;
    protected Dictionary <string, float> gameScaleInfo;

    //Selection
    protected bool thisSquadSelected = false;
    CommandScript cmdScrpt;
    
    

    //Manager
    GameObject GameEnvironment = GameObject.Find("GameEnvironment");

    public SquadState(GameObject _squad, NavMeshAgent _agent, string _EnemyColor)
    {
        Squad = _squad;
        agent = _agent;
        EnemyColor = _EnemyColor;
        stage = EVENT.ENTER;

    }
    //Define each Enter/Update/Exit
    public virtual void Enter() 
    { 
        stage = EVENT.UPDATE;  
        cmdScrpt = Squad.GetComponent<CommandScript>();
        try 
        {
            gameScaleInfo = GameObject.Find("Game Surface Folder").GetComponent<GameSurfaceParent>().getGameScale();
        }
        catch(Exception e)
        {
            gameScaleInfo = GameObject.Find("Game Surface Folder(Clone)").GetComponent<GameSurfaceParent>().getGameScale();
        }
        setColor();
    }
    public virtual void Update() { 
        stage = EVENT.UPDATE; 
        //Debug.Log("update");
        
        if(cmdScrpt.getCommandStatus())
        {
            try {
                clickCommand(cmdScrpt.getHit());
            }
            catch(NullReferenceException e)
            {
                targetSelection(cmdScrpt.getCurrentTarget());
            }
            //clickCommand(cmdScrpt.getHit());

        }
        
       // Debug.Log("Updating");
        /*
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse Click");
            //labelPointer(Input.mousePosition);
        }     
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            Debug.Log("Touch");
            /*labelPointer(Input.touches[0].position);*/
        

    }
    public virtual void Exit() { stage = EVENT.EXIT; }

    // Determine this Color
    public void setColor() {
        if (this.EnemyColor == "Red Team")
        {
            color = "Gray";
        }
        else {
            color = "Red";
        }

    }
    //function for Process
    public SquadState Process()
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
    void clickCommand(RaycastHit hit)
    {
        //Debug.Log(hit + " "+ hit.collider.name);
        if(hit.collider.name == Squad.name)
        {
            triggerSelection();
            return;
        }
        
        //Check to see if this squad is already selected
        if(thisSquadSelected  && color == "Gray")
        {
            //Condidtion in case enemy is selected
            if(hit.collider.tag == EnemyColor)
            {
                SelectedEnemy = hit.collider.gameObject;    
            }
            else //Uncheck selected enemy if this is case
            {
                SelectedEnemy = null;
            }
            Vector3 tempVector = hit.point;
            tempVector.y = Squad.transform.GetChild(0).transform.position.y;
            ObjectivePosition = tempVector;
            hasObjective = true;
        }
        if(this.thisSquadSelected)
        {
            triggerSelection();
        }
    } 
    //AI selection 
    void targetSelection(GameObject enemy)
    {
        SelectedEnemy = enemy;
        Vector3 tempVector = enemy.transform.position;
        //tempVector.y = enemy.transform.GetChild(0).transform.position.y;
        ObjectivePosition = tempVector;
        hasObjective = true;
        return;
    }

    //Selection Function
    void triggerSelection()
    {
        if(thisSquadSelected && color == "Gray")
        {
            thisSquadSelected = false;
            Squad.GetComponent<AIGray>().selectionSprite.SetActive(false);
        }
        else if(thisSquadSelected && color == "Red") {
            thisSquadSelected = false;
            Squad.GetComponent<AIRed>().selectionSprite.SetActive(false);
        }
        else if(!thisSquadSelected && color == "Gray")
        {
            thisSquadSelected = true;
            Squad.GetComponent<AIGray>().selectionSprite.SetActive(true);
        }
        else {
            thisSquadSelected = true;
            Squad.GetComponent<AIRed>().selectionSprite.SetActive(true);
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
    public bool EnemiesInVicinity()
    {
        //Check if selected enemy is in vicinity first.
        if(SelectedEnemy != null)
        {
            if (Vector3.Distance(Squad.transform.position, SelectedEnemy.transform.position) < gameScaleInfo["StandardShotDist"])
            {
                ArEnemiesInVicinity = true;
                return ArEnemiesInVicinity;
            } 
            //else selected enemy should be null
            //SelectedEnemy = null;

        }
        //else {}

        //Calculation for idle with no selected enemy
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
        if (SelectedEnemy == null)
        {
            return direction.magnitude < gameScaleInfo["StandardStopDist"];
        }
        else 
        {
            return direction.magnitude < gameScaleInfo["StandardShotDist"];
        }
    }

    //Deal Damage
    public void DealDamage(float time)
    {
        if (SelectedEnemy != null)
        {
            if(SelectedEnemy.TryGetComponent(out CommandScript commandScript))
            {
                commandScript.takeDamage(1 * time);
            }
        }
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
        if (SelectedEnemy != null)
        {
            ObjectivePosition = SelectedEnemy.transform.position;
        }
    }
    //turnSquad
    public void turnSquad()
    {
        Transform squadT = Squad.transform;
        int childCount = squadT.childCount;
        for(int i = 0; i < childCount; i++)
        {
            if (squadT.GetChild(i).TryGetComponent(out Animator anim))
            {
                squadT.GetChild(i).LookAt(ObjectivePosition);
            }
            
        }
    }
    //Animate Squad
    public void animateSquad(string Trigger)
    {
        Transform squadT = Squad.transform;
        int childCount = squadT.childCount;
        for(int i = 0; i < childCount; i++)
        {
            if (squadT.GetChild(i).TryGetComponent(out Animator anim))
            {
                anim.SetTrigger(Trigger);
            }
        }
    }

    //Finish Animation
    public void endAnimateSquad(string Trigger)
    {
        Transform squadT = Squad.transform;
        int childCount = squadT.childCount;
        
        for(int i = 0; i < childCount; i++)
        {
            if (Squad.transform.GetChild(i).TryGetComponent(out Animator anim))
            {
                anim.ResetTrigger(Trigger);
            }
        }
    }

    /*
    public bool thisSelected()
    {
        return false;
    }
    */


}





