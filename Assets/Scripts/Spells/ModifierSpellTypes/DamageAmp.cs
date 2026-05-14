using UnityEngine;
using System.Collections;

public class DamageAmpModifier : SpellModifier
{
    //constructor
    public DamageAmpModifier(Spell inner) : base(inner)
    {
        this.modData = GameManager.Instance.spells["damage-amplified"];
    }

    

}