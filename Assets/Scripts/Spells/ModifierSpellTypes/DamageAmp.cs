using UnityEngine;
using System.Collections;

public class DamageAmpModifier : SpellModifier
{
    //constructor
    public DamageAmpModifier(Spell inner) : base(inner)
    {
        this.inner = inner;
        this.modData = GameManager.Instance.spells["damage-amplified"];
        Debug.Log("Damage Amp Spell Stats: " + this.inner.baseDamage.amount); // is getting the base stats from the spell 
    }

    //Issue here:
    protected override void ApplyModifier(ValueModifier modifier)
    {
        new SpellModifierBuilder(modifier).SpellModifierQuickBuilder(this.modData);
        Debug.Log("Damage Amp Apply Modifier: " + this.inner.baseDamage.amount); // never happens
    }

    

}