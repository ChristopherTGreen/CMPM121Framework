using UnityEngine;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Reflection;


public class SpellBuilder 
{
    // still need someway to declare owner?
    private Spell spell = new Spell();
    public SpellBuilder WithDamage(string amount, string type) 
    {
        // do dictionary values later
        spell.baseDamage = new Damage(RPNEvaluator.RPNEvaluator.Evaluate(amount, null), Damage.TypeFromString(type));
        return this;
    }
    public SpellBuilder withIcon(int icon) { spell.icon = icon; return this; }
    public SpellBuilder withTrajectory(string trajectory) { spell.baseTrajectory = trajectory; return this; }
    public SpellBuilder withSpeed(string speed) { spell.baseSpeed = RPNEvaluator.RPNEvaluator.Evaluate(speed, null); return this; }
    public SpellBuilder withHeal(string heal) { spell.baseHeal = RPNEvaluator.RPNEvaluator.Evaluate(heal, null); return this; }
    public SpellBuilder withNumber(string number) { spell.baseNumber = RPNEvaluator.RPNEvaluator.Evaluate(number, null); return this; }
    public SpellBuilder withManaCost(string manaCost) { spell.baseManaCost = RPNEvaluator.RPNEvaluator.Evaluate(manaCost, null); return this; }
    public SpellBuilder withCooldown(string cooldown) { spell.baseCooldown = RPNEvaluator.RPNEvaluator.Evaluate(cooldown, null); return this; }
    public SpellBuilder WithAngle(float angle) { spell.baseAngle = angle; return this; }
    public SpellBuilder withDelay(string delay) { spell.baseDelay = RPNEvaluator.RPNEvaluator.Evaluate(delay, null); return this; }
    public SpellBuilder withLifetime(string lifetime) { spell.baseLifetime = RPNEvaluator.RPNEvaluator.Evaluate(lifetime, null); return this; }

    public Spell Build(SpellCaster owner)
    {
        // need some way to attach onto the spell, without needing to specify?
        // example: new Spell(owner).WithHealth.WithDamage etc

        return new Spell(owner);
    }

    public SpellBuilder()
    {
        
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
    }
    */

}
