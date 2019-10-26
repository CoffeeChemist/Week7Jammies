using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using UnityEngine.SceneManagement;

public class JumpingManager : MonoBehaviour
{
    public GameObject[] p_topping = new GameObject[4];
    public Player[] p_input = new Player[4];
    public Sprite[] sprites = new Sprite[8];

    // Start is called before the first frame update
    void Start()
    {

        for (int i = 0; i < 4; i++)
        {
            p_topping[i] = new GameObject();
            p_topping[i].AddComponent<SpriteRenderer>();
            p_topping[i].AddComponent<Rigidbody2D>();
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
