using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using UnityEngine.SceneManagement;

public class BalanceManager : MonoBehaviour
{

    public Vector3 Total_push;
    

    

    
    public GameObject[] p_topping;
 
    public Sprite[] sprites;
  //  public Player[] p_input;
    public Player p1_input;
    public Player p2_input;
    public Player p3_input;
    public Player p4_input;

    public Vector3[] vecpp;

    private float force;
    private int direction;
    public int counter = 0;
    public int i =0;
    private float timer = 0f;
    public float[] tip ;

    // Start is called before the first frame update


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
            p_topping[i] = new GameObject();
            p_topping[i].AddComponent<SpriteRenderer>();
            tip[i] = 0f;
            
        }
         

         
    
        force = UnityEngine.Random.value;
        /*
                for (int i = 0; i < 4; i++)
                {
                   p_input[i] =  ReInput.players.GetPlayer(i);
                }
        */
        p1_input = ReInput.players.GetPlayer(0);
        p2_input = ReInput.players.GetPlayer(1);
        p3_input = ReInput.players.GetPlayer(2);
        p4_input = ReInput.players.GetPlayer(3);


    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(Random.value);
        

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
             vecpp[i] = new Vector3(0.0f, 0.0f, p1_input.GetAxis("HorizontalStick") * -2);

            tip[i] = p_topping[i].GetComponent<Transform>().rotation.eulerAngles.z;


            p_topping[i].GetComponent<SpriteRenderer>().sprite = sprites[Sprite_Decode(i)];
            p_topping[i].GetComponent<Transform>().Rotate(Total_push + vecpp[1]);
        }
         

      

        Debug.Log("Rotation: " + p_topping[0].GetComponent<Transform>().rotation.eulerAngles.z);



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
        }
        

    }
}
