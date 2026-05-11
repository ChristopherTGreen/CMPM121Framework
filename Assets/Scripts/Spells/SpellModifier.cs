using System;
using System.Collections.Generic;
using System.Text;

public class SpellModifier : Spell
{
    Spell inner;
    public SpellModifier(SpellCaster owner) : base(owner)
    {
        
    }

    protected override void Cast(ValueModifier modifier)
    {
        inner.Cast();
    }
}
