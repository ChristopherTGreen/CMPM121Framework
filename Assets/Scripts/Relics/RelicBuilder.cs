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

    public RelicBuilder RelicQuickBuilder(RelicData relicData, string triggerType) 
    {
        relic.conditionDescription = relicData.trigger.description;
        relic.sprite = relicData.sprite;

        relic.relicEffect = RelicEffectBuilder(relicData);
        relic.applyTrigger = ConditionTriggerBuilder(relicData, triggerType);
        relic.completeTrigger = EffectTriggerBuilder(relicData, triggerType);


        return this;
    }


    public RelicTrigger ConditionTriggerBuilder(RelicData relicData, string triggerType)
    {
        switch (triggerType)
        {
            case "counter": return new RelicCounter(relicData.trigger.type, relicData.trigger.amount);
            case "duration": return new RelicDuration(relicData.trigger.type, relicData.trigger.amount, relicData.trigger.until, false);
            case "instant": return new RelicInstant(relicData.trigger.type, relicData.trigger.amount, relicData.trigger.check);
            default: return new RelicTrigger(triggerType, relicData.trigger.amount);
        }
        
        
    }
    public RelicTrigger EffectTriggerBuilder(RelicData relicData, string triggerType)
    {
        case "counter": return new RelicCounter(relicData.trigger.type, relicData.trigger.amount);
        case "duration": return new RelicDuration(relicData.trigger.type, relicData.trigger.amount, relicData.trigger.until, true);
        case "instant": return new RelicInstant(relicData.trigger.type, relicData.trigger.amount, relicData.trigger.check);
        default: return new RelicTrigger(triggerType, relicData.trigger.amount);
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
            case "gain-damage": return new GainDamage();
            case "gain-spellpower": return new GainSpellPower();
            case "gain-hp": return new GainHp();
            case "gain-maxhp": return new GainMaxHp();
            case "reduce-manacost": return new ReduceManaCost();

        }
        throw new Exception("Relic Error: Relic effect type does not exist");



    }
}