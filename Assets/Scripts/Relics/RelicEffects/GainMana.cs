using System;
using System.Collections.Generic;
using System.Text;

public class GainMana : RelicEffect
{
    protected override void ApplyEffect(EventContext context)
    {
        context.owner.mana = RPNEvaluator.RPNEvaluator.Evaluate(amount, GameManager.Instance.variables);
    }
}
