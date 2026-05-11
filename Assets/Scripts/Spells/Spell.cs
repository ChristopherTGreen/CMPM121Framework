using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

public class Spell 
{
    public float last_cast;
    public string name { get; set; } = null; // should this be restricted to its own spell (not base class) - chris
    public SpellCaster owner;
    public Hittable.Team team;
    public int icon { get; set; } = 0;

    // Variables for base class (we need to find default values)
    public int baseDamage { get; set; } = -1;
    public int baseHeal { get; set; } = -1;
    public float baseSpeed { get; set; } = -1;
    public int baseNumber { get; set; } = -1;
    public int baseManaCost { get; set; } = -1;
    public float baseCooldown { get; set; } = -1;
    public int baseLifetime { get; set; } = -1;


    public Spell(SpellCaster owner) // change this probably - chris
    {
        this.owner = owner;
    }

    public interface ISpell
    {

    }

    public int GetDamage()
    {
        return baseDamage;
    }

    // IsReady() 
    // Seems to return if the spell is ready to be spawned if player clicks a button
    public bool IsReady() 
    {
        return (last_cast + baseCooldown < Time.time);
    }

    public virtual IEnumerator CastRoutine(Vector3 where, Vector3 target, Hittable.Team team)
    {
        this.team = team;
        GameManager.Instance.projectileManager.CreateProjectile(0, "straight", where, target - where, 15f, OnHit);
        yield return new WaitForEndOfFrame();
    }
    // Not too sure if we should merge the cast here with the cast above - chris
    public void Cast()
    {
        Cast(new ValueModifier());
    }

    protected virtual void Cast(ValueModifier modifier)
    {
        
    }

    void OnHit(Hittable other, Vector3 impact)
    {
        if (other.team != team)
        {
            other.Damage(new Damage(baseDamage, Damage.Type.ARCANE));
            GameManager.Instance.sessionStats.totalDamageDealt += baseDamage;
        }

    }

    // Note: 
    // The original version of OnHit and IsReady has GetDamage(), not too sure if we need them to be get calls which can be overriden or not - chris

}
