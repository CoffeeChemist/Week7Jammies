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
    public Sprite[] sprites = new Sprite[8];

    
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


    // Start is called before the first frame update
    void Start()
    {

        for (int i = 0; i < 4; i++)
        {
            p_topping[i] = new GameObject("Topping");
            p_topping[i].AddComponent<SpriteRenderer>();
            p_topping[i].AddComponent<Rigidbody2D>();
            
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
                logs[i]._player = i;
            }

            timer = 0f;
        }


        timer += Time.deltaTime;


    }
}
