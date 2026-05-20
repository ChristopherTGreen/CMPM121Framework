using System;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;

public class RelicEffect
{
    // amount applied
    protected string amountToApply { get; set; } = null;

    protected Action trigger { get; set; } = null;
    protected RelicTrigger relicTrigger { get; set; } = new RelicTrigger(null, null);
    public RelicTimer timer;

    public RelicEffect(Action trigger)
    {
        this.trigger = trigger;
       
    }

    // Called in place of the action inside a trigger
    protected void ApplyEffect()
    {
        
    }

    public virtual void RelicTimeCheck()
    {
        if (timer != null)
        {
            timer.OnTimerFinished -= OnAction;
            timer.Cancel();
            timer = null;
        }

        timer = new RelicTimer(RPNEvaluator.RPNEvaluator.Evaluatef(amountToCheck, GameManager.Instance.variables));
        timer.OnTimerFinished += OnAction;
    }

    protected virtual void OnAction()
    {

    }
}
