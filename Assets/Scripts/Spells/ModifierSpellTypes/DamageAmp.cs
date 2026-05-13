using UnityEngine;
using System.Collections;

public class DamageAmpModifier : SpellModifier
{
    //constructor
    public DamageAmpModifier(Spell inner) : base(inner)
    {
        this.modData = GameManager.Instance.spells["damage-amplified"];
    }

    protected override void ApplyModifier(ValueModifier modifier)
    {
        new SpellModifierBuilder(modifier).SpellModifierQuickBuilder(this.modData);


    }

}