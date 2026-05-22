using System;
using System.Collections.Generic;
using System.Text;

public class GainMaxHp : RelicEffect
{
    protected override void ApplyEffect(EventContext context)
    {
        int hpGain = RPNEvaluator.RPNEvaluator.Evaluate(amount, GameManager.Instance.variables);

        context.hittable.SetMaxHP(hpGain);
    }
}