using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Class for Shooting another Squad
public class ShootSquad: SquadState
{
    public ShootSquad(GameObject _squad, NavMeshAgent _agent, string _EnemyColor, GameObject _SelectedEnemy, bool selected)
        : base(_squad, _agent, _EnemyColor)
        {
            name = SQUADSTATE.SHOOTSQUAD;
            SelectedEnemy = _SelectedEnemy;
            thisSquadSelected = selected;
        }

    public override void Enter()
    {
        Debug.Log("shoot");
        animateSquad("isShooting");
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        if(SelectedEnemy == null)
        {
            nextState = new Wait(Squad, agent, EnemyColor, thisSquadSelected);
            stage = EVENT.EXIT;
            return;
        }
        //To Do
        if(hasObjective)
        {
            nextState = new Walking(Squad, agent, EnemyColor, ObjectivePosition, thisSquadSelected, SelectedEnemy);
            stage = EVENT.EXIT;
        }
        if(!EnemiesInVicinity())
        {
            nextState = new Wait(Squad, agent, EnemyColor, thisSquadSelected);
            stage = EVENT.EXIT;
        }
        FindEnemyPosition();
        turnSquad();
        DealDamage(Time.deltaTime);
        //base.Update();

        //Exit Conditions
        //Enemy has left vicinity:
        /*
        if(!ReachedObjective())
        {
            nextState = new Walking(Squad, agent, EnemyColor);
            stage = EVENT.EXIT;
        }
        else if()
        {
            
        }
        */
    }

    public override void Exit()
    {
        //Set enemies in vicinity to false, to reset the Enemy Detection
        ArEnemiesInVicinity = Squad.GetComponent<EnemyDetection>().ShootableEnemy = false;
        Debug.Log("ending Shoot");
        endAnimateSquad("isShooting");
        base.Exit();
    }

}
            
