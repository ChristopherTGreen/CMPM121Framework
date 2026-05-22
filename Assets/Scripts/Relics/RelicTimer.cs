using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class RelicTimer
{
    // general relic timer class, used for the relic 
    public event Action<EventContext> OnTimerFinished;
    private Coroutine timer;
    public bool running { get; private set; } = false;
    public RelicTimer(float amount, EventContext context)
    {
        timer = CoroutineManager.Instance.StartCoroutine(CountDown(amount, context));
    }

    public IEnumerator CountDown(float amount, EventContext context)
    {
        Debug.Log("COUNTDOWN " + amount);
        yield return new WaitForSeconds(amount);
        Debug.Log("COUNTDOWN FINISHED");
        OnTimerFinished?.Invoke(context);
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