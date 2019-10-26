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
        if(instance != null && instance != this)
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
        for(int i = 0; i < totalRounds; i++)
        {
            roundList[i].hasDrawn = false;
            roundList[i].numDrawn = 0;
            roundList[i].playerRanking = new int[4];
        }
    }

    public void AddRound()
    {
        //for each player
        for(int i = 0; i < 4; i++)
        {
            //for each entry
            for(int j = 0; j < 4; j++)
            {
                //Check if entry is the player
                if(roundList[currentRound].playerRanking[j] == i)
                {
                    //Check if the case is a draw score
                    if (j <= roundList[currentRound].numDrawn - 1)
                    {
                        totalScores[i] += 4;
                    }
                    else
                    {
                        switch (j)
                        {
                            case 1: totalScores[i] += 3; break;
                            case 2: totalScores[i] += 2; break;
                            case 3: totalScores[i] += 1; break;
                        }
                    }
                    break;
                }
            }
        }
    }
}

public struct Round
{
    public bool hasDrawn;
    public int numDrawn;
    public int[] playerRanking;
}
