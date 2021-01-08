 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CaptureScript : MonoBehaviour
{

    // Enemy Items
    private string enemyTag;
    private string thisTag;
    GameObject enemyFlag;
    GameObject captureHealthBar;
    HealthBar cptBarScpt;

    private const float totalCapturePoints = 1000;
    private float capturePoints = 1000;
    private float capPS = 10;
    List<GameObject> Captors = new List<GameObject>();

    //Construct
    void Awake(){
        //Determine enemyTag
        DetermineTags();
        
    }

    void Start(){
        // Find Enemy Flag
        string flagName = "Flags/Flag" + thisTag;
        enemyFlag = transform.parent.Find(flagName).gameObject;
        captureHealthBar = enemyFlag.transform.FindChild("HealthBar").gameObject;
        cptBarScpt = captureHealthBar.GetComponent<HealthBar>();
        
    }

    //When Capture Box is entered start capture function
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag.Contains(enemyTag))
        {
            Captors.Add(other.gameObject);
        }
    }
    //Exit Function
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag.Contains(enemyTag))
        {
            Captors.Remove(other.gameObject);
        }
    }

    //Subtract Capture points
    private void captureBase() 
    {
        int strength = Captors.Count;
        float cptPoints = capPS * strength * Time.deltaTime;
        capturePoints -= cptPoints;
        cptBarScpt.updateBar(capturePoints/totalCapturePoints);
        if(capturePoints < 0){GameOver();}
    }

    void Update()
    {
        captureBase();
        checkArrayList();
    }

    //Check for deaths
    private void checkArrayList()
    {
        for(int i = 0; i < Captors.Count; i++){
            if (Captors[i] == null ) {
                Captors.RemoveAt(i);
            }
        }
    }
    //Game Won
    private void GameOver()
    {
        //Load Game Over Screen
        SceneManager.LoadScene("GameFeedback Scene");
    }

    private void DetermineTags()
    {
        if(this.gameObject.name.Contains("Red")) {
            enemyTag = "Gray";
            thisTag = "Red";
        }
        else {
            enemyTag = "Red";
            thisTag = "Gray";
        }  
    }

}
