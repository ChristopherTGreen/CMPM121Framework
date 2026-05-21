using System;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;

public class RelicEffect
{   
    //protected Action triggerInitial { get; set; } = null;
    protected RelicTrigger removeTrigger;
    // constructor given the relevant trigger 
    public RelicEffect(RelicTrigger relicTrigger)
    {
        this.removeTrigger = relicTrigger;
        this.removeTrigger.OnTrigger += RemoveEffect; 
    }

    public void CallEffect()
    {
        ApplyEffect();
    }

    protected virtual void ApplyEffect()
    {
        
    }

    protected virtual void RemoveEffect()
    {
    }
}
