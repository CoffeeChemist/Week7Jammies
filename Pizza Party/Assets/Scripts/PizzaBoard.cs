using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PizzaBoard : MonoBehaviour
{
    [SerializeField] private GameObject[] toppings;
    private GameControl gc;
    private int[] minigameIDs = { 3 };
    private int chosenMinigame;
    private TimerObjects timer;
    private AudioSource source;
    [SerializeField] AudioClip[] catchphrase;

    //UI
    [SerializeField] private Text[] minigameDisplay;
    [SerializeField] private Text timeCounter;


    void Start()
    {
        timer = GetComponent<TimerObjects>();
        gc = FindObjectOfType<GameControl>();
        source = GetComponent<AudioSource>();
        
        if(gc.currentRound != 0)
        {
            gc.AddRound();
            DecorateBoard();
        }

        gc.currentRound++;
        
        if(gc.currentRound <= gc.totalRounds)
        {
            ChooseMinigame();
            timer.ResetClock();
            timeCounter.gameObject.SetActive(true);

        }
        else if( gc.currentRound == gc.totalRounds + 1)
        {

            int winner = 0;
            int bestScore = gc.totalScores[0];

            for(int i = 1; i < 4; i++)
            {
                if(gc.totalScores[i] > bestScore)
                {
                    winner = i;
                    bestScore = gc.totalScores[i];
                }
            }

            WinningDecorateBoard(winner);
            minigameDisplay[0].gameObject.SetActive(true);
            switch (winner)
            {
                case 0: minigameDisplay[0].text = "ChockedOrLate Wins"; break;
                case 1: minigameDisplay[0].text = "Pickle-sama Wins!"; break;
                case 2: minigameDisplay[0].text = "Cho Cho Wins!"; break;
                case 3: minigameDisplay[0].text = "Nipping Pineapple Wins"; break;
            }
            timer.ResetClock();
            source.PlayOneShot(catchphrase[winner]);
            
        }
        else
        {
            timer.IncrementTimer();
            if (timer.TimeExpired())
            {
                Application.Quit();
            }
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
            for (int j = 0; j < gc.totalScores[i]; j++)
            {
                toppings[i].transform.GetChild(j).gameObject.SetActive(true);
            }
        }
    }

    private void WinningDecorateBoard(int winningPlayer)
    {
        for (int i = 0; i < 80; i++)
        {
            toppings[winningPlayer].transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    private void ChooseMinigame()
    {
        chosenMinigame = minigameIDs[Random.Range(0, minigameIDs.Length)];
        Debug.Log(chosenMinigame);
        foreach (Text t in minigameDisplay) { t.gameObject.SetActive(true); }

        switch (chosenMinigame)
        {
            case 3:
                minigameDisplay[1].text = "Dish breaker!";
                break;
        }
    }
}
