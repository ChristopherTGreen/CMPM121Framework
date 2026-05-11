using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;


public class SpellBuilder 
{
    // still need someway to declare owner?
    private Spell spell;
    public SpellBuilder WithName(string name) { spell.name = name; return this; }
    public SpellBuilder WithDescription(string description) { spell.description = description; return this; }
    public SpellBuilder WithIcon(int icon) { spell.icon = icon; return this; }
    public SpellBuilder WithDamage(string amount, string type)
    {
        // do dictionary values later
        spell.baseDamage = new Damage(RPNEvaluator.RPNEvaluator.Evaluate(amount, null), Damage.TypeFromString(type));
        return this;
    }
    public SpellBuilder WithTrajectory(string trajectory) { spell.baseTrajectory = trajectory; return this; }
    public SpellBuilder WithSpeed(string speed) { spell.baseSpeed = RPNEvaluator.RPNEvaluator.Evaluate(speed, null); return this; }
    public SpellBuilder WithHeal(string heal) { spell.baseHeal = RPNEvaluator.RPNEvaluator.Evaluate(heal, null); return this; }
    public SpellBuilder WithNumber(string number) { spell.baseNumber = RPNEvaluator.RPNEvaluator.Evaluate(number, null); return this; }
    public SpellBuilder WithManaCost(string manaCost) { spell.baseManaCost = RPNEvaluator.RPNEvaluator.Evaluate(manaCost, null); return this; }
    public SpellBuilder WithCooldown(string cooldown) { spell.baseCooldown = RPNEvaluator.RPNEvaluator.Evaluate(cooldown, null); return this; }
    public SpellBuilder WithAngle(string angle) { spell.baseAngle = RPNEvaluator.RPNEvaluator.Evaluate(angle, null); return this; }
    public SpellBuilder WithDelay(string delay) { spell.baseDelay = RPNEvaluator.RPNEvaluator.Evaluate(delay, null); return this; }
    public SpellBuilder WithLifetime(string lifetime) { spell.baseLifetime = RPNEvaluator.RPNEvaluator.Evaluate(lifetime, null); return this; }
    // Returns the final result after stacking prior spell builder datas
    public Spell Build(SpellCaster owner)
    {
        return spell;
    }
    // Acts as an interface and class call
    public SpellBuilder()
    {
        
    }
    // Temporary factory we may move outside this class
    public Spell CreateSpell(SpellCaster owner, SpellData data)
    {
        Spell newSpell = new SpellBuilder()
            .WithName(data.name)
            .WithDescription(data.description)
            .WithIcon(data.icon)
            .WithDamage(data.damage.amount, data.damage.type)
            .WithTrajectory(data.projectile_trajectory)
            .WithSpeed(data.projectile.speed)
            .WithHeal(data.heal)
            .WithNumber(data.N)
            .WithManaCost(data.mana_cost)
            .WithCooldown(data.cooldown)
            .WithAngle(data.angle)
            .WithDelay(data.delay)
            .WithLifetime(data.projectile.lifetime)
            .Build(owner);
        return newSpell;
    }

    // findValue()
    // check if a field exists and collects the reference (experimental)
    public static bool FindValue(SpellData givenSpell, string valueName, out object value)
    {
        value = givenSpell.GetType().GetProperty(valueName).GetValue(givenSpell, null);
        return false;
    }
    // saving this code for when making random functions for spell grabbing
    /*
    public static void RandomFunc()
    {
        var keys = new List<string>(GameManager.Instance.spells.Keys);
        string randomKey = keys[Random.Range(0, keys.Count)];
        Spell builtSpell = ;
        GameManager.Instance.spells[randomKey];
    }spell.WithDamage(spellData.damage.amount, spellData.damage.type)
    */

}
