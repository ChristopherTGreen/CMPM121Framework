using System;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;

public class RelicEffect
{
    public string amount;
    public string description;

    //public SpellCaster caster;

    // constructor given the relevant trigger 
    public RelicEffect()
    {
        
    }
    // sub interfaces to prevent messing with apply effects and remove effects
    public void StartEffect(EventContext context)
    {
        ApplyEffect(context);
    }
    public void EndEffect(EventContext context)
    {
        RemoveEffect(context);
    }


    protected virtual void ApplyEffect(EventContext context)
    {
        
    }

    protected virtual void RemoveEffect(EventContext context)
    {
    }

    // potential effect locations?

}
