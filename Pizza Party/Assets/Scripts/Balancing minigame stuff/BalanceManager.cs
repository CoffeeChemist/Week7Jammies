using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using UnityEngine.SceneManagement;
using System;

public class BalanceManager : MonoBehaviour
{
    public Vector3[] UpAndDown = new Vector3[4];
    public Vector3 Total_push;
    public Vector3[] vecpp = new Vector3[4];
    public Vector2 gap;
    public Vector2 knife_gap;

    private AudioSource source;
    [SerializeField] private AudioClip[] clips;

    


    public GameObject[] p_topping = new GameObject[4];
    public GameObject[] knifes = new GameObject[4];
    public GameObject background;
    public GameControl gc;


    public Sprite[] sprites = new Sprite[9];
    public Player[] p_input = new Player[4];

    private float timer = 0f;
    public  float[] tip = new float[4];
   // public float[] prev_tip = new float[4];
    private float force;

    
    public  int counter = 0;
    public  int[] winners = new int[4]; // winners[0] is the 1st place. Lower number == lower place
    private int place = 3;

    public bool[] fall = new bool[4];
    public bool[] firstcheck = new bool[4];
    public bool firststdisplay;
    public int any1 ;



    int Sprite_Decode(int a) // decodes the 0 - 4 id from the for loops into sprite id on the sprites array
    {
        switch (a)
        {
            case 0:
                a = 0;
                break;
            case 1:
                a = 2;
                break;
            case 2:
                a = 4;
                break;
            case 3:
                a = 6;
                break;
        }

        return a;
    }

  

    void Start()    
    {

        //general setup

        source = GetComponent<AudioSource>();

       gc = FindObjectOfType<GameControl>();

        for (int i = 0; i < 4; i++)
        {
            fall[i] = false;
            firstcheck[i] = true;
            gap = new Vector2 ( - 18 + i * 12,0);
            knife_gap = new Vector2(-18 + i * 12, -9);
            p_topping[i] = new GameObject("Topping");
            p_topping[i].AddComponent<SpriteRenderer>();    
            p_topping[i].AddComponent<Rigidbody2D>();
            p_topping[i].GetComponent<Rigidbody2D>().gravityScale = 0;
            p_topping[i].GetComponent<Rigidbody2D>().MovePosition(gap);

            knifes[i] = new GameObject("knife");
            knifes[i].AddComponent<SpriteRenderer>();
            knifes[i].GetComponent<SpriteRenderer>().sprite = sprites[8];
            knifes[i].AddComponent<Rigidbody2D>();
            knifes[i].GetComponent<Rigidbody2D>().gravityScale = 0;
            knifes[i].GetComponent<Rigidbody2D>().MovePosition(knife_gap);

            tip[i] = 0f;
            
        }

        background.SetActive(true);
        any1 = 4;
        firststdisplay = true;
        
        
        for (int i = 0; i < 4; i++)
        {
            p_input[i] =  ReInput.players.GetPlayer(i);
        }
        

        
    }
    /// <summary>
    /// 
    /// Wanted to make the toppings go down a little bit, so the stay on top of the knifes, but I have no idea how to do it and what I tried failed
    /// 
    /// </summary>

    void Update()
    {


        if (any1 > 1) // if the game can continue (if anyone is still up)
        {
            if (force > 0.5f) // force is random from 0.0 to 1.0
            {
                Total_push = Total_push -  Vector3.forward * ( force * 0.05f) * (Time.deltaTime) * 90; // forward push
                
                
            }
            else
            {
                Total_push = Total_push - Vector3.back * ( force * 0.05f) * (Time.deltaTime ) * 90; //backward push

            }

            if (Total_push.x >= 2) // this way the push should be more fair, but please test it
            {
                Total_push.x = Vector3.forward.x * 2 - Total_push.x * 0.1f;
            }
            else if (Total_push.x <= -2)
            {
                Total_push.x = Vector3.back.x * 2 - Total_push.x * 0.1f;
            }

            for (int i = 0; i < 4; i++)
            {
                
                if (!fall[i]) // if i is still up
                {
                    vecpp[i] = new Vector3(0.0f, 0.0f, p_input[i].GetAxis("HorizontalStick") * -3f ); // player input
                    UpAndDown[i] = Vector3.down * (tip[i] * 0.1f);

                    //prev_tip[i] = tip[i];
                    tip[i] = p_topping[i].GetComponent<Transform>().rotation.eulerAngles.z; // getting the rotation value 


                    p_topping[i].GetComponent<SpriteRenderer>().sprite = sprites[Sprite_Decode(i)]; // draw the sprite
                    p_topping[i].GetComponent<Transform>().Rotate(Total_push + vecpp[i]); //rotate by the sum of total_push (random one) and vecpp (player controlled one)
                    knifes[i].GetComponent<Transform>().Rotate(-1 * (Total_push + vecpp[i]));
                }
            }



            timer += Time.deltaTime;
            if (timer >= 0.5f + ((UnityEngine.Random.value) * 0.1f)) // randomising force (the random push) on a timer
            {
                force = UnityEngine.Random.value;
                timer = 0f;
            }

            for (int i = 0; i < 4; i++)
            {



                if (tip[i] > 30) // change the sprites and flip depending on the tip
                {

                    if (tip[i] < 180)
                    {
                        p_topping[i].GetComponent<SpriteRenderer>().flipX = true;
                        p_topping[i].GetComponent<SpriteRenderer>().sprite = sprites[Sprite_Decode(i) + 1];
                       
                        
                    }
                    if (tip[i] < 320 && tip[0] > 180)
                    {
                        p_topping[i].GetComponent<SpriteRenderer>().sprite = sprites[Sprite_Decode(i) + 1];
                        
                    }
                }
                if (tip[i] > 60 && tip[i] < 180) // fall if the tip is too high
                {
                    fall[i] = true;
                    p_topping[i].GetComponent<Rigidbody2D>().gravityScale = 1;
                    knifes[i].GetComponent<Rigidbody2D>().gravityScale = 0.5f;

                }
                if (tip[i] > 180 && tip[i] < 300)
                {
                    fall[i] = true;
                    p_topping[i].GetComponent<Rigidbody2D>().gravityScale = 1;
                    knifes[i].GetComponent<Rigidbody2D>().gravityScale = 0.5f;
                }
            }

            for (int i = 0; i < 4; i++)
            {
                if (fall[i] && firstcheck[i]) // if you fell and it is the first time you end up here
                {
                    winners[place] = i; // get the place 4th place = winners[3]. 1st place == winners[0]
                    //Debug.Log(winners[place]);
                    place--; 
                    firstcheck[i] = false; // making sure you won't take 2 places on the table
                    any1 =0; // someone has fallen. maybe it was the last one
                }
            }


            for (int i = 0; i < 4; i++) // check if anyone is still to declare their loss
            {
                if (firstcheck[i]) 
                {
                    any1 ++; // game can continue if someone is not a loser
                }
            }

        }
        else if (firststdisplay) // I"m lazy and only want to call this once
        {

            source.PlayOneShot(clips[winners[0]]);
            
             

            for (int i = 0; i < 4; i++)
            {
                Debug.Log(winners[i]); //winners[] contins the results of the game
            } 
            firststdisplay = false;

            gc.roundList[gc.currentRound - 1].winner = winners[0];
            SceneManager.LoadScene(2);
            
        }
    }

        

    
}
