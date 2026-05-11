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
    public SpellBuilder WithAngle(float angle) { spell.baseAngle = angle; return this; }


    public Spell Build(SpellCaster owner)
    {
        // need some way to attach onto the spell, without needing to specify?
        // example: new Spell(owner).WithHealth.WithDamage etc

        return new Spell(owner);
    }

    public SpellBuilder(SpellData givenSpell)
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
