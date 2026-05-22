using System;
using System.Collections.Generic;
using System.Text;

public class Relic
{
    // classes
    public RelicEffect relicEffect; // actual effect itself
    public RelicTrigger applyTrigger;
    public RelicTrigger completeTrigger;
    public string conditionDescription;

    
    // actual information for relic class
    public string name { get; set; } = null;
    public int sprite { get; set; } = 0;


    public Relic()
    {
        //if (relicData != null) Build(relicData);
    }

    public Relic Build(RelicData relicData)
    {
        return new RelicBuilder(this).RelicQuickBuilder(relicData).Build();
    }

    public void Enable()
    {
        applyTrigger.AddObserver();
        completeTrigger.AddObserver();
        applyTrigger.OnTrigger += relicEffect.StartEffect;
        completeTrigger.OnTrigger += relicEffect.EndEffect;
    }

    public void Disable()
    {
        applyTrigger.RemoveObserver();
        completeTrigger.RemoveObserver();
        applyTrigger.OnTrigger -= relicEffect.StartEffect;
        completeTrigger.OnTrigger -= relicEffect.EndEffect;
    }


    





    // get call methods
    public virtual string GetName()
    {
        return name;
    }
    public virtual int GetSprite()
    {
        return sprite;
    }
    public virtual string GetConditionDescription() 
    {
        return conditionDescription;
    }
    public virtual string GetConditionTrigger()
    {
        return applyTrigger.triggerMain;
    }
    public virtual string GetConditionAmount()
    {
        return applyTrigger.amountToCheck;
    }
    public virtual string GetConditionUntil()
    {
        return applyTrigger.triggerBreak;
    }
    public virtual string GetEffectDescription()
    {
        return relicEffect.description;
    }
    public virtual string GetEffectType()
    {
        return completeTrigger.triggerMain;
    }
    public virtual string GetEffectAmount()
    {
        return completeTrigger.amountToCheck;
    }
    public virtual string GetEffectUntil()
    {
        return completeTrigger.triggerBreak;
    }

}