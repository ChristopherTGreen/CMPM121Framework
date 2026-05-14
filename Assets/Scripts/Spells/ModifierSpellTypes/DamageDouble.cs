using UnityEngine;
using System.Collections;
using NUnit.Framework;

public class DamageDoubleModifier : SpellModifier
{
    //constructor
    public DamageDoubleModifier(Spell inner) : base(inner)
    {
        //Changed to doubled - was getting a key not found error
        this.modData = GameManager.Instance.spells["doubled"];
    }

    protected override void ApplyModifier(ValueModifier modifier)
    {
        new SpellModifierBuilder(modifier).SpellModifierQuickBuilder(this.modData);


    }

}