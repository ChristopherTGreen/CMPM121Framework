using System;
using System.Collections.Generic;
using System.Text;

public class ReduceManaCost : RelicEffect
{
    protected override void ApplyEffect(EventContext context)
    {
        int reduceManaCost = RPNEvaluator.RPNEvaluator.Evaluate(amount, GameManager.Instance.variables);

        context.player.spellcaster.mana_cost_extra -= reduceManaCost;
    }

    protected override void RemoveEffect(EventContext context)
    {
        int reduceManaCost = RPNEvaluator.RPNEvaluator.Evaluate(amount, GameManager.Instance.variables);

        context.player.spellcaster.mana_cost_extra += reduceManaCost;
    }
}