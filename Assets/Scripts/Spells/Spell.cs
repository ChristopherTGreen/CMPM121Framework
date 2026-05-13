using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

public class Spell 
{
    public float last_cast;
    public SpellCaster owner;
    public Hittable.Team team;
    public ValueModifier stats = new ValueModifier();
    // modifiable data below
    public int icon { get; set; } = 0;
    public string name { get; set; } = null; // should this be restricted to its own spell (not base class) - chris
    public string description { get; set; } = null; // should this be restricted to its own spell (not base class) - chris
    public string baseTrajectory { get; set; } = null;
    public int sprite { get; set; } = 0;
    // Variables for base class (we need to find default values)
    public Damage baseDamage { get; set; } = null;
    public int baseHeal { get; set; } = -1;
    public float baseSpeed { get; set; } = -1;
    public int baseNumber { get; set; } = -1;
    public int baseManaCost { get; set; } = -1;
    public float baseCooldown { get; set; } = -1;
    public float baseAngle { get; set; } = 0;
    public float baseDelay { get; set; } = -1;
    public float baseLifetime { get; set; } = -1;


    // Constructor
    public Spell(SpellCaster owner = null) // change this probably - chris
    {
        this.owner = owner;
    }

    public virtual int GetIcon()
    {
        return icon;
    }

    public virtual string GetTrajectory()
    {
        return baseTrajectory;
    }

    public virtual int GetDamage()
    {
        return baseDamage.amount;
    }

    public virtual Damage.Type GetDamageType()
    {
        Damage.Type type = baseDamage.type;
        return type;
    }

    public virtual int GetHeal()
    {
        return baseHeal;
    }

    public virtual float GetSpeed()
    {
        return baseSpeed;
    }

    public virtual int GetNumber()
    {
        return baseNumber;
    }

    public virtual int GetManaCost()
    {
        return baseManaCost;
    }

    public virtual float GetCooldown()
    {
        return baseCooldown;
    }
    public virtual float GetAngle()
    {
        return baseAngle;
    }
    public virtual float GetDelay()
    {
        return baseDelay;
    }
    public virtual float GetLifetime()
    {
        return ValueModifier.GetValue(stats.lifetime, baseLifetime);
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
        GameManager.Instance.projectileManager.CreateProjectile(GetIcon(), GetTrajectory(), where, target - where, GetSpeed(), OnHit);
        yield return new WaitForEndOfFrame();
    }

    // Not too sure if we should merge the cast here with the cast above - chris
    public void Cast()
    {
        Cast(new ValueModifier());
    }

    // This gets edited by child methods
    protected virtual void Cast(ValueModifier modifier)
    {
        
    }

    void OnHit(Hittable other, Vector3 impact)
    {
        if (other.team != team)
        {
            other.Damage(new Damage(GetDamage(), Damage.Type.ARCANE));
            GameManager.Instance.sessionStats.totalDamageDealt += GetDamage();
        }

    }

    // Note: 
    // The original version of OnHit and IsReady has GetDamage(), not too sure if we need them to be get calls which can be overriden or not - chris

}
