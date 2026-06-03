using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


public class SpellBuilder 
{
    // still need someway to declare owner?
    private Spell spell = new Spell(null);
    public SpellBuilder WithName(string name) { spell.name = name; return this; }
    public SpellBuilder WithDescription(string description) { spell.description = description; return this; }
    public SpellBuilder WithIcon(int icon) { spell.icon = icon; return this; }
    public SpellBuilder WithDamage(string amount, string type)
    {
        // do dictionary values later
        spell.baseDamage = amount; 
        spell.baseDamageType = Damage.TypeFromString(type);
        return this;
    }
    public SpellBuilder WithTrajectory(string trajectory) { spell.baseTrajectory = trajectory; return this; }
    public SpellBuilder WithSpeed(string speed) { spell.baseSpeed = speed; return this; }
    public SpellBuilder WithSprite(int sprite) { spell.sprite = sprite; return this; }
    public SpellBuilder WithHeal(string heal) { spell.baseHeal = heal; return this; }
    public SpellBuilder WithNumber(string number) { spell.baseNumber = number; return this; }
    public SpellBuilder WithSmallNumber(string number) { spell.baseSmallNumber = number; return this; }
    public SpellBuilder WithManaCost(string manaCost) { spell.baseManaCost = manaCost; return this; }
    public SpellBuilder WithCooldown(string cooldown) { spell.baseCooldown = cooldown; return this; }
    public SpellBuilder WithAngle(string angle) { spell.baseAngle = angle; return this; }
    public SpellBuilder WithDelay(string delay) { spell.baseDelay = delay; return this; }
    public SpellBuilder WithLifetime(string lifetime) { spell.baseLifetime = lifetime; return this; }
    public SpellBuilder WithRepeat(string repeat) { spell.baseRepeat = repeat; return this; }
    public SpellBuilder WithPierce(string pierce) { spell.basePierce = pierce; return this; }
    public SpellBuilder WithBounce(string bounce) { spell.baseBounce = bounce; return this; }
    public SpellBuilder WithSize(string size) { spell.baseSize = size; return this; }
    // secondary builders
    public SpellBuilder WithSecondaryDamage(string amount) {spell.baseSecondaryDamage = amount; return this; }
    public SpellBuilder WithSecondaryTrajectory(string trajectory) { spell.baseSecondaryTrajectory = trajectory; return this; }
    public SpellBuilder WithSecondaryLifetime(string lifetime) { spell.baseSecondaryLifetime = lifetime; return this; }
    public SpellBuilder WithSecondarySpeed(string speed) { spell.baseSecondarySpeed = speed; return this; }
    public SpellBuilder WithSecondarySprite(int icon) { spell.baseSecondaryIcon = icon; return this; }


    public Spell Build(SpellCaster owner)
    {
        spell.owner = owner;
        return spell;
    }



    // Acts as an interface and class call
    // this is a constructor for the spell builder - Jay
    // take a peek at SpellModifier.cs I did something similar regarding the constructors
    public SpellBuilder(Spell existingSpell)
    {
        this.spell = existingSpell;
    }


    // Copies all values from a source spell and applies them to the current existing spell (C# clone creates a ghost)
    public SpellBuilder SyncDataFrom(Spell source)
    {
        if (source == null) throw new Exception("invalid syncing data source");
        PropertyInfo[] properties = typeof(Spell).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        foreach (PropertyInfo prop in properties)
        {
            //Debug.Log("prop");
            //Debug.Log(prop.Name);
            // no stats, modData or inner
            if (prop.Name == "stats") continue;
            if (prop.Name == "modData") continue;
            if (prop.Name == "inner") continue;
            // Only copy if we can read from source and write to target
            if (prop.CanRead && prop.CanWrite)
            {
                object value = prop.GetValue(source);
                prop.SetValue(spell, value);
            }
        }
        return this;
    }

    // Quickly constructs a spell incase you don't want to use the building options at the top
    public SpellBuilder SpellQuickBuilder(SpellData data)
    {
        // find a way to remove painful list of if statements please - message to myself chris
        if (data.name != null) WithName(data.name);
        if (data.description != null) WithDescription(data.description);
        if (data.icon != -1) WithIcon(data.icon);
        if (data.damage.amount != null) WithDamage(data.damage.amount, data.damage.type);
        if (data.secondary_damage != null) WithSecondaryDamage(data.secondary_damage);
        if (data.heal != null) WithHeal(data.heal);
        if (data.mana_cost != null) WithManaCost(data.mana_cost);
        if (data.cooldown != null) WithCooldown(data.cooldown);
        if (data.N != null) WithNumber(data.N);
        if (data.small_N != null) WithSmallNumber(data.small_N);
        if (data.repeat != null) WithRepeat(data.repeat);
        if (data.pierce != null) WithPierce(data.pierce);
        if (data.bounce != null) WithBounce(data.bounce);
        if (data.size != null) WithSize(data.size);
        if (data.delay != null) WithDelay(data.delay);
        if (data.angle != null) WithAngle(data.angle);

        if (data.projectile != null)
        {
            if (data.projectile.speed != null) WithSpeed(data.projectile.speed);
            if (data.projectile.lifetime != null) WithLifetime(data.projectile.lifetime);
            if (data.projectile.sprite != -1) WithSprite(data.projectile.sprite);
            if (data.projectile.trajectory != null) WithTrajectory(data.projectile.trajectory);
        }

        if (data.secondary_projectile != null)
        {
            if (data.secondary_projectile.speed != null) WithSecondarySpeed(data.secondary_projectile.speed);
            if (data.secondary_projectile.lifetime != null) WithSecondaryLifetime(data.secondary_projectile.lifetime);
            if (data.secondary_projectile.sprite != -1) WithSecondarySprite(data.secondary_projectile.sprite);
            if (data.secondary_projectile.trajectory != null) WithSecondaryTrajectory(data.secondary_projectile.trajectory);
        }
        return this;
    }




/*
    // findValue()
    // check if a field exists and collects the reference (experimental)
    public static bool FindValue(string valueName, out object value)
    {
        //value = givenSpell.GetType().GetProperty(valueName).GetValue(givenSpell, null);
        return false;
    }
*/



    //Note: We don't need a spell factory in the spell builder.

    /*
    // Temporary factory we may move outside this class
    // we should move this outside of the class. This should be it's own class as we shouldn't be building a spell with the builder inside of the builder - Jay
    public Spell CreateSpell(SpellCaster owner, SpellData data)
    {
        Spell newSpell = new SpellBuilder(spell)
            .WithName(data.name)
            .WithDescription(data.description)
            .WithIcon(data.icon)
            .WithDamage(data.damage.amount, data.damage.type)
            .WithTrajectory(data.projectile_trajectory)
            .WithSpeed(data.projectile.speed)
            .WithHeal(data.heal)
            .WithNumber(data.N)
            .WithManaCost(data.mana_cost)
            .WithCooldown(data.cooldown)
            .WithAngle(data.angle)
            .WithDelay(data.delay)
            .WithLifetime(data.projectile.lifetime)
            .Build(owner);
        return newSpell;
    }
    */


    // saving this code for when making random functions for spell grabbing
    /*
    public static void RandomFunc()
    {
        var keys = new List<string>(GameManager.Instance.spells.Keys);
        string randomKey = keys[Random.Range(0, keys.Count)];
        Spell builtSpell = ;
        GameManager.Instance.spells[randomKey];
    }spell.WithDamage(spellData.damage.amount, spellData.damage.type)
    */

}
