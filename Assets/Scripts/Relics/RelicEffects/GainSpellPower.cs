using System;
using System.Collections.Generic;
using System.Text;

public class GainSpellPower : RelicEffect
{
    protected override void ApplyEffect(EventContext context)
    {
        int gainPower = RPNEvaluator.RPNEvaluator.Evaluate(amount, GameManager.Instance.variables);

        context.player.power += gainPower;
    }
    protected override void RemoveEffect(EventContext context)
    {
        int gainPower = RPNEvaluator.RPNEvaluator.Evaluate(amount, GameManager.Instance.variables);

        context.player.power -= gainPower;
    }
}
