using System;
using System.Collections.Generic;
using System.Text;

public class GainHp : RelicEffect
{
    protected override void ApplyEffect(EventContext context)
    {
        int manaGain = RPNEvaluator.RPNEvaluator.Evaluate(amount, GameManager.Instance.variables);

        context.player.spellcaster.SetMana(manaGain);
    }
}