using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using UnityEngine.SceneManagement;

public class JumpingManager : MonoBehaviour
{
    public GameObject[] p_topping = new GameObject[4];
    public GameObject[] stopper = new GameObject[4];
    public Log[] logs = new Log[4];
    public Player[] p_input = new Player[4];
    public Sprite[] sprites = new Sprite[9];

    

    public Vector3[] Log_start = new Vector3[4];
    public Vector3[] Topping_start = new Vector3[4];
    public Vector2 forcevec;
    public Vector2 jumpvec;


    private float speed;
    private float timer1;
    private float[] timer2 = new float[4];
    private float[] min_height = new float[4];

    public bool[] InAir = new bool[4];
    public bool[] Alive = new bool[4];
    public bool[] First_check = new bool[4];
    public bool cancontinue ;
    public bool end_result;
    public bool[] Onwayback = new bool[4];


    public int[] winners = new int[4];

    public struct Log // I really thought there was going to  be more stuff that should be packed neatly into a struct.
    {                   // and now I'm too lazy+sleepy to change that :v 
        public GameObject _object;

      
        public int _speed;


    }

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




    // Start is called before the first frame update
    void Start()
    {
 

        for (int i = 0; i < 4; i++) // setup for topping objects, input and some general variables
        {
            p_topping[i] = new GameObject("Topping");
            p_topping[i].AddComponent<SpriteRenderer>();
            p_topping[i].GetComponent<SpriteRenderer>().sprite = sprites[Sprite_Decode(i)];
            p_topping[i].AddComponent<Rigidbody2D>();
            p_topping[i].GetComponent<Rigidbody2D>().gravityScale = 0;
            p_topping[i].GetComponent<Transform>().Translate(Topping_start[i]);
            p_input[i] = ReInput.players.GetPlayer(i);

            min_height[i] = Topping_start[i].y;

            Alive[i] = true;
            First_check[i] = true;
            end_result = true;
            Onwayback[i] = false;
        }
        jumpvec = new Vector2(-500, 500);
        cancontinue = true;
    }

    void Update()
    {
        if (cancontinue) // same deal as in balancing game, results will be "ready" when !cancontinue
        {
            cancontinue = false;
            if (Random.value >= 0.75f && timer1 >= 3f) //1/4 chance of spawning every frame after 3 secs. The logs spawn very soon after the 3s mark, but it's not perfect 3s so (for me at least lol) it feels less like a timer
            {
                // Debug.Log("Random chance xddd I am alive");
                for (int i = 0; i < 4; i++) // setup for log objects
                {

                    logs[i]._object = new GameObject("Log");
                    logs[i]._object.AddComponent<SpriteRenderer>();
                    logs[i]._object.AddComponent<Rigidbody2D>();
                    logs[i]._object.AddComponent<LogRemover>();
                    logs[i]._object.GetComponent<LogRemover>().player = i;
                    logs[i]._object.GetComponent<Transform>().Translate(Log_start[i]);
                    logs[i]._object.GetComponent<Rigidbody2D>().gravityScale = 0;
                    logs[i]._object.GetComponent<SpriteRenderer>().sprite = sprites[8];
                    logs[i]._speed = 1;
                    logs[i]._object.GetComponent<Rigidbody2D>().AddForce(forcevec * logs[i]._speed);
                }

                timer1 = 0f;
            }

            for (int i = 0; i < 4; i++) //janky jumping mechanics
            {

                if (InAir[i] && timer2[i] < 9.75f) //start falling (and nail that superhero landing lol)
                {

                    p_topping[i].GetComponent<Rigidbody2D>().AddForce(Vector2.down * 2000, ForceMode2D.Force);
                    Onwayback[i] = true;

                }

                if (p_topping[i].GetComponent<Rigidbody2D>().position.y < min_height[i] +2  && Onwayback[i]) // return to your place
                {
                    p_topping[i].GetComponent<Rigidbody2D>().Sleep();
                    InAir[i] = false;
                    Onwayback[i] = false;
                    p_topping[i].GetComponent<Rigidbody2D>().MovePosition(Topping_start[i]);
                    p_topping[i].GetComponent<SpriteRenderer>().sprite = sprites[Sprite_Decode(i)];

                }

                if ((p_input[i].GetButton("Action") || Input.GetKeyDown(KeyCode.Space)) && !InAir[i]) // press to jump
                {
                    //  Debug.Log("I am pressing a button on controller nr: " + i);
                    p_topping[i].GetComponent<Rigidbody2D>().WakeUp();
                    InAir[i] = true;
                    p_topping[i].GetComponent<Rigidbody2D>().AddForce(Vector2.up * 2000, ForceMode2D.Force);
                    timer2[i] = 10;
                    p_topping[i].GetComponent<SpriteRenderer>().sprite = sprites[Sprite_Decode(i) + 1];

                }

                if (!Alive[i]) // flying off to the skies after death (literally, not afterlife style)
                {
                    p_topping[i].GetComponent<Rigidbody2D>().AddForce(jumpvec * 15);
                    p_topping[i].GetComponent<Rigidbody2D>().SetRotation((Time.time % 0.6f) * 600);

                }

            }

            for (int i = 0; i < 4; i++)
            {


                if (!Alive[i] && First_check[i]) // results are being saved on the array
                {
                    winners[3 - i] = i;
                    First_check[i] = false;
                }
            }

            for (int i = 0; i < 4; i++) // can the game stil continiue ?
            {
                if (First_check[i])
                {
                    cancontinue = true;
                }
            }

            timer1 += Time.deltaTime; // a wild variable in it's natural habitat

            for (int i = 0; i < 4; i++) 
            {
                timer2[i] -= Time.deltaTime;
            }
        }
        else if (end_result) // I like it nicely printed once :v
        {
            for (int i = 0; i < 4; i++)
            {
                Debug.Log(winners[i]); // results 
            }
            end_result = false;
        }
    }
}
