using UnityEngine;
using System.Collections;

public class DoublerModifier : SpellModifier
{
    //constructor
    public DoublerModifier(Spell inner, SpellCaster owner) : base(inner, owner){}

    protected override void ApplyModifier(Spell innerspell)
    {
        
        //testing the wrapper, this is hard coded - reoptimize with actual implementation later
        

    }

}