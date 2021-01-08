using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    public bool ShootableEnemy = false;
    public GameObject ImmediateEnemy;
    string enemytag;

    /**
    Needs a checker because trigger is sensitive
    **/
    void Awake()
    {
        if (!this.gameObject.name.Contains("E"))
        {
            enemytag = "Red Team";
        }
        else
        {
            enemytag = "Gray Team";
        }
    }
    
    public string getEnemyTag()
    {
        return enemytag;
    }
    /*

    private void OnTriggerEnter(Collider Other)
    {
        Debug.Log("entrance");
        if(Other.gameObject.tag == enemytag)
        {
            Debug.Log("enter");
            ShootableEnemy = true;
            ImmediateEnemy = Other.gameObject;
        }
    }


    private void OnTriggerExit(Collider Other)
    {
        

    }
    */
    void OnCollisionStay(Collision Collision)
    {
        GameObject Other = Collision.collider.gameObject;
        if(Other.tag == enemytag)
        {
            //Debug.Log("Variable: " + enemytag + ", actual tag: " + Other.tag);
            //Debug.Log("enter" + enemytag);
            ShootableEnemy = true;
            ImmediateEnemy = Other.gameObject;
        }
    }
    /*
    void OnCollisionEnter(Collision Collision)
    {
        //Debug.Log("EnterCollision");
        
        GameObject Other = Collision.collider.gameObject;
        Debug.Log("Variable: " + enemytag + ", actual tag: " + Other.tag);
        Debug.Log(Other.name);
        if(Other.tag == enemytag)
        {
            Debug.Log("enter");
            ShootableEnemy = true;
            ImmediateEnemy = Other.gameObject;
        }
    }


    void OnCollisionExit(Collision Collision)
    {
        Debug.Log("EnterCollision");
        
        GameObject Other = Collision.collider.gameObject;
        Debug.Log("Variable: " + enemytag + ", actual tag: " + Other.tag);
        Debug.Log(Other.name);
        if(Other.tag == enemytag)
        {
            //Debug.Log("exit");
            ShootableEnemy = false;
        }
    }
    */

}
