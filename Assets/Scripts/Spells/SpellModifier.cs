using System;
using System.Collections.Generic;
using System.Text;

// This should Modify any spell it wraps 
public class SpellModifier : Spell
{

    // inner should be the spell being wrapped.
    protected Spell inner;

    // Variables for base class (we need to find default values)
    SpellStatContainer statContainer { get; set; } = new SpellStatContainer();



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

    protected override void Cast(ValueModifier modifier)
    {
        inner.Cast();
    }
}
