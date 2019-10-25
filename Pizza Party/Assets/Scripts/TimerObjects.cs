using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerObjects : MonoBehaviour
{
    private float currentTime;
    [SerializeField] private float timeLimit;
    [SerializeField] private bool countDown;

    public float CurrentTime { get => currentTime; set => currentTime = value; }

    public bool TimeExpired()
    {
        if (CurrentTime >= timeLimit)
        {
            return true;
        }
        return false;
    }

    public void ResetClock()
    {

        CurrentTime = countDown ? CurrentTime = timeLimit : CurrentTime = 0;
    }

    public void IncrementTimer()
    {
        if (countDown)
        {
            CurrentTime -= Time.deltaTime;
        }
        else
        {
            CurrentTime += Time.deltaTime;
        }
    }
}