using System;
using System.Collections.Generic;
using System.Text;

public class GainDamage : RelicEffect
{
    protected override void ApplyEffect(EventContext context)
    {
        int damageGain = RPNEvaluator.RPNEvaluator.Evaluate(amount, GameManager.Instance.variables);

        //context.player.spellcaster.spell += spellcaster.spell(manaGain);
    }

    protected override void RemoveEffect(EventContext context)
    {
        int damageGain = RPNEvaluator.RPNEvaluator.Evaluate(amount, GameManager.Instance.variables);

        //context.player.spellcaster.SetMana(manaGain);
    }
}
