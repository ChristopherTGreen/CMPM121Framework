using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Bson;
using UnityEngine;
using UnityEngine.UI;

// This is the SpellModifier wrapper Template. Any new spell modifier should inherit this class!
public class SpellModifier : Spell
{

    // inner should be the spell being wrapped.
    public Spell inner; // inner is holding all the spell data
    public SpellData modData;
    
    // Variables for base class (we need to find default values)
    // inherit stats from spell which is what we pass
    


    //This is SpellModifier class' constructor. So we can call it like SpellModifier()
    //I THINK this constructor inherits the Spell class (Parent class') Constructor with the "[This Class' Constructor] : base(owner)" where base is the parent class' constructor
    public SpellModifier(Spell inner) : base(inner.owner)
    {

        this.inner = inner; //This line makes this constructor inherit the Spell Class' constructor.
        // It should store the Spell class (Modifier wrapping the Spell)

        // Doing this with the constructors allows you to do 
        //
        // SpellModifier(
        //  Spell(owner), owner
        // ) 
        //
        // I Vaguely remember this from CSE 101 this also should be the decorator pattern - I think...

    }



    //Editing the Castroutine Method from the Parent (Spell) class
    protected override void Cast(ValueModifier valueModifier)
    {

        ApplyModifier(valueModifier);
        if (inner is ISpell spellInner)
        {
            spellInner.Cast(valueModifier);
        }
        
    }



    //This is a editable method that the children of this class can edit
    // This is where the spell modifier will be
    protected virtual void ApplyModifier(ValueModifier valueModifier) {}



    // I don't like how we have to do this many overrides but this may be the fix to the data loss issue
    public override int GetIcon()
    {
        return inner.icon;
    }

    public override string GetTrajectory()
    {
        Debug.Log("Debugging from GetTrajectory() in SpellModifier.cs: " + inner.baseTrajectory);
        return inner.baseTrajectory;
    }

    public override int GetDamage()
    { 
        return ValueModifier.GetValue(stats.amount, inner.baseDamage.amount);
    }

    public override Damage.Type GetDamageType()
    {
        Damage.Type type = inner.baseDamage.type;
        return type;
    }

    public override int GetHeal()
    {
        return ValueModifier.GetValue(stats.heal, inner.baseHeal);
    }

    public override float GetSpeed()
    {
        return ValueModifier.GetValue(stats.speed, inner.baseSpeed);
    }

    public override int GetNumber()
    {
        return ValueModifier.GetValue(stats.number, inner.baseNumber);
    }

    public override int GetManaCost()
    {
        return ValueModifier.GetValue(stats.manaCost, inner.baseManaCost);
    }

    public override float GetCooldown()
    {
        return ValueModifier.GetValue(stats.cooldown, inner.baseCooldown);
    }
    public override float GetAngle()
    {
        return ValueModifier.GetValue(stats.angle, inner.baseAngle);
    }
    public override float GetDelay()
    {
        return ValueModifier.GetValue(stats.angle, inner.baseAngle);
    }
    public override float GetLifetime()
    {
        return ValueModifier.GetValue(stats.lifetime, inner.baseLifetime);
    }


}
