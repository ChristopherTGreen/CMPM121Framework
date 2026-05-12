using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

//Notes to self:
// Lecture 10 - Probably use decorator Pattern for a spell class? Similar to template?
// Lecture 11 - Reference the Slay the Spire structure
// Lecture 12 - Take a look at lecture 12 later today...



// A spell template
// Base spell classes should be a child of this class
public class Spell 
{
    public float last_cast;
    public SpellCaster owner;
    public Hittable.Team team;

    // modifiable data below
    public int icon { get; set; } = 0;
    public string name { get; set; } = null; // should this be restricted to its own spell (not base class) - chris
    public string description { get; set; } = null; // should this be restricted to its own spell (not base class) - chris
    public string baseTrajectory { get; set; } = null;

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



    // Do not get rid of this, this is the Spell class' constructor. 
    // This also gets called in the SpellModifier COnstructor.
    public Spell(SpellCaster owner) // change this probably - chris
    {
        this.owner = owner;
    }



    // Not too sure if we should merge the cast here with the cast above - chris
    // Nope don't merge it - Jay
    public void Cast()
    {

        Cast(new ValueModifier());

    }



    // This is a editable method that the Children of the spell class can edit
    // This should be edited in a base spell class - ArcaneBolt, Arcane Spray, Magic Missile, Arcane Explosion (See assignment)
    // Do not edit this method in the SpellModifier class, this method is not for that class
    protected virtual void Cast(ValueModifier modifier){}


    
    // This makes the projectile. I think this should be put in the base spell instead regarding projectiles
    // Editable method
    //Commenting out temporarily

    /*
    public virtual IEnumerator CastRoutine(Vector3 where, Vector3 target, Hittable.Team team)
    {
        this.team = team;
        GameManager.Instance.projectileManager.CreateProjectile(GetIcon(), GetTrajectory(), where, target - where, GetSpeed(), OnHit);
        yield return new WaitForEndOfFrame();
    }
    */



    //Commenting out for now. Need to rework this so there's more customization over the spell
    // Spell actions shouldn;t just happen upon a hit

    /*
    void OnHit(Hittable other, Vector3 impact)
    {
        if (other.team != team)
        {
            other.Damage(new Damage(GetDamage(), Damage.Type.ARCANE));
            GameManager.Instance.sessionStats.totalDamageDealt += GetDamage();
        }

    }
    */




    // Note: 
    // The original version of OnHit and IsReady has GetDamage(), not too sure if we need them to be get calls which can be overriden or not - chris



    // Every child of this class will have this Get method - not great for modifiers.
    /*
    public int GetIcon()
    {
        return icon;
    }

    public string GetTrajectory()
    {
        return baseTrajectory;
    }

    public int GetDamage()
    {
        return baseDamage.amount;
    }
    public Damage.Type GetDamageType()
    {
        Damage.Type type = baseDamage.type;
        return type;
    }
    public int GetHeal()
    {
        return baseHeal;
    }
    public float GetSpeed()
    {
        return baseSpeed;
    }
    public int GetNumber()
    {
        return baseNumber;
    }

    public int GetManaCost()
    {
        return baseManaCost;
    }
    public float GetCooldown()
    {
        return baseCooldown;
    }
    public float GetAngle()
    {
        return baseAngle;
    }
    public float GetDelay()
    {
        return baseDelay;
    }
    public float GetLifetime()
    {
        return baseLifetime;
    }
    */

    // IsReady() 
    // Seems to return if the spell is ready to be spawned if player clicks a button
    // Cooldown timer for the spell. Checks when the spell is avaliable to use. - Jay
    public bool IsReady() 
    {
        return (last_cast + baseCooldown < Time.time);
    }
    


}
