using UnityEngine;
using System.Collections;
using NUnit.Framework;

public class DamageDoubleModifier : SpellModifier
{
    //constructor
    public DamageDoubleModifier(Spell inner) : base(inner)
    {
        this.modData = GameManager.Instance.spells["doubler"];
    }

    protected override void ApplyModifier(ValueModifier modifier)
    {
        new SpellModifierBuilder(modifier).SpellModifierQuickBuilder(this.modData);


    }

}