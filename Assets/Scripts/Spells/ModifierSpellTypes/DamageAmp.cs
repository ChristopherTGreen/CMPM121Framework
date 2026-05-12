using UnityEngine;
using System.Collections;

public class DamageAmpModifier : SpellModifier
{
    //constructor
    public DamageAmpModifier(Spell inner, SpellCaster owner) : base(inner, owner){}

    protected override void ApplyModifier(Spell innerspell)
    {

        //this should return the float number of the damage multiplier
        float DamageMultipler = float.Parse(GameManager.Instance.spells["damage_amp"].damage_multiplier);
        
        //Add to the damagemodifier list (later dictionary) the multiplied damage
        SpellStatContainer.DamageModifier.Add(new ValueModifier{mathoperations = ValueModifier.MathOperations.ScalarValue, modifieramount = DamageMultipler});

    }

}