using System;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;

public interface IRelicEffect
{
    void ApplyEffect();
    void RemoveEffect(); 
}

public class RelicEffect
{
    // amount applied
    protected string amountToApply { get; set; } = null;
    // amount to check
    protected string amountToCheck { get; set; } = null;
    // time (incase duration)
    protected string time { get; set; } = null;

    protected Action trigger { get; set; } = null;
    
    //protected Action triggerInitial { get; set; } = null;
    protected RelicTrigger relicTrigger { get; set; } = new RelicTrigger();
    public RelicTimer timer;

    public RelicEffect(Action trigger, RelicTrigger relicTrigger)
    {
        this.trigger = trigger;
        //this.triggerInitial = triggerInitial;
        this.relicTrigger = relicTrigger;
    }

    // Called in place of the action inside a trigger
    public void ApplyEffect()
    {
        if (time != null)
        {
            //this.triggerInitial += RelicTimeCheck;
            OnRelicTimeCheck();
            this.trigger += OnAction;

        }
        else
        {
            this.trigger += OnCheck;
        }
    }
    protected virtual void OnCheck()
    {
        // calls internal check, if true OnAction()
        if (relicTrigger.TestCheck(amountToCheck)) OnAction();
    }

    // Early exit condition for effect (removes timer, but since its effect, reverses given effect, so either way both lead to end of effect)
    public virtual void OnRelicTimeCheck()
    {
        if (timer != null)
        {
            OnAction();
            timer.OnTimerFinished -= OnAction;
            timer.Cancel();
            timer = null;
        }

        timer = new RelicTimer(RPNEvaluator.RPNEvaluator.Evaluatef(amountToCheck, GameManager.Instance.variables));
        timer.OnTimerFinished += OnAction;
    }
    // should reverse all values
    protected virtual void OnAction()
    {
    }
}
