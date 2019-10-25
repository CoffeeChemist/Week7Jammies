﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;

public class ReadyUpMenu : MonoBehaviour
{
    private Player[] players;
    private bool[] ready;
    private TimerObjects timer;


    //UI
    [SerializeField] private Text[] readyText;
    [SerializeField] private Text countDownText;

    void Start()
    {
        timer = GetComponent<TimerObjects>();

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
        if (!CheckReady())
        {
            foreach (Player p in players)
            {
                if (p.GetButtonDown("Action"))
                {
                    ready[p.id] = !ready[p.id];
                    readyText[p.id].text = ready[p.id] ? "Ready" : "Not Ready";
                }
            }

            if (CheckReady())
            {
                timer.ResetClock();
                countDownText.gameObject.SetActive(true);
                
            }
        }
        else
        {
            timer.IncrementTimer();
            int time = (int) timer.CurrentTime;
            countDownText.text = time.ToString();
        }
        
    }

    bool CheckReady()
    {
        //foreach(bool b in ready)
        for(int i = 0; i < 1; i++)
        {
            //if (!b)
            if(!ready[i])
            {
                return false;
            }
        }
        return true;
    }
}
