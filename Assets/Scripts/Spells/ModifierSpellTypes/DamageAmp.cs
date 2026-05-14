using UnityEngine;
using System.Collections;

public class DamageAmpModifier : SpellModifier
{
    //constructor
    public DamageAmpModifier(Spell inner) : base(inner)
    {
        this.modData = GameManager.Instance.spells["damage-amplified"];
        //new SpellBuilder(this).Build(inner.owner);
        Debug.Log("ah1");
    }

    /*protected override void ApplyModifier(ValueModifier modifier)
    {
        this.stats = new SpellModifierBuilder(modifier).SpellModifierQuickBuilder(this.modData);
        Debug.Log($"Damage{this.stats.amount}");
        // ValueModifier.GetValue(this.stats.amount, this.baseDamage.amount)}");
    }*/

}