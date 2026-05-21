using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class RelicCountdown : RelicTrigger
{
    public Action triggerBreak;
    public bool running;
    public RelicTimer timer;

    // decides if a break call should start the action or not (should be false if condition, but if effect true since needs to deapply effects)
    public bool earlyCall;

    public RelicCountdown(Action trigger, string amountToCheck, Action triggerSecondary, bool endActionEarly = false) : base(trigger, amountToCheck)
    {
        this.triggerBreak = triggerSecondary;
        triggerBreak += BreakCall;
        this.earlyCall = endActionEarly;
    }

    private void BreakCall()
    {
        // if timer is not running, why are you here, go back
        if (timer == null) return;

        // if timer is running (exists still) ends event early, calling an action if need be
        switch (earlyCall)
        {
            case true:
                Check();
                break;
            case false:
                timer.OnTimerFinished -= base.Check;
                timer = null;
                break;
            default: break;
        }
    }

    protected override bool TriggerCheck(string amountToCheck)
    {
        // inital call, starts timer, and if followed through, returns true
        if (timer == null)
        {
            float duration = RPNEvaluator.RPNEvaluator.Evaluatef(amountToCheck, GameManager.Instance.variables);
            timer = new RelicTimer(duration);

            timer.OnTimerFinished += base.Check;
        }
        // later call, if the timer exists (not broken call) then we completed countdown
        timer.OnTimerFinished -= base.Check;
        timer = null;
        return true;
    }

}


