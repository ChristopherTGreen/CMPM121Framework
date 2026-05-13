using UnityEngine;
using System.Collections;

public class DoublerModifier : SpellModifier
{
    //constructor
    public DoublerModifier(Spell inner, SpellCaster owner) : base(inner){}

    protected override void ApplyModifier(ValueModifier modifier)
    {
        
        //testing the wrapper, this is hard coded - reoptimize with actual implementation later
        

    }

}