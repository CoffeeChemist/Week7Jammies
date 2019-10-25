using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerObjects : MonoBehaviour
{
    [SerializeField] private int numOfTimers;
    Timer[] timerList;

    void Start()
    {
        timerList = new Timer[numOfTimers];

        for(int i = 0; i < numOfTimers; i++)
        {
            timerList[i].currentTime = 0;
            timerList[i].timeLimit = 0;
        }
    }

    void Update()
    {
        
    }
}

public struct Timer
{
    public float currentTime;
    public float timeLimit;

    public bool TimeExpired()
    {
        if (currentTime >= timeLimit)
        {
            return true;
        }
        return false;
    }
}