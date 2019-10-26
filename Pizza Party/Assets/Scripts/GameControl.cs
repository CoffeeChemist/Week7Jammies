using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    private static GameControl instance = null;
    [SerializeField] private int totalRounds;

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
}

public struct Round
{
    public bool hasDrawn;
    public int numDrawn;
    public int[] playerRanking;
}
