using System;
using System.Collections.Generic;
using System.Text;

public class Relic
{
    // amount applied
    protected string amountToApply { get; set; } = null;
    // amount to check
    protected string amountToCheck { get; set; } = null;
    // time (incase duration)
    protected string time { get; set; } = null;

    protected Action trigger { get; set; } = null;
    protected Action triggerBreak { get; set; } = null;

    //protected Action triggerInitial { get; set; } = null;
    protected RelicTrigger relicTrigger { get; set; } = new RelicTrigger();
    public RelicTimer timer;

    public bool running { get; set; } = false;

    public Relic(Action trigger, RelicTrigger relicTrigger, Action triggerBreak)
    {
        this.trigger = trigger;
        //this.triggerInitial = triggerInitial;
        this.relicTrigger = relicTrigger;
        this.triggerBreak = triggerBreak;
        RelicCondition();
    }

    // called to set up condition (maybe make it its own class)
    public void RelicCondition()
    {
        if (time != null)
        {
            //this.triggerInitial += RelicTimeCheck;
        
            this.trigger += OnRelicTimeCheck;

        }
        else
        {
            this.trigger += OnCheck;
        }
    }

    // Called in place of the action inside a trigger
    protected void ApplyEffect()
    {
        effect.ApplyEffect();
    }
    protected virtual void OnCheck()
    {
        running = false;
        // calls internal check, if true OnAction()
        if (relicTrigger.TestCheck(amountToCheck))
        {
            ApplyEffect();
        }
        
    }

    // Early exit condition for effect (removes timer, but since its effect, reverses given effect, so either way both lead to end of effect)
    public virtual void OnRelicTimeCheck()
    {
        if (timer != null)
        {
            this.triggerBreak -= OnRelicTimeCheck;
            timer.OnTimerFinished -= ApplyEffect;
            timer.Cancel();
            timer = null;
        }

        timer = new RelicTimer(RPNEvaluator.RPNEvaluator.Evaluatef(amountToCheck, GameManager.Instance.variables));
        timer.OnTimerFinished += ApplyEffect;
        this.trigger -= OnRelicTimeCheck;
        this.triggerBreak += OnRelicTimeCheck;
    }


}