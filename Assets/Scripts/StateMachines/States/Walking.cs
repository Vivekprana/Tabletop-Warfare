using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Walking: SquadState
{
    public Walking(GameObject _squad, NavMeshAgent _agent,  string _EnemyColor, Vector3 Objposition, bool selected, GameObject _SelectedEnemy)
        : base(_squad, _agent, _EnemyColor)
        {
            name = SQUADSTATE.WALKING;
            declareObjective(Objposition);
            thisSquadSelected = selected;
            SelectedEnemy = _SelectedEnemy;
        }

        public override void Enter()
        {   
            //Debug.Log("enterWALK");
            base.Enter();
        }

        public override void Update()
        {
            base.Update();
            animateSquad("isWalking");
            if (!hasObjective)
            {
                nextState = new Wait(Squad, agent, EnemyColor, thisSquadSelected);
                stage = EVENT.EXIT;
            }
            FindEnemyPosition();
            turnSquad();
            agent.SetDestination(ObjectivePosition);
            if(ReachedObjective())
            {
                if(!EnemiesInVicinity())
                {
                    nextState = new Wait(Squad, agent, EnemyColor, thisSquadSelected);
                    stage = EVENT.EXIT;
                   
                }
                else
                {

                    nextState = new ShootSquad(Squad, agent, EnemyColor, SelectedEnemy, thisSquadSelected);
                    stage = EVENT.EXIT;
                }
            }


        }

        public override void Exit()
        {
            endAnimateSquad("isWalking");
            agent.SetDestination(Squad.transform.position);
            base.Exit();
        }

}