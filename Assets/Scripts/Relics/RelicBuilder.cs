using System;
using System.Collections.Generic;
using System.Text;

public class RelicBuilder
{
    public Relic relic;
    public RelicData relicData


    public RelicBuilder(RelicData relicData)
    {
        this.relicData = relicData;
        RelicTriggerBuilder(relic);
    }


    public RelicTrigger ConditionTriggerBuilder()
    {
        RelicTrigger trig = new RelicTrigger(EventBus.Instance.OnDamageTaken, "50");
    }
    public RelicTrigger EffectTriggerBuilder()
    {

    }

    public void RelicEffectBuilder(=)
    {
        string description;
        string amount;
        
    }

    // used for type finding in conditions, or until finding in effects
    public Action findAction(string action) 
    {
        switch (action)
        {
            case "take-damage":
                return EventBus.Instance.OnDamageTaken;
            case "on-damage":
            case "stand-still":
            case "cast-spell":
            case "move":



        }
    }




    public Relic Build()
    {
        return relic;
    }





    // Acts as an interface and class call
    public SpellModifierBuilder(ValueModifier existingValueModifier)
    {
        this.valueMod = existingValueModifier;
    }


}