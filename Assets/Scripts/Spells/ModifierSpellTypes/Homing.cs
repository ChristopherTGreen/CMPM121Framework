using UnityEngine;
using System.Collections;

public class HomingModifier : SpellModifier
{
    //constructor
    public HomingModifier(Spell inner, SpellCaster owner) : base(inner){}

    protected override void ApplyModifier(ValueModifier modifier)
    {
        
        //testing the wrapper, this is hard coded - reoptimize with actual implementation later
        

    }

}