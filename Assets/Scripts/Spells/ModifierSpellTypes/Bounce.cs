using UnityEngine;
using System.Collections;


public class BounceModifier : SpellModifier
{
    //constructor
    public BounceModifier(Spell inner) : base(inner)
    {
        this.modData = GameManager.Instance.spells["bounce"];
        Debug.Log("Modifier: Bounce Constructed");
    }

}
