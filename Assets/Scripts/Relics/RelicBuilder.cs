using System;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEditor.PackageManager;

public class RelicBuilder
{
    public Relic relic = new Relic();


    public RelicBuilder(Relic relic)
    {
        this.relic = relic;
    }
    public Relic Build()
    {
        return relic;
    }

    public RelicBuilder RelicQuickBuilder(RelicData relicData) 
    {
        relic.conditionDescription = relicData.trigger.description;
        relic.sprite = relicData.sprite;

        relic.relicEffect = RelicEffectBuilder(relicData);
        relic.applyTrigger = ConditionTriggerBuilder(relicData);
        relic.completeTrigger = EffectTriggerBuilder(relicData);


        return this;
    }


    public RelicTrigger ConditionTriggerBuilder(RelicData relicData)
    {
        return new RelicTrigger(relicData.trigger.type, relicData.trigger.amount);
    }
    public RelicTrigger EffectTriggerBuilder(RelicData relicData)
    {
        return new RelicTrigger(relicData.effect.until, relicData.effect.check);
    }
    public RelicEffect RelicEffectBuilder(RelicData relicData) 
    {
        RelicEffect relicEffect = RelicEffectFinder(relicData);
        relicEffect.amount = relicData.effect.amount;
        relicEffect.description = relicData.effect.description;
        return relicEffect;
    }
    public RelicEffect RelicEffectFinder(RelicData relicData)
    {
        switch (relicData.effect.type)
        {
            case "gain-mana": return new GainMana();

        }
        throw new Exception("Relic Error: Relic effect type does not exist");



    }
}