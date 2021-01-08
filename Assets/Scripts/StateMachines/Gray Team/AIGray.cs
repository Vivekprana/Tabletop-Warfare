using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIGray : MonoBehaviour
{
    // Start is called before the first frame update
    NavMeshAgent agent;
    Animator anim;
    public Transform player;
    SquadState currentState;
    public GameObject selectionSprite;
    public string enemytag = "Red Team";

    public float health = 100;
    /*
    void awake()
    {
        this.GetComponent<EnemyDetection>().enemytag = enemytag;
    }*/

    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        //selectionSprite = GameObject.Find("Selection Sprite");
        selectionSprite.SetActive(false);
        //anim = this.GetComponent<Animator>();
        currentState = new Wait(this.gameObject, agent, enemytag, false);
    }

    // Update is called once per frame
    void Update()
    {
        currentState =currentState.Process();
    }


}