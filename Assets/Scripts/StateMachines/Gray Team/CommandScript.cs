using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandScript : MonoBehaviour
{
    //Commands Generic
    public bool controllable = true;
    private bool command = false;
    public Vector2 position;
    string enemyTag;
    private RaycastHit hit;

    

    //For AI
    GameObject[] enemies;
    GameObject currentTarget;

    //Health Manager
    private float totalHealth = 100;
    public float health = 100;

    //Health Bar
    public GameObject healthBar;
    private HealthBar healthScript;

    void Awake()
    {
        enemyTag = this.GetComponent<EnemyDetection>().getEnemyTag();
        healthScript = healthBar.GetComponent<HealthBar>();
    }

    //if player
    

    void Update()
    {
        if (controllable)
        {
            if (Input.GetMouseButtonDown(0))
            {
                //command =true;
                //position = Input.mousePosition;
                labelPointer(Input.mousePosition);
            }     
            if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
            {
                ////Debug.Log("Touch");
                /*labelPointer(Input.touches[0].position);*/
            }
        }
        if(!controllable)
        {
            if (Input.GetMouseButtonDown(0))
            {
                //command =true;
                //position = Input.mousePosition;
                labelPointer(Input.mousePosition);
            }     

            if(currentTarget == null)
            {
            
                findNextTarget();
                //command = true;
            }
            
        }

    }

    void labelPointer(Vector2 selectedPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(selectedPosition);
        if (Physics.Raycast(ray, out hit))
        {
            command = true;

            //hit.collider = this.gameObject.collider;
            ////Debug.Log(hit.collider.name);
            //hit.point = new Vector3(0f, 0f, 0f);


        }
    }

    void findNextTarget()
    {
        enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        //Debug.Log(enemies);

        if(enemies.Length == 0)
        {
            captureZone();
            //command = false;
            return;
        }
        float distance = 99999999999999; //Very high number, don't like it but will fix later.
        foreach(GameObject enemy in enemies)
        {
            float thisdistance = Vector3.Distance(enemy.transform.position,this.transform.position);
            if(thisdistance < distance)
            {
                distance = thisdistance;
                currentTarget = enemy;
            }
        }
        command = true;
    }

    // Capure the Zone 
    void captureZone() {
        // Set Current Target to enemy base
        currentTarget = GameObject.Find("start1");
        command = true;
        
    }

    //getCommandStatus
    public bool getCommandStatus()
    {
        if(command)
        {
            command = false;
            return true;
        }
        else {return false;}
    }
    // get hit data
    public RaycastHit getHit()
    {
        return hit;
    }
    //Get ai enemy
    public GameObject getCurrentTarget()
    {
        return currentTarget;
    }
    
    /** Health Management Section **/

    //Destroy Health
    public void takeDamage(float damage)
    {
        health -= damage;
        healthScript.updateBar(health/totalHealth);

        if(health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
