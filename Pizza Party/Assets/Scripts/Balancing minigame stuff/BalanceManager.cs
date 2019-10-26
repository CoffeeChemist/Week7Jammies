using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using UnityEngine.SceneManagement;

public class BalanceManager : MonoBehaviour
{

    public Vector3 Total_push;
    public Vector3[] vecpp;
    public Vector2 gap;




    public GameObject[] p_topping;
 
    public Sprite[] sprites;
    public Player[] p_input = new Player[4];

    private float timer = 0f;
    public float[] tip;
    private float force;

    private int direction;
    public int counter = 0;

   
    
    public bool[] fall;
   


    int Sprite_Decode(int a)
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

        
        for (int i = 0; i < 4; i++)
        {
            fall[i] = false;
            gap = new Vector2 ( - 18 + i * 12,0);
            p_topping[i] = new GameObject();
            p_topping[i].AddComponent<SpriteRenderer>();    
            p_topping[i].AddComponent<Rigidbody2D>();
            p_topping[i].GetComponent<Rigidbody2D>().gravityScale = 0;
            p_topping[i].GetComponent<Rigidbody2D>().MovePosition(gap);
            tip[i] = 0f;
            
        }
         
        force = UnityEngine.Random.value;
        
        for (int i = 0; i < 4; i++)
        {
            p_input[i] =  ReInput.players.GetPlayer(i);
        }
        

        
    }

   
    void Update()
    {
        
        

        if (force > 0.5f)
        {
            Total_push = Vector3.forward * (1 + force);
        }
        else
        {
            Total_push = Vector3.back * (2 - force);
        }

        for (int i = 0; i < 4; i++)
        {
            if (!fall[i])
            {
                vecpp[i] = new Vector3(0.0f, 0.0f, p_input[i].GetAxis("HorizontalStick") * -1.4f);
                
                tip[i] = p_topping[i].GetComponent<Transform>().rotation.eulerAngles.z;


                p_topping[i].GetComponent<SpriteRenderer>().sprite = sprites[Sprite_Decode(i)];
                p_topping[i].GetComponent<Transform>().Rotate(Total_push + vecpp[i]);
            }
        }
         


        timer += Time.deltaTime;
        if (timer >= 0.5f + ((UnityEngine.Random.value) * 0.1f))
        {
            force = UnityEngine.Random.value;
            timer = 0f;
        }

        for (int i = 0; i < 4; i++)
        {
            if (tip[i] > 30)
            {

                if (tip[i] < 180)
                {
                    p_topping[i].GetComponent<SpriteRenderer>().flipX = true;
                    p_topping[i].GetComponent<SpriteRenderer>().sprite = sprites[Sprite_Decode(i)+1];
                }
                if (tip[i] < 320 && tip[0] > 180)
                {
                    p_topping[i].GetComponent<SpriteRenderer>().sprite = sprites[Sprite_Decode(i)+1];
                }
            }
            if (tip[i] > 60 && tip[i] < 180)
            {
                fall[i] = true;
                p_topping[i].GetComponent<Rigidbody2D>().gravityScale = 1;

            }
            if (tip[i] > 180 && tip[i] < 300)
            {
                fall[i] = true;
                p_topping[i].GetComponent<Rigidbody2D>().gravityScale = 1;
            }
        }

        

    }
}
