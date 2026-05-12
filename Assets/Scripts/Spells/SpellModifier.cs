using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using System.Collections;

// This is the SpellModifier wrapper Template. Any new spell modifier should inherit this class!
public class SpellModifier : Spell
{
    


    // inner should be the spell being wrapped.
    Spell inner;

    /*
    // Variables for base class (we need to find default values)
    SpellStatContainer statContainer { get; set; } = new SpellStatContainer();
    */


    //This is SpellModifier class' constructor. So we can call it like SpellModifier()
    //I THINK this constructor inherits the Spell class (Parent class') Constructor with the "[This Class' Constructor] : base(owner)" where base is the parent class' constructor
    public SpellModifier(Spell inner, SpellCaster owner) : base(owner)
    {

        this.inner = inner; //This line makes this constructor inherit the Spell Class' constructor.
        // It should store the Spell class (Modifier wrapping the Spell)

        // Doing this with the constructors allows you to do 
        //
        // SpellModifier(
        //  Spell(owner), owner
        // ) 
        //
        // I Vaguely remember this from CSE 101 this also should be the decorator pattern - I think...

    }



    //Editing the Castroutine Method from the Parent (Spell) class
    public override IEnumerator CastRoutine(Vector3 where, Vector3 target, Hittable.Team team)
    {

        ApplyModifier(inner);
        yield return inner.CastRoutine(where, target, team);

    }



    //This is a editable method that the children of this class can edit
    // This is where the spell modifier will be
    protected virtual void ApplyModifier(Spell innerspell){}

}
