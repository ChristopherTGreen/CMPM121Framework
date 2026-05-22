using System;
using System.Collections.Generic;
using System.Text;

public class Relic
{
    public RelicEffect relicEffect; // actual effect itself
    public RelicTrigger applyTrigger;
    public RelicTrigger completeTrigger;
    public string conditionDescription;
    public int sprite;

    public Relic()
    {
        
    }

    public void SetupRelic(RelicEffect relicEffect, RelicTrigger effectStartTrigger, RelicTrigger effectEndTrigger)
    {
        applyTrigger = effectStartTrigger;
        applyTrigger.OnTrigger += relicEffect.StartEffect;

        this.relicEffect = relicEffect;

        completeTrigger = effectEndTrigger;
        completeTrigger.OnTrigger += relicEffect.EndEffect;
    }
    

}