using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using UnityEngine.SceneManagement;
public class MenuManagerScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject b1;
    public bool b1_bool;
    public GameObject b2;
    public bool b2_bool;
    private readonly int ID = 0;
    private Player p;
    private bool Is_Up;
    private bool Can_switch = true;
    private int clock = 0;


    void Start()
    {
       b1_bool = GameObject.Find("Play button").GetComponent<ButtonScript>().Is_Selected;
       b2_bool = GameObject.Find("Quit button").GetComponent<ButtonScript>().Is_Selected;
       p = ReInput.players.GetPlayer(ID);
    }

    // Update is called once per frame
    void Update()
    {
        if (!Can_switch)
        {
            if (clock >= 10)
            {
                Can_switch = true;
                clock = 0;
            }
            else
            {
                clock++;
            }
        }

        if (p.GetAxis("VerticalStick") != 0.0f && Can_switch)
        {
            if (b1_bool)
            {
                b2_bool = true;
                b1_bool = false;
                Can_switch = false;
            }
            else
            {
                b2_bool = false;
                b1_bool = true;
                Can_switch = false;
            }

        }
    }
}
