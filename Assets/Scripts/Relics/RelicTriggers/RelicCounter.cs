using System;
using System.Collections.Generic;
using System.Text;

public class RelicCounter : RelicTrigger
{
    // Essentially a live tracker to count up instances of event calls
    public int counter { get; set; } = 0;
    public RelicCounter(string trigger, string amountToCheck) : base(trigger, amountToCheck)
    {

        this.counter = counter;
    }

    protected override bool TriggerCheck(string amountToCheck, EventContext context)
    {
        
        if (counter <= RPNEvaluator.RPNEvaluator.Evaluatef(amountToCheck, GameManager.Instance.variables))
        {
            counter = 0;
            return true;
        }
        counter += 1;
        return false;
    }
}
