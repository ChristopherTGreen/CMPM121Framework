using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class RandomModifier
{
    private Dictionary<string, Func<SpellCaster, Spell>> spellDict = new Dictionary<string, Func<SpellCaster, Spell>>
    {
        { "ArcaneBolt", (owner) => new ArcaneBolt(owner) }
    };
    private Dictionary<string, Func<Spell, SpellModifier>> modDict = new Dictionary<string, Func<Spell, SpellModifier>>
    {
        { "DamageAmp", (inner) => new DamageAmpModifier(inner) }
    };

    //constructor
    public Spell CreateRandomSpell(SpellCaster owner)
    {
        List<string> baseSpellKeys = spellDict.Keys.ToList();
        string randomKey = baseSpellKeys[UnityEngine.Random.Range(0, baseSpellKeys.Count)];
        Spell randomSpell = spellDict[randomKey].Invoke(owner);
        randomSpell.owner = owner;


        int randomModCount = 2;
        for (int i = 0; i < randomModCount; i++)
        {
            randomSpell = CreateRandomModifier(randomSpell);
        }
        return randomSpell;

    }
    public SpellModifier CreateRandomModifier(Spell inner)
    {
        List<string> modKeys = modDict.Keys.ToList();
        string randomKey = modKeys[UnityEngine.Random.Range(0, modKeys.Count)];
        SpellModifier randomMod = modDict[randomKey].Invoke(inner);

        return randomMod;

    }


    /*protected override void ApplyModifier(ValueModifier modifier)
    {
        this.stats = new SpellModifierBuilder(modifier).SpellModifierQuickBuilder(this.modData);
        Debug.Log($"Damage{this.stats.amount}");
        // ValueModifier.GetValue(this.stats.amount, this.baseDamage.amount)}");
    }*/

}
