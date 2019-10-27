using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PizzaBoard : MonoBehaviour
{
    private GameObject toppings;
    private GameControl gc;
    private int[] minigameIDs = { 3 };
    private int chosenMinigame;
    private TimerObjects timer;

    //UI
    [SerializeField] private Text[] minigameDisplay;
    [SerializeField] private Text timeCounter;


    void Start()
    {
        //toppings = GetComponentInChildren<GameObject>();
        timer = GetComponent<TimerObjects>();
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
            timer.ResetClock();
            timeCounter.gameObject.SetActive(true);

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
        timer.IncrementTimer();
        timeCounter.text = ((int)timer.CurrentTime).ToString();
        if (timer.TimeExpired())
        {
            Debug.Log("Scene loaded: " + chosenMinigame);
            SceneManager.LoadScene(chosenMinigame);
        }
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
        chosenMinigame = minigameIDs[Random.Range(0, minigameIDs.Length)];
        foreach (Text t in minigameDisplay) { t.gameObject.SetActive(true); }

        switch (chosenMinigame)
        {
            case 3:
                minigameDisplay[1].text = "Fruit Shoot!";
                break;
        }
    }
}
