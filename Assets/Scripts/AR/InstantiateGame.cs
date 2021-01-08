using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateGame : MonoBehaviour
{
    public GameObject game;
    // Start is called before the first frame update
    void Start()
    {
       Instantiate(game, this.transform.position, this.transform.rotation, this.transform);
    }


}
