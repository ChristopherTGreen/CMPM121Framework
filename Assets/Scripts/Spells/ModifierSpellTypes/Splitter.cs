using UnityEngine;
using System.Collections;

public class SplitterModifier : SpellModifier
{
    //constructor
    public SplitterModifier(Spell inner, SpellCaster owner) : base(inner, owner){}

    protected override void ApplyModifier(Spell innerspell)
    {
        
        //testing the wrapper, this is hard coded - reoptimize with actual implementation later
        

    }

}