using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
public class GainSpellPower : RelicEffect
{
    protected override void ApplyEffect(EventContext context)
    {
        base.ApplyEffect(context);
        int gainPower = RPNEvaluator.RPNEvaluator.Evaluate(amount, GameManager.Instance.variables);

        context.player.SetPower(context.player.GetPower() + gainPower);

    }
    protected override void RemoveEffect(EventContext context)
    {
        base.ApplyEffect(context);
        int gainPower = RPNEvaluator.RPNEvaluator.Evaluate(amount, GameManager.Instance.variables);

        context.player.SetPower(context.player.GetPower() - gainPower);

        
    }
}
