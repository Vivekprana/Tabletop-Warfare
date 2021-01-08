using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Wait: SquadState
{
    public Wait(GameObject _squad, NavMeshAgent _agent,  string _EnemyColor, bool selected)
        : base(_squad, _agent, _EnemyColor)
        {
            name = SQUADSTATE.WAIT;
            thisSquadSelected = selected;
        }

    public override void Enter()
    {
        animateSquad("isIdle");
        base.Enter();
        //Debug.Log("EnteredWait");
        
    }

    public override void Update()
    {
        base.Update();
        //Debug.Log("wait is happening" + Squad.name);

        if (hasObjective)
        {
            Debug.Log("hasObje");
            nextState = new Walking(Squad, agent, EnemyColor, ObjectivePosition, thisSquadSelected, SelectedEnemy);
            stage = EVENT.EXIT;
        }
        else if (EnemiesInVicinity())
        {
            //Debug.Log("Enemies");
            nextState = new ShootSquad(Squad, agent, EnemyColor, SelectedEnemy, thisSquadSelected);
            stage = EVENT.EXIT;
        }
        //Debug.Log("Made throughloop" + Squad.name);
    }

    public override void Exit()
    {
        endAnimateSquad("isIdle");
        base.Exit();
    }
}
