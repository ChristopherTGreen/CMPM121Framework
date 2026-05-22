using UnityEngine;
using System.Collections;

public class DamageAmpModifier : SpellModifier
{
    //constructor
    public DamageAmpModifier(Spell inner) : base(inner)
    {
        this.modData = GameManager.Instance.spells["damage-amplified"];
        this.modData.damage_adder = "0";
        this.modData.damage_multiplier = "2";
        Debug.Log("Modifier: Damage Amp Constructed");
    }

}