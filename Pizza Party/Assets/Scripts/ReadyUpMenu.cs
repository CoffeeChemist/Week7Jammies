using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class ReadyUpMenu : MonoBehaviour
{
    private Player[] players;
    private bool[] ready;

    void Start()
    {
        players = new Player[4];
        ready = new bool[4];
       
        for(int i = 0; i < 4; i++)
        {
            players[i] = ReInput.players.GetPlayer(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Player p in players)
        {
            if (p.GetButtonDown("Action"))
            {
                ready[p.id] = !ready[p.id];
                Debug.Log("At location " + p.id + " boolean is " + ready[p.id]);
            }
        }

        if (CheckReady())
        {
            //MoveScene;
            Debug.Log("change scene");
        }
        
    }

    bool CheckReady()
    {
        foreach(bool b in ready)
        {
            if (!b)
            {
                return false;
            }
        }
        return true;
    }
}
