using System;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;

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
        Debug.Log("1. Kings Charge constructor started");
        relic.conditionDescription = relicData.trigger.description;
        relic.sprite = relicData.sprite;


        relic.relicEffect = RelicEffectBuilder(relicData);

        Debug.Log("2. Setup code finished");
        relic.applyTrigger = ConditionTriggerBuilder(relicData);
        relic.completeTrigger = EffectTriggerBuilder(relicData);


        return this;
    }


    public RelicTrigger ConditionTriggerBuilder(RelicData relicData)
    {
        string currentType = relicData.trigger.type;
        // most likely triggers which don't need checks or comparisons
        if (relicData.trigger.amount == null) 
        {
            switch (currentType)
            {
                case ("take-damage"): return new RelicTrigger(relicData.trigger.type, null);
                case ("dealt-damage"): return new RelicTrigger(relicData.trigger.type, null);
                case ("on-kill"): return new RelicTrigger(relicData.trigger.type, null);
                case ("cast-spell"): return new RelicTrigger(relicData.trigger.type, null);
                case ("on-wave"): return new RelicTrigger(relicData.trigger.type, null);
            }
        }



        // most likely different special types
        switch (currentType)
        {
            case ("stand-still"): return new RelicDuration(relicData.trigger.type, relicData.trigger.amount, "move"); // technically "move" cold be the effect.until, research this more - chris
            case ("move"): return new RelicDuration(relicData.trigger.type, relicData.trigger.amount, "stand-still");
            case ("on-update"): return new RelicDuration(relicData.trigger.type, relicData.trigger.amount, relicData.trigger.check);
            case ("take-damage"): return new RelicDuration(relicData.trigger.type, relicData.trigger.amount, "take-damage");
        }

        throw new Exception("Relic Condition Trigger: Could not find condition trigger");

        /*
        switch (relicData.trigger.type)
        {
            // ignore below
            case "counter": return new RelicCounter(relicData.trigger.type, relicData.trigger.amount);
            case "duration": return new RelicDuration(relicData.trigger.type, relicData.trigger.amount, relicData.trigger.until, false);
            case "instant": return new RelicInstant(relicData.trigger.type, relicData.trigger.amount, relicData.trigger.check);
            default: return new RelicTrigger(relicData.trigger.type, relicData.trigger.amount);
        }
        */
        
    }
    public RelicTrigger EffectTriggerBuilder(RelicData relicData)
    {
        if (relicData.effect.until == null) return null;
        string currentType = relicData.effect.until;
        
        
        // most likely triggers which don't need checks or comparisons (this is so far all of them for json)
        if (currentType != null)
        {
            switch (currentType)
            {
                case ("move"): return new RelicTrigger(currentType, null);
                case ("stand-still"): return new RelicTrigger(currentType, null);
                case ("cast-spell"): return new RelicTrigger(currentType, null);
                case ("take-damage"): return new RelicTrigger(currentType, null);
            }
        }

        /*

        // most likely different special types
        switch (relicData.trigger.type)
        {
            case ("stand-still"): return new RelicDuration(relicData.trigger.type, relicData.trigger.amount, "move");
        }
        */
        throw new Exception("Relic Condition Trigger: Could not find condition trigger");

        /*switch (triggerName)
        {
            case "counter": return new RelicCounter(relicData.trigger.type, relicData.trigger.amount);
            case "duration": return new RelicDuration(relicData.trigger.type, relicData.trigger.amount, relicData.trigger.until, true);
            case "instant": return new RelicInstant(relicData.trigger.type, relicData.trigger.amount, relicData.trigger.check);
            default: return new RelicTrigger(relicData.trigger.type, relicData.trigger.amount);
        }*/
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