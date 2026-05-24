using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class RelicDuration : RelicTrigger
{
    public RelicTimer timer;

    // decides if a break call should start the action or not (should be false if condition, but if effect true since needs to deapply effects)
    public bool earlyCall;

    public RelicDuration(string trigger, string amountToCheck, string triggerSecondary, bool endActionEarly = false) : base(trigger, amountToCheck)
    {
        this.triggerBreak = triggerSecondary;
        EventBus.Instance.Register(triggerSecondary, BreakCall);
        this.earlyCall = endActionEarly;
    }

    private void BreakCall(EventContext context)
    {
        // if timer is not running, why are you here, go back
        if (timer == null) return;
        Debug.Log("break call");
        // if timer is running (exists still) ends event early, calling an action if need be
        switch (earlyCall)
        {
            case true:
                Debug.Log("end effect early");
                Check(context);
                break;
            case false:
                Debug.Log("end condition");
                timer.OnTimerFinished -= base.Check;
                timer = null;
                break;
        }
    }

    protected override bool TriggerCheck(string amountToCheck, EventContext context)
    {
        Debug.Log("Trig check duration");
        // inital call, starts timer, and if followed through, returns true
        if (timer == null)
        {
            Debug.Log("Start Timer");
            
            float duration = RPNEvaluator.RPNEvaluator.Evaluatef(amountToCheck, GameManager.Instance.variables);
            timer = new RelicTimer(duration, context);

            timer.OnTimerFinished += base.Check;
            return false;
        }
        // later call, if the timer exists (not broken call) then we completed countdown
        Debug.Log("completed countdown");
        timer.OnTimerFinished -= base.Check;
        timer = null;
        return true;
    }

}


