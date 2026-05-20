using System;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;

public class RelicEffect
{
    // amount applied
    protected string amountToApply { get; set; } = null;
    // amount to check
    protected string amountToCheck { get; set; } = null;

    protected Action trigger { get; set; } = null;
    
    //protected Action triggerInitial { get; set; } = null;
    protected RelicTrigger relicTrigger { get; set; } = new RelicTrigger(null, null);
    public RelicTimer timer;

    public RelicEffect(Action trigger, RelicTrigger relicTrigger)
    {
        this.trigger = trigger;
        //this.triggerInitial = triggerInitial;
        this.relicTrigger = relicTrigger;
        if (this.relicTrigger.time >= 0)
        {
            //this.triggerInitial += RelicTimeCheck;
            OnRelicTimeCheck();
            this.trigger += OnCheck;

        }
        else
        {
            this.trigger += OnCheck;
        }
    }

    // Called in place of the action inside a trigger
    protected virtual void ApplyEffect()
    {
        // place RPN eval and which value being adjusted
    }
    protected virtual void OnCheck()
    {
        // calls internal check, if true OnAction()
        if (relicTrigger.TestCheck(amountToCheck)) ApplyEffect();
    }

    // Early exit condition for effect (removes timer, but since its effect, reverses given effect, so either way both lead to end of effect)
    public virtual void OnRelicTimeCheck()
    {
        if (timer != null)
        {
            ApplyEffect();
            timer.OnTimerFinished -= ApplyEffect;
            timer.Cancel();
            timer = null;
        }

        timer = new RelicTimer(RPNEvaluator.RPNEvaluator.Evaluatef(amountToCheck, GameManager.Instance.variables));
        timer.OnTimerFinished += OnCheck;
    }
}
