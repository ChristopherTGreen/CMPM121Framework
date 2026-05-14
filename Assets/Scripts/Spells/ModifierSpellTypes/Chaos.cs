using UnityEngine;
using System.Collections;

public class ChaosModifier : SpellModifier
{
    //constructor
    public ChaosModifier(Spell inner) : base(inner)
    {
        this.modData = GameManager.Instance.spells["chaotic"];
    }
}

    

