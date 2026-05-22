using System;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;

public class RelicEffect
{
    public string amount;
    public string description;
    // Check before effect trigger so it doesn't accidentally end effect before even starting it
    public bool applied = false;

    //public SpellCaster caster;

    // constructor given the relevant trigger 
    public RelicEffect()
    {
        
    }
    // sub interfaces to prevent messing with apply effects and remove effects
    public void StartEffect(EventContext context)
    {
        Debug.Log(applied);
        if (this.applied) return;
        this.applied = true;
        Debug.Log("START EFFECT");
        ApplyEffect(context);
    }
    public void EndEffect(EventContext context)
    {
        Debug.Log(this.applied);
        if (!this.applied) return;
        this.applied = false;

        Debug.Log("END EFFECT");
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
