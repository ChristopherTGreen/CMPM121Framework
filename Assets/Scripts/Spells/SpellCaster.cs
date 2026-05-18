using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpellCaster 
{
    public int mana;
    public int max_mana;
    public int mana_reg;
    public Hittable.Team team;
    public Spell spell;

    public IEnumerator ManaRegeneration()
    {
        while (true)
        {
            mana += mana_reg;
            mana = Mathf.Min(mana, max_mana);
            yield return new WaitForSeconds(1);
        }
    }

    public SpellCaster(int mana, int mana_reg, Hittable.Team team)
    {
        this.mana = mana;
        this.max_mana = mana;
        this.mana_reg = mana_reg;
        this.team = team;
        //spell = new SpellBuilder().Build(this);
        spell = new ArcaneBolt(this);
        //spell = new DamageAmpModifier(new DamageAmpModifier(baseSpell));
        //spell = new RandomModifier().CreateRandomModifier(baseSpell);
        //spell = new RandomModifier().CreateRandomSpell(this);
        //spell = new DamageAmpModifier(spell);

        spell = new DamageAmpModifier(new DoublerModifier(new HomingModifier(new ArcaneBolt(this))));

        // Storing the first random spell created
        GameManager.Instance.StoreActiveSpell(spell);
        Debug.Log("SpellCaster.cs_SpellCaster(mana, mana_reg, team) >> Stored: " + spell.name + " in activeSpells");
    }

    public void SetMaxMana(int max_mana)
    {
        float perc = this.mana * 1.0f / this.max_mana; // converts to float
        this.max_mana = max_mana;
        this.mana = Mathf.RoundToInt(perc * max_mana);
    }



    public void CurrentActiveSpell(Spell spell)
    {
        this.spell = spell;
    }



    public IEnumerator Cast(Vector3 where, Vector3 target)
    {        
        if (mana >= spell.GetManaCost() && spell.IsReady())
        {
            mana -= spell.GetManaCost();
            yield return spell.CastRoutine(where, target, team);
        }
        yield break;
    }

}
