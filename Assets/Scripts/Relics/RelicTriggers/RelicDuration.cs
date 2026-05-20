using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class RelicCountdown : RelicTrigger
{
    // Essentially a tracker to count up instances of event calls
    public Action triggerInitial { get; set; } = null;
    public RelicTimer timer;
    // If trigger front does not exist or is null, it is an until, meaning the effect is applied until needing to be reversed
    // if trigger front does exist, it is a condition check, which then starts timing for an action unless the triggerBack is called, resetting the condition
    // trigger front doesnt always exist, trigger back will
    // say, if you stop moving for 4 seconds, then do this action, the issue is the trigger back is the opposite reaction
    // but, the action has started, and lasts for a duration, then it needs to know when it stops early (or reaches countdown) since it might reset early
    public RelicCountdown(Action trigger, RelicEffect effect, Action triggerInitial = null) : base(trigger, effect)
    {
        this.triggerInitial = triggerInitial;
        if (this.triggerInitial != null)
        {
            this.triggerInitial += ConditionCheck;
            RelicTimeCheck();
            this.trigger -= ConditionCheck; // reverses what was done in parent constructor
            
        }
        
    }

    protected override void ConditionCheck()
    {
        if (triggerInitial != null)
        {
            // create temporary timer action, switching current reading of actions
            if (timer.running)
            {
                this.trigger -= ConditionCheck;
                this.triggerInitial += ConditionCheck;
            }
            RelicTimeCheck();
        }
        else
        {
            OnAction();
        }


    }
    // Coroutine running (calls action after what would be all frames being run
    // Checks for complete call back of the coroutine
    protected virtual IEnumerator ConditionCheckRoutine()
    {
        yield return new WaitForSeconds(RPNEvaluator.RPNEvaluator.Evaluatef(amountToCheck, GameManager.Instance.variables));
        OnAction();
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


    protected override void OnAction()
    {
        
    }
}


