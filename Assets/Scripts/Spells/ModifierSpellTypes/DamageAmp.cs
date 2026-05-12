using UnityEngine;
using System.Collections;

public class DamageAmpModifier : SpellModifier
{
    //constructor
    public DamageAmpModifier(Spell inner, SpellCaster owner) : base(inner, owner){}

    protected override void ApplyModifier(Spell innerspell)
    {
        
        //Add to the damagemodifier list (later dictionary) the multiplied damage
        SpellStatContainer.DamageModifier.Add(new ValueModifier{mathoperations = ValueModifier.MathOperations.ScalarValue, modifieramount = DamageMultipler});

    }

}