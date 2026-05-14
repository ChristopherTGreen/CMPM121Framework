using UnityEngine;
using System.Collections;

public class HomingModifier : SpellModifier
{
    //constructor
    public HomingModifier(Spell inner) : base(inner)
    {
        this.modData = GameManager.Instance.spells["homing"];
    }

    

}