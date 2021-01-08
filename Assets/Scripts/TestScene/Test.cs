using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public GameObject plane;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(plane, this.transform);   
    }

    public void onHitConfirm()
    {
        GameObject.Find("Game Surface Folder(Clone)").GetComponent<GameSurfaceParent>().onHitConfirm();

    }

}
