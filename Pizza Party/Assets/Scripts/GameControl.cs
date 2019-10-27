using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    private static GameControl instance = null;
    public int totalRounds;

    public int[] totalScores = new int[4];
    public Round[] roundList;
    public int currentRound = 0;


    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy();
        }
        Create();

        SetupRounds();
    }

    private void Destroy()
    {
        Debug.Log("Instance Destroyed");
        Destroy(this.gameObject);
    }

    private void Create()
    {
        Debug.Log("Instance Created");
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    private void SetupRounds()
    {
        roundList = new Round[totalRounds];
        for (int i = 0; i < totalRounds; i++)
        {
            roundList[i].winner = -1;
        }
    }

    public void AddRound()
    {
        for(int i = 0; i < 4; i++)
        {
            if (roundList[currentRound].winner == i)
            {
                totalScores[i] += 4;
                break;
            }
        }
    }

    public struct Round
    {
        public int winner;
    }
}
