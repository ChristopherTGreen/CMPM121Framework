using System;
using System.Collections.Generic;
using System.Text;

public class RelicCounter : RelicTrigger
{
    // Essentially a tracker to count up instances of event calls
    public int counter { get; set; } = 0;
    public RelicCounter(Action trigger, RelicEffect effect) : base(trigger, effect) { }

    protected override void ConditionCheck()
    {
        
        if (counter <= RPNEvaluator.RPNEvaluator.Evaluatef(amountToCheck, GameManager.Instance.variables))
        {
            counter = 0;
            OnAction();
        }
        counter += 1;
    }

    protected override void OnAction()
    {

    }
}
