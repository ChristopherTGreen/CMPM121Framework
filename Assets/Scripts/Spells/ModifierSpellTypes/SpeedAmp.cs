using UnityEngine;
using System.Collections;

public class SpeedAmpModifier : SpellModifier
{
    //constructor
    public SpeedAmpModifier(Spell inner) : base(inner)
    {
        this.modData = GameManager.Instance.spells["speed-amplified"];
        Debug.Log("Modifier: Spped Amp Constructed");
    }

    

}