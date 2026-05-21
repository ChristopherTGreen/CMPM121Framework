using System;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;

public class RelicEffect
{
    public string description;
    public string type;
    public string amount;
    public string until;

    public SpellCaster caster;

    // constructor given the relevant trigger 
    public RelicEffect(RelicTrigger relicTrigger)
    {
        
    }
    // sub interfaces to prevent messing with apply effects and remove effects
    public void StartEffect()
    {
        ApplyEffect();
    }
    public void EndEffect()
    {
        RemoveEffect();
    }


    protected virtual void ApplyEffect()
    {
        
    }

    protected virtual void RemoveEffect()
    {
    }

    // potential effect locations?

}
