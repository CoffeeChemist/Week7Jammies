﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaBoard : MonoBehaviour
{
    private GameObject toppings;
    private GameControl gc;
    private int[] minigameIDs = { 3 };
    private int chosenMinigame;


    void Start()
    {
        toppings = GetComponentInChildren<GameObject>();
        gc = FindObjectOfType<GameControl>();
        
        if(gc.currentRound != 0)
        {
            gc.AddRound();
            DecorateBoard();
        }

        gc.currentRound++;
        
        if(gc.currentRound < gc.totalRounds)
        {
            ChooseMinigame();
        }
        else
        {
            //Winning Decorate
            //winning text
            //play winning sounds
            //exit game
        }
    }

    void Update()
    {
        //
    }

    private void DecorateBoard()
    {
        for(int i = 0; i < 4; i++)
        {
            for (int j = 0; j < gc.totalScores[i] - 1; i++)
            {
                toppings.transform.GetChild(i).GetChild(j).gameObject.SetActive(true);
            }
        }
    }

    private void ChooseMinigame()
    {
        chosenMinigame = Random.Range(0, minigameIDs.Length);
    }
}