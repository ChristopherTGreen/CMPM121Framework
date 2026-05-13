using UnityEngine;
using System.Collections;
using NUnit.Framework;

public class DamageDoubleModifier : SpellModifier
{
    //constructor
    public DamageDoubleModifier(Spell inner, SpellCaster owner) : base(inner, owner){}

    protected override void ApplyModifier(Spell innerspell)
    {
        int amount = 5;
        stats.AddValue(new Multiplier<MathOperationsInt, int>(amount), "amount");
        

    }

}