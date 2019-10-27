using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;

/*
    - smoke

*/



public class Duck_Hunt : MonoBehaviour
{
    public GameObject[] p_aims;
    public GameObject obj_tomato;
    public GameObject[] obj_points;

    public Sprite aims;
    public Sprite[] tomato;
    public Player[] p_input = new Player[4];

    //Aims stuff
    public Vector2 gap;
    public Vector2 userinput;
    private float speed = 12.0f;      //Player Sensi

    //tomato stuff
    public Vector2 tomato_pos;
    private bool tomato_killed = false;
    private bool tomato_iswaiting = true;
    public Vector2  tomato_force;

    //physics stuff
    private float gravity = 9.8f;

    //Gameplay stuff
    private int points_adder = 100;
    public int[] p_points = new int[4];






    // Start is called before the first frame update
    void Start()
    {
        //Init players
        for (int i = 0; i < 4; i++)
        {
            p_input[i] = ReInput.players.GetPlayer(i);
        }


        //Init aim sprites
        for (int i = 0; i < 4; i++)
        {
            gap = new Vector2(-8 + i * 5, 0);
            if (i > 1)
                gap.x += 1;
            p_aims[i] = new GameObject();
            p_aims[i].AddComponent<SpriteRenderer>();
            p_aims[i].AddComponent<Rigidbody2D>();
            p_aims[i].GetComponent<Rigidbody2D>().gravityScale = 0;
            p_aims[i].GetComponent<Rigidbody2D>().MovePosition(gap);
            p_aims[i].GetComponent<SpriteRenderer>().sprite = aims;
        }


        //Init tomato sprite
        tomato_pos = new Vector2(0, -3);
        obj_tomato = new GameObject("tomato");
        obj_tomato.AddComponent<SpriteRenderer>();
        obj_tomato.AddComponent<Rigidbody2D>();
        obj_tomato.AddComponent<BoxCollider2D>();
        obj_tomato.GetComponent<Rigidbody2D>().gravityScale = 0;
        obj_tomato.GetComponent<Rigidbody2D>().MovePosition(tomato_pos);
        obj_tomato.GetComponent<SpriteRenderer>().sprite = tomato[0];
        //translate the tomato in the Z axis so it's behind the background
        obj_tomato.GetComponent<Transform>().Translate(0, 0, 2);


        //Init points
    }







    // Update is called once per frame
    void Update()
    {
        //Updating Aims postions
        for (int i = 0; i < 4; i++)
        {
            //get the Input from Vertical axis and Horizontal Axis
            userinput = new Vector2(p_input[i].GetAxis("HorizontalStick") * speed * Time.deltaTime, p_input[i].GetAxis("VerticalStick") * speed * Time.deltaTime);
            //update the position
            p_aims[i].GetComponent<Transform>().Translate(userinput);
        }


        //Do we need a new tomato ?
        if(tomato_iswaiting)
            tomato_force = new Vector2(UnityEngine.Random.Range(-10.0f, 10.0f), 110.0f);
        tomato_iswaiting = false;
        tomato_killed = false;
        //Adding the force to the tomato
        if (!tomato_killed)
        {
            obj_tomato.GetComponent<Rigidbody2D>().WakeUp();
            obj_tomato.GetComponent<Rigidbody2D>().AddForce(tomato_force);
            if (tomato_force.y > -gravity)
                tomato_force.y -= gravity;
        }
        //Is the tomato outside of the play area ? If, so, reset sprite and position
        if(obj_tomato.transform.position.x > 14 || obj_tomato.transform.position.x < -14 || obj_tomato.transform.position.y > 8 || obj_tomato.transform.position.y < -12 || tomato_killed)
        {
            tomato_killed = true;
            tomato_pos.x = UnityEngine.Random.Range(-5.0f, 5.0f);
            tomato_pos.y = -6;
            obj_tomato.GetComponent<Rigidbody2D>().Sleep();
            obj_tomato.GetComponent<SpriteRenderer>().sprite = tomato[0];
            obj_tomato.GetComponent<Rigidbody2D>().MovePosition(tomato_pos);
            tomato_iswaiting = true;
        }


        //When press ACTION, does it hit ? If it does, update points
        for (int i = 0; i < 4; i++)
        {
            if(!tomato_killed)
            {
                if (p_input[i].GetButtonDown("Action"))
                {
                    if ((Math.Abs(obj_tomato.transform.position.x - p_aims[i].transform.position.x) < 1 && Math.Abs(obj_tomato.transform.position.y - p_aims[i].transform.position.y) < 1) && p_aims[i].transform.position.y > 4 )
                    {
                        tomato_killed = true;
                        obj_tomato.GetComponent<Rigidbody2D>().Sleep();
                        obj_tomato.GetComponent<SpriteRenderer>().sprite = tomato[1];
                        p_points[i] += points_adder;
                        

                        Debug.Log("Player " +i +" has scored " +p_points[i]);
                    }
                }
            }
        }





    }
}
