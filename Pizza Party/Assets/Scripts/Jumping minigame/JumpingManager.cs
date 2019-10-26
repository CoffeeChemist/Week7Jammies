using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using UnityEngine.SceneManagement;

public class JumpingManager : MonoBehaviour
{
    public GameObject[] p_topping = new GameObject[4];
    public Log[] logs = new Log[4];
    public Player[] p_input = new Player[4];
    public Sprite[] sprites = new Sprite[9];

    public Vector3[] Log_start = new Vector3[4];
    public Vector3[] Topping_start = new Vector3[4];
    public Vector2 forcevec;

    private float speed;
    private float timer;

    public bool[] InAir = new bool[4];
    public bool[] Alive = new bool[4];


    public struct Log
    {
        public GameObject _object;

        public int _player;
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
        /*
        for (int i = 0; i < 4; i++)
        {
            Log_start[i] = new Vector3(100, 100 - i * 10, 0);
        }
        */

        for (int i = 0; i < 4; i++)
        {
            p_topping[i] = new GameObject("Topping");
            p_topping[i].AddComponent<SpriteRenderer>();
            p_topping[i].GetComponent<SpriteRenderer>().sprite = sprites[Sprite_Decode(i)];
            p_topping[i].AddComponent<Rigidbody2D>();
            p_topping[i].GetComponent<Rigidbody2D>().gravityScale = 0;
            p_topping[i].GetComponent<Transform>().Translate(Topping_start[i]);
            
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (Random.value >= 0.75f && timer >= 3f)
        {
            for (int i = 0; i < 4; i++)
            {
                logs[i]._object = new GameObject("Log");
                logs[i]._object.AddComponent<SpriteRenderer>();
                logs[i]._object.AddComponent<Rigidbody2D>();
                logs[i]._object.AddComponent<LogRemover>();
                logs[i]._object.GetComponent<Transform>().Translate(Log_start[i]);
                logs[i]._object.GetComponent<Rigidbody2D>().gravityScale = 0;
                logs[i]._object.GetComponent<SpriteRenderer>().sprite = sprites[8];
                logs[i]._player = i;
                logs[i]._object.GetComponent<Rigidbody2D>().AddForce(forcevec);
            }

            timer = 0f;
        }




        timer += Time.deltaTime;


    }
}
