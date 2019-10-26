using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LogRemover : MonoBehaviour
{

    public int player;
    private GameObject mngr;
    
    private float X;
    // Start is called before the first frame update
    void Start()
    {
        mngr = GameObject.Find("Game Manager");

         X =  mngr.GetComponent<JumpingManager>().Topping_start[player].x;
       
    }

    // Update is called once per frame
    void Update()
    {

    //    SpriteRenderer.


        if (Math.Abs( transform.position.x - X) < 2 && !mngr.GetComponent<JumpingManager>().InAir[player])
        {
           // Debug.Log("YOU LOST " + player);
            mngr.GetComponent<JumpingManager>().Alive[player] = false;
        }




        if (transform.position.x < -50)
        {
            Destroy(gameObject);

        }
    }
}
