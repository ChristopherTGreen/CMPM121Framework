using System;
using System.Collections.Generic;
using System.Text;

public class RelicCounter : RelicTrigger
{
    // Essentially a tracker to count up instances of event calls
    public int counter { get; set; } = 0;
    public RelicCounter(Action trigger, RelicEffect effect) : base(trigger, effect) { }

    protected override bool ConditionCheck(string amountToCheck)
    {
        
        if (counter <= RPNEvaluator.RPNEvaluator.Evaluatef(amountToCheck, GameManager.Instance.variables))
        {
            counter = 0;
            return true;
        }
        counter += 1;
        return false;
    }

    protected override void OnAction()
    {

    }
}
