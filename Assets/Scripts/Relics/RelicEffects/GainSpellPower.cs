using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
public class GainSpellPower : RelicEffect
{
    protected override void ApplyEffect(EventContext context)
    {
        int gainPower = RPNEvaluator.RPNEvaluator.Evaluate(amount, GameManager.Instance.variables);
        Debug.Log(context.player.power);
        context.player.SetPower(context.player.GetPower() + gainPower);
        Debug.Log(context.player.power);
    }
    protected override void RemoveEffect(EventContext context)
    {
        int gainPower = RPNEvaluator.RPNEvaluator.Evaluate(amount, GameManager.Instance.variables);
        Debug.Log(context.player.power);
        context.player.SetPower(context.player.GetPower() - gainPower);
        Debug.Log(context.player.power);
    }
}
