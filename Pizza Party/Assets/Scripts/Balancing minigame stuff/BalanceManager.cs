using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using UnityEngine.SceneManagement;

public class BalanceManager : MonoBehaviour
{
    private Player p1;
    private Player p2;
    private Player p3;
    private Player p4;
    /////////////////
    public GameObject p1_topping;
    public GameObject p2_topping;
    public GameObject p3_topping;
    public GameObject p4_topping;

    private float force;
    private int direction;


    // Start is called before the first frame update
    void Start()
    {
       force = Random.value;
         
        p1 = ReInput.players.GetPlayer(0);
        p2 = ReInput.players.GetPlayer(1);
        p3 = ReInput.players.GetPlayer(2);
        p4 = ReInput.players.GetPlayer(3);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time % 3 == 0 )
        {
            force = Random.value;
            Debug.Log("now");

        }
    }
}
