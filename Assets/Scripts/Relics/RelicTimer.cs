using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class RelicTimer
{
    // general relic timer class, used for the relic 
    public event Action OnTimerFinished;
    private Coroutine timer;
    public bool running { get; private set; } = false;
    public RelicTimer(float amount)
    {
        timer = CoroutineManager.Instance.StartCoroutine(CountDown(amount));
    }

    public IEnumerator CountDown(float amount)
    {
        yield return new WaitForSeconds(amount);
        OnTimerFinished?.Invoke();
    }

    public void Cancel()
    {
        if (timer != null)
        {   
            CoroutineManager.Instance.StopCoroutine(timer);
            timer = null;
        }
    }
}