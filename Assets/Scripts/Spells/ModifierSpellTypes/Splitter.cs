using UnityEngine;
using System.Collections;

public class SplitterModifier : SpellModifier
{
    //constructor
    public SplitterModifier(Spell inner, SpellCaster owner) : base(inner){}

    protected override void ApplyModifier(ValueModifier modifier)
    {
        
        //testing the wrapper, this is hard coded - reoptimize with actual implementation later
        

    }

}