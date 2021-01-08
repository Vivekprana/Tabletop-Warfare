using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class State
{
    public enum STATE
    {
        IDLE, PATROL, WALK, SHOOT, SHOOTPLAYER
    };

    public enum EVENT
    {
        ENTER, UPDATE, EXIT
    };

    public STATE name;
    protected EVENT stage;
    protected GameObject npc;
    protected Animator anim;
    protected Transform player;
    protected State nextState;
    protected NavMeshAgent agent;
    protected Vector3 ObjPosition;

    //Game Items
    protected GameObject SelectedEnemy;
    protected string EnemyColor;


    public State(GameObject _npc, NavMeshAgent _agent, Animator _Anim, Transform _player,string _EnemyColor)
    {
        npc = _npc;
        agent = _agent;
        anim = _Anim;
        stage = EVENT.ENTER;
        player = _player;
        ObjPosition = GameObject.Find("start1").transform.position;
        EnemyColor = _EnemyColor;
    }

    public virtual void Enter() { stage = EVENT.UPDATE; }
    public virtual void Update() { stage = EVENT.UPDATE; }
    public virtual void Exit() { stage = EVENT.EXIT; }
    
    public State Process()
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

    //Find Enemy to chase
    public void DeclareEnemy()
    {
        SelectedEnemy = GameObject.FindWithTag(EnemyColor);
    }
    public void DealDamage(float time)
    {
        if (SelectedEnemy != null)
        {
           /* if(SelectedEnemy.TryGetComponent(out AIGray grayScript))
            {
                grayScript.takeDamage(1 * time);
            }*/
        }
    }
    /*
    public void FindObjectivePosition()
    {
        if(npc.tag != "Red Team")
        {
            GameObject Enemy = GameObject.FindWithTag("Red Team");
            ObjPosition = Enemy.transform.position;
        }
        else if (npc.tag != "Gray Team")
        {
            GameObject Enemy = GameObject.FindWithTag("Gray Team");
            ObjPosition = Enemy.transform.position;
        }
    }
    */
    public bool ReachedObjective()
    {
        Vector3 direction = npc.transform.position -  ObjPosition;
        //Debug.Log(direction.magnitude);
        if (direction.magnitude < 0.1)
        {
            return true;
        }
        return false;
    }

    public void FindEnemyPosition()
    {
        if (SelectedEnemy != null)
        {
            ObjPosition = SelectedEnemy.transform.position;
        }
    }


}
public class Idle : State
{
    public Idle(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player, string _EnemyColor)
        : base(_npc, _agent, _anim, _player, _EnemyColor)
        {
            name = STATE.IDLE;
        }

        public override void Enter()
        {
            //anim.SetTrigger("isIdle");
            //base.Enter();
            Debug.Log("enterIdle");
            anim.SetTrigger("isIdle");
            base.Enter();
        }

        public override void Update()
        {
            Debug.Log("idle");

        }

        public override void Exit()
        {
            //anim.ResetTrigger("isIdle");
            //base.Exit();
            anim.ResetTrigger("isIdle");
            Debug.Log("exitIdle");
            base.Exit();
        }
}

public class Walk: State
{
    public Walk(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player, string _EnemyColor )
        : base(_npc, _agent, _anim, _player, _EnemyColor)
        {
            name = STATE.WALK;
        }

        public override void Enter()
        {
            //anim.SetTrigger("isIdle");
            //base.Enter();
            //Debug.Log("enterWALK");
            anim.SetTrigger("isWalking");
            DeclareEnemy();
            //FindObjectivePosition();
            base.Enter();
        }

        public override void Update()
        {
            //g("walking");
            FindEnemyPosition();
            npc.transform.LookAt(ObjPosition);
            
            agent.SetDestination(ObjPosition);
            if(ReachedObjective())
            {
                
                nextState = new Shoot(npc, agent, anim, player, EnemyColor, SelectedEnemy);
                stage = EVENT.EXIT;
                
            }


        }

        public override void Exit()
        {
            //anim.ResetTrigger("isIdle");
            
            anim.ResetTrigger("isWalking");
            //base.Exit();
            agent.SetDestination(npc.transform.position);
            base.Exit();
        }

}
public class Shoot: State
{
    float timer = 0.0f;

    public Shoot(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player, string _EnemyColor, GameObject Enemy)
        : base(_npc, _agent, _anim, _player, _EnemyColor)
        {
            name = STATE.SHOOT;
            base.SelectedEnemy = Enemy;
            
        }

        public override void Enter()
        {
            anim.SetTrigger("isShooting");
            base.Enter();
        }
        //Update
        public override void Update()
        {
            if(SelectedEnemy == null)
            {
                nextState = new Idle(npc, agent, anim, player, EnemyColor);
                stage = EVENT.EXIT;
                return;
            }
            FindEnemyPosition();
            npc.transform.LookAt(ObjPosition);
            timer = Time.deltaTime;
            DealDamage(timer);
        }

        public override void Exit()
        {
            anim.ResetTrigger("isShooting");
            base.Exit();
        }

}

public class ShootPlayer: State
{
    public ShootPlayer(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player, string _EnemyColor)
        : base(_npc, _agent, _anim, _player, _EnemyColor)
        {
            name = STATE.SHOOTPLAYER;
        }

        public override void Enter()
        {
            anim.SetTrigger("isShooting");
            base.Enter();
        }
        //Update
        public override void Update()
        {
            FindEnemyPosition();
            npc.transform.LookAt(ObjPosition);
        }

        public override void Exit()
        {
            anim.ResetTrigger("isShooting");
            base.Exit();
        }

}
