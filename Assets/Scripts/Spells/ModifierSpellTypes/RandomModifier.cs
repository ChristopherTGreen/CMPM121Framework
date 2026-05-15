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
        { "DamageAmp", (inner) => new DamageAmpModifier(inner) },
        { "Chaos", (inner) => new ChaosModifier(inner) },
        { "Doubler", (inner) => new DoublerModifier(inner) },
        { "Homing", (inner) => new HomingModifier(inner) },
        { "SpeedAmp", (inner) => new SpeedAmpModifier(inner) },

        // Not yet completed - uncomment when completed
        //{ "Splitter", (inner) => new SplitterModifier(inner) }
    };

    //constructor
    public Spell CreateRandomSpell(SpellCaster owner)
    {

        // grabbing a random spell from the spellDict
        List<string> baseSpellKeys = spellDict.Keys.ToList();
        string randomKey = baseSpellKeys[UnityEngine.Random.Range(0, baseSpellKeys.Count)];
        Spell randomSpell = spellDict[randomKey].Invoke(owner);
        randomSpell.owner = owner;

        // adding a # of random modifiers
        int randomModCount = 2;
        for (int i = 0; i < randomModCount; i++)
        {
            randomSpell = CreateRandomModifier(randomSpell);
        }

        // Need to store the randomly modified spell to a dictionary.

        return randomSpell;

    }
    public SpellModifier CreateRandomModifier(Spell inner)
    {
        List<string> modKeys = modDict.Keys.ToList();
        string randomKey = modKeys[UnityEngine.Random.Range(0, modKeys.Count)];
        SpellModifier randomMod = modDict[randomKey].Invoke(inner);

        //updating session stats list with the mod name so it prints in the reward UI description
        GameManager.Instance.sessionStats.currentSpellModNames.Add(randomKey);

        return randomMod;

    }


    /*protected override void ApplyModifier(ValueModifier modifier)
    {
        this.stats = new SpellModifierBuilder(modifier).SpellModifierQuickBuilder(this.modData);
        Debug.Log($"Damage{this.stats.amount}");
        // ValueModifier.GetValue(this.stats.amount, this.baseDamage.amount)}");
    }*/

}
