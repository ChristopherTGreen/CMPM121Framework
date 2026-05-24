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
    private bool effectApplied;
    public bool applied
    {
        get => effectApplied;
        set
        {
            // Add a stack trace here. This prints the EXACT chain of code 
            // that is calling this line.
            //Debug.Log($"Applied changed to '{value}' by: {System.Environment.StackTrace}");
            effectApplied = value;
        }
    }

    //public SpellCaster caster;

    // constructor given the relevant trigger 
    public RelicEffect()
    {
        
    }
    // sub interfaces to prevent messing with apply effects and remove effects
    public void StartEffect(EventContext context)
    {
        if (this.applied) return;
        this.applied = true;
        Debug.Log("start");

        ApplyEffect(context);
    }
    public void EndEffect(EventContext context)
    {
        if (!this.applied) return;
        this.applied = false;
        Debug.Log("end");
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
