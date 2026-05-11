using System;
using System.Collections.Generic;
using System.Text;

class SpellTest
{
    // still experimenting, ignore this all
    int baseDamage;
    int baseHeal; // look at slay spire, doesn't need declaration, then won't be applied
    int baseLifetime;
    int baseSpeed;
    int baseManaCost;
    int baseCooldown;
    int baseNumber;
    // should we have push, pierce, and accel; and should they be in a modifier instead? I'm leaning towards functionality
    int basePierce; // probably should be functionality
    int basePush;
    int basePull;
    int baseAreaEffect;

    public SpellCaster owner;
    public Hittable.Team team;
    string projecticleTrajectory;
    public void Cast()
    {
        Cast(new ValueModifier)
    }
    // I assume there will be multiple types of this class, or methods which we define, which should be its own class?
    protected virtual void Cast(ValueModifier modifier)
    {

    }
}
