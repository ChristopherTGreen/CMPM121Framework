using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class RandomModifier
{
    private Dictionary<string, Spell> spellDict = new Dictionary<string, Spell> 
    {
        { "ArcaneBolt", new ArcaneBolt(null) }
    };
    private Dictionary<string, SpellModifier> modDict = new Dictionary<string, SpellModifier>
    {
        { "DamageAmp", new DamageAmpModifier(null) }
    };

    //constructor
    public Spell CreateRandomSpell(SpellCaster owner)
    {
        List<string> baseSpellKeys = spellDict.Keys.ToList();
        string randomKey = baseSpellKeys[UnityEngine.Random.Range(0, baseSpellKeys.Count)];
        Spell randomSpell = spellDict[randomKey];


        int randomModCount = UnityEngine.Random.Range(0, GameManager.Instance.wave_count);
        for (int i = 0; i < randomModCount; i++)
        {
            randomSpell = CreateRandomModifier(randomSpell);
        }
        return spellDict[randomKey];

    }
    private Spell CreateRandomModifier(Spell inner)
    {
        List<string> modKeys = modDict.Keys.ToList();
        string randomKey = modKeys[UnityEngine.Random.Range(0, modKeys.Count)];
        SpellModifier randomMod = modDict[randomKey];

        randomMod.inner = inner;
        return randomMod;

    }


    /*protected override void ApplyModifier(ValueModifier modifier)
    {
        this.stats = new SpellModifierBuilder(modifier).SpellModifierQuickBuilder(this.modData);
        Debug.Log($"Damage{this.stats.amount}");
        // ValueModifier.GetValue(this.stats.amount, this.baseDamage.amount)}");
    }*/

}
