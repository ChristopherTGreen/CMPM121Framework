using System;
using System.Collections.Generic;
using System.Text;

class ArcaneBolt : Spell
{
    public ArcaneBolt(SpellCaster owner) : base(owner)
    {
        SpellData data = GameManager.Instance.spells["Arcane Bolt"];
        new SpellBuilder(this)
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
    }
    protected override void Cast(ValueModifier modifier)
    {
        
    }
}