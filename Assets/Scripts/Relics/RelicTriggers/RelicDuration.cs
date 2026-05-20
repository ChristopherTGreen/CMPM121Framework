using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class RelicCountdown : RelicTrigger
{

    // might be able to scrap this so it can be used in conjunction with other checks??'
    // might not be able to be used with effects, since we probably don't want condiitonals
    // but for condiitonals, yes ish? probably not, its its own feature, so yeah
    public Action triggerInitial { get; set; } = null;
    public bool running;
    public RelicTimer timer;
    // If trigger front does not exist or is null, it is an until, meaning the effect is applied until needing to be reversed
    // if trigger front does exist, it is a condition check, which then starts timing for an action unless the triggerBack is called, resetting the condition
    // trigger front doesnt always exist, trigger back will
    // say, if you stop moving for 4 seconds, then do this action, the issue is the trigger back is the opposite reaction
    // but, the action has started, and lasts for a duration, then it needs to know when it stops early (or reaches countdown) since it might reset early
    public RelicCountdown(Action trigger, RelicEffect effect, Action triggerInitial = null) : base(trigger, effect)
    {
        
    }

    protected override bool ConditionCheck(string amountToCheck)
    {
        if (RPNEvaluator.RPNEvaluator.Evaluatef(amountToCheck, GameManager.Instance.variables) < time) return true;
        return false;
            

    }

    /*public virtual void RelicTimeCheck()
    {
        if (timer != null)
        {
            timer.OnTimerFinished -= OnAction;
            timer.Cancel();
            timer = null;
        }

        timer = new RelicTimer(RPNEvaluator.RPNEvaluator.Evaluatef(amountToCheck, GameManager.Instance.variables));
        timer.OnTimerFinished += OnAction;
    }*/
}


