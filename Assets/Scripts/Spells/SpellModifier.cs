using System;
using System.Collections.Generic;
using System.Text;

public class SpellModifier : Spell
{
    Spell inner;

    // Variables for base class (we need to find default values)
    SpellStatContainer statContainer { get; set; } = new SpellStatContainer();

    public SpellModifier(SpellCaster owner) : base(owner)
    {
        
    }

    protected override void Cast(ValueModifier modifier)
    {
        inner.Cast();
    }
}
