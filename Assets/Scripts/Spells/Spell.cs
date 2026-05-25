using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;
using static UnityEngine.UI.Image;

public interface ISpell
{
    public void Cast(ValueModifier modifier);
}

public class Spell : ISpell
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
    public string baseDamage { get; set; } = "0";
    public Damage.Type baseDamageType { get; set; } = 0;
    public string baseHeal { get; set; } = "0";
    public string baseSpeed { get; set; } = "0";
    public string baseNumber { get; set; } = "1";
    public string baseManaCost { get; set; } = "0";
    public string baseCooldown { get; set; } = "0";
    public string baseAngle { get; set; } = "0";
    public string baseDelay { get; set; } = "1";
    public string baseLifetime { get; set; } = "5";
    public string baseRepeat { get; set; } = "1";
    public string basePierce { get; set; } = "1";
    public string baseBounce { get; set; } = "0";


    // Constructor
    public Spell(SpellCaster owner = null) // change this probably - chris
    {
        this.owner = owner;
    }

    public virtual List<string> GetModName()
    {
        return stats.name;
    }

    public virtual int GetIcon()
    {
        return icon;
    }

    public virtual string GetTrajectory()
    {
        //GetValue(List < ValueModifier<float> > valueMod, float original)
        //Debug.Log("Ahhh");
        //Debug.Log($"Spell.cs_GetTrajectory() >> Base Damage Amount: {ValueModifier.GetValue(this.stats.amount, this.baseDamage.amount)}");
        //Debug.Log($"Spell.cs_GetTrajectory() >> Modified? Damage Amount: { ValueModifier.GetValue(stats.amount, this.baseDamage.amount) }");
        //Debug.Log($"Spell.cs_GetTrajectory() >> Standard Projectile Trajectory: {ValueModifier.GetValue(stats.trajectory, baseTrajectory)}");
        //Debug.Log($"Spell.cs_GetTrajectory() >> Specified Projectile Trajectory (projectile_trajectory): {ValueModifier.GetValue(stats.projectile_trajectory, baseTrajectory)}");
        //Debug.Log("Spell.cs_GetTrajectory() >> Projectile Trajectory Count: " + stats.projectile_trajectory.Count);
        //if (stats.projectile_trajectory.Count >= 4) Debug.Log(stats.projectile_trajectory[3]);

        // if a random spell doesn't have a projectile_trajectory, then it throws a reference error. Hence why I added the conditional

        if (ValueModifier.GetValue(stats.projectile_trajectory, baseTrajectory) != null) return ValueModifier.GetValue(stats.projectile_trajectory, baseTrajectory);
        else return ValueModifier.GetValue(stats.projectile_trajectory, baseTrajectory); // else return the basespell's trajectory
    }

    public virtual int GetDamage()
    {
        //Debug.Log("Damage");
        //force a int after damage multiplier - I dont like this...
        // Spell.cs line 147 doesn't like when I change this method to a float

        return (int)ValueModifier.GetValue(stats.amount, IntRPN(baseDamage));
    }

    public virtual Damage.Type GetDamageType()
    {
        Damage.Type type = baseDamageType;
        return type;
    }

    public virtual int GetHeal()
    {
        return (int)ValueModifier.GetValue(stats.heal, IntRPN(baseHeal));
    }

    public virtual float GetSpeed()
    {
        return ValueModifier.GetValue(stats.speed, FloatRPN(baseSpeed));
    }

    public virtual int GetNumber()
    {
        return ValueModifier.GetValue(stats.number, IntRPN(baseNumber));
    }
    public virtual int GetRepeat()
    {
        return ValueModifier.GetValue(stats.repeat, IntRPN(baseRepeat));
    }


    public virtual int GetManaCost()
    {
        // also force int after manaCost Multiplier - I don't like this either...
        // SpellCaster.cs Line 39 doesn't like when I change this method to float
        return (int)ValueModifier.GetValue(stats.manaCost, IntRPN(baseManaCost));
    }

    public virtual float GetCooldown()
    {
        return ValueModifier.GetValue(stats.cooldown, FloatRPN(baseCooldown));
    }
    public virtual float GetAngle()
    {
        return ValueModifier.GetValue(stats.angle, IntRPN(baseAngle));
    }
    public virtual float GetDelay()
    {
        return ValueModifier.GetValue(stats.delay, FloatRPN(baseDelay));
    }
    public virtual float GetLifetime()
    {
        return ValueModifier.GetValue(stats.lifetime, FloatRPN(baseLifetime));
    }
    public virtual int GetPierce()
    {
        return ValueModifier.GetValue(stats.pierce, IntRPN(basePierce));
    }
    public virtual int GetBounce()
    {
        return ValueModifier.GetValue(stats.bounce, IntRPN(baseBounce));
    }

    // IsReady() 
    // Seems to return if the spell is ready to be spawned if player clicks a button
    public bool IsReady() 
    {
        return (last_cast + GetCooldown() < Time.time);
    }

    public virtual IEnumerator CastRoutine(Vector3 where, Vector3 target, Hittable.Team team)
    {
        this.team = team;
   
        Vector3 direction = new Vector3();

        
        Cast();

        int repeat = GetRepeat();
        int number = GetNumber();
        for (int i = 0; i < repeat; i++)
        {
            for (int j = 0; j < number; j++)
            {
                direction = Quaternion.Euler(0, 0, UnityEngine.Random.Range(-GetAngle() / 2.0f, GetAngle() / 2.0f)) * (target - where).normalized;
                GameManager.Instance.projectileManager.CreateProjectile(GetIcon(), GetTrajectory(), where, direction, GetSpeed(), OnHit, GetLifetime(), GetPierce(), GetBounce(), GetDamage());
            }
            // Wait before the next shot (but don't wait after the final shot)
            if (i < (GetRepeat() - 1))
            {
                yield return new WaitForSeconds(GetDelay());
            }
        }



        yield return new WaitForEndOfFrame();
        EventBus.Instance.DoCast(where, GameManager.Instance.player.GetComponent<PlayerController>());
    }

  

    // This gets edited by child methods
    protected virtual void Cast(ValueModifier modifier)
    {
        
    }

    // Not too sure if we should merge the cast here with the cast above - chris
    public void Cast()
    {
        ((ISpell)this).Cast(new ValueModifier());
    }

    void ISpell.Cast(ValueModifier modifier)
    {
        this.stats = modifier; // saving stats
        this.Cast(modifier);
    }

    public void OnHit(Hittable other, Vector3 impact, int damage)
    {
        if (other.team != team)
        {
            other.Damage(new Damage(damage, GetDamageType()));
            EventBus.Instance.DoDamageDealt(GameManager.Instance.player.GetComponent<PlayerController>().hp);
            GameManager.Instance.sessionStats.totalDamageDealt += damage;
            if (GetHeal() > 0) GameManager.Instance.player.GetComponent<PlayerController>().hp.SetCurrentHP(GetHeal());
        }

    }

    // Note: 
    // The original version of OnHit and IsReady has GetDamage(), not too sure if we need them to be get calls which can be overriden or not - chris
    public int IntRPN(string value)
    {
        //Debug.Log(value);
        return RPNEvaluator.RPNEvaluator.Evaluate(value, GameManager.Instance.variables);
    }
    public float FloatRPN(string value)
    {
        return RPNEvaluator.RPNEvaluator.Evaluatef(value, GameManager.Instance.variables);
    }
}
