using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameSurfaceParent : MonoBehaviour
{
    //Nav Builder
    public GameObject NavBuilder;
    //Troop Manager
    public GameObject troopManager;
    TroopManager troopScript;
    //Enemy troop Manager
    public GameObject enemyTroopManager;
    enemyTroopManager enemyScript;
    //Canvas
    GameObject playCanvas;
    
    //Team Info on Mesh
    GameObject TeamInfo;

    //Gaming Scale 
    Dictionary<string, float> gameScaleInfo;
    
    // Start is called before the first frame update
    void Awake()
    {
        //Team Info off
        TeamInfo = GameObject.Find("TeamInfo");
        TeamInfo.SetActive(false);
        //Turn off canvass
        playCanvas = GameObject.Find("PlayCanvas");
        playCanvas.SetActive(false);
        Debug.Log(playCanvas);

        troopScript = troopManager.GetComponent<TroopManager>();
        enemyScript = enemyTroopManager.GetComponent<enemyTroopManager>();

        //Scale items
        setGameScaleDict();
    }
    //Set game scale info:
    private void setGameScaleDict()
    {
        gameScaleInfo = new Dictionary<string, float>();
        //Scale items
        gameScaleInfo.Add("GameScale", transform.localScale.x);
        gameScaleInfo.Add("StandardStopDist", gameScaleInfo["GameScale"] * 0.25f);
        gameScaleInfo.Add("StandardShotDist",gameScaleInfo["GameScale"] * 1f);
    }


    public void onHitConfirm() {
        //Instantiate(NavBuilder, this.transform);
        
        
        GameObject surface = GameObject.Find("Plane");
        NavMeshSurface surfaceC = surface.GetComponent<NavMeshSurface>();
        surfaceC.BuildNavMesh();
        try {
            GameObject.Find("Game Surface Folder(Clone)").GetComponent<GameSurfaceParent>().startGame();
        } catch(NullReferenceException e)
        {
            GameObject.Find("Game Surface Folder").GetComponent<GameSurfaceParent>().startGame();
        }
        TeamInfo.SetActive(true);
    }

    public void disableNavBuilder()
    {
       //GameObject.Find("LocalNavMeshBuilder(Clone)").GetComponent<LocalNavMeshBuilder>().OnDisable();
       
    }

    //Create starting points for the two teams
    public void startGame() 
    {
        troopScript.instantiateSquad();
        enemyScript.instantiateEnemy();
        enemyScript.startTimer();
        playCanvas.SetActive(true);
    }

    public Dictionary<string, float> getGameScale()
    {
        return gameScaleInfo;
    }
}
