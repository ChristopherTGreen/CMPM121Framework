/*
using UnityEngine;
using System.Collections;

public class SpellFactory
{
    
    // Moved from SpellBuilder.cs
    public Spell CreateSpell(SpellCaster owner, SpellData data)
    {
        Spell newSpell = new SpellBuilder(null)
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

}
*/