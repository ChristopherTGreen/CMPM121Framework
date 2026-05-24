using System;
using System.Collections.Generic;
using System.Text;

public class GainDamage : RelicEffect
{
    protected override void ApplyEffect(EventContext context)
    {
        int damageGain = RPNEvaluator.RPNEvaluator.Evaluate(amount, GameManager.Instance.variables);


        context.damage.amount += damageGain;
    }
}
