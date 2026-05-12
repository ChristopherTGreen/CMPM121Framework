using UnityEngine;
using System.Collections;

public class DamageAmpModifier : SpellModifier
{
    //constructor
    public DamageAmpModifier(Spell inner, SpellCaster owner) : base(inner, owner){}

    protected override void ApplyModifier(Spell innerspell)
    {

        float DamageMultipler = float.Parse(GameManager.Instance.spells["damage_amp"].damage_multiplier);
        SpellStatContainer.DamageModifier.Add(new ValueModifier.ScalarValue(baseDamage.amount, DamageMultipler));

    }

}