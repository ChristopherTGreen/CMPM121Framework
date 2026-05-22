using System;
using System.Collections.Generic;
using System.Text;

public class DamageMulModifier : SpellModifier
{
    //constructor
    public DamageMulModifier(Spell inner) : base(inner)
    {
        this.modData = GameManager.Instance.spells["damage-amplified"];
        this.modData.damage_adder = "0";
        this.modData.damage_multiplier = "2";
        //Debug.Log("Modifier: Damage Amp Constructed");
    }

}