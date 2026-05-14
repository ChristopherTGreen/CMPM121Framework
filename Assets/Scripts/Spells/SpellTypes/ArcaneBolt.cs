using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using UnityEngine;

class ArcaneBolt : Spell
{
    public ArcaneBolt(SpellCaster owner) : base(owner)
    {
        
        SpellData data = GameManager.Instance.spells["Arcane Bolt"];

        Debug.Log("ArcaneBolt Constructor: Got Arcane Bolt from the GameManager");

        Spell spell = new SpellBuilder(this)
            .WithName(data.name)
            .WithDescription(data.description)
            .WithIcon(data.icon)
            .WithDamage(data.damage.amount, data.damage.type)
            .WithManaCost(data.mana_cost)
            .WithCooldown(data.cooldown)
            .WithTrajectory(data.projectile.trajectory)
            .WithSpeed(data.projectile.speed)
            .WithSprite(data.projectile.sprite)
            .Build(owner);

        Debug.Log("ArcaneBolt Constructor: Finished Building Arcane Bolt");
        Debug.Log("Arcane Bolt Spell Stats: " + spell.baseDamage.amount);
    }

    protected override void Cast(ValueModifier modifier)
    {
        // overrids here should apply any modifiers if there are any, if there aren't then this does nothing (base stats)
        // Jay Testing something:
        //GameManager.Instance.projectileManager.CreateProjectile(GetIcon(), GetTrajectory(), where, target - where, GetSpeed(), OnHit);
    }
}