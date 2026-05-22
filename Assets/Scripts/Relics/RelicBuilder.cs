using System;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEditor.PackageManager;

public class RelicBuilder
{
    public Relic relic = new Relic();
    public RelicData relicData;


    public RelicBuilder(RelicData relicData)
    {
        this.relicData = relicData;
    }
    public Relic Build()
    {
        return relic;
    }

    public void RelicQuickBuilder() 
    {
        relic.conditionDescription = relicData.trigger.description;
        relic.sprite = relicData.sprite;

        relic.relicEffect = RelicEffectBuilder();
        relic.applyTrigger = ConditionTriggerBuilder();
        relic.completeTrigger = EffectTriggerBuilder();
    }


    public RelicTrigger ConditionTriggerBuilder()
    {
        return new RelicTrigger(relicData.trigger.type, relicData.trigger.amount);
    }
    public RelicTrigger EffectTriggerBuilder()
    {
        return new RelicTrigger(relicData.effect.until, relicData.effect.check);
    }
    public RelicEffect RelicEffectBuilder() 
    {
        RelicEffect relicEffect = RelicEffectFinder();
        relicEffect.amount = relicData.effect.amount;
        relicEffect.description = relicData.effect.description;
        return relicEffect;
    }
    public RelicEffect RelicEffectFinder()
    {
        switch (relicData.effect.type)
        {
            case "gain-mana": return new GainMana();

        }
        throw new Exception("Relic Error: Relic effect type does not exist");



    }
}