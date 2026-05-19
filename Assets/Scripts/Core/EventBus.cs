using UnityEngine;
using System;

public class EventBus 
{
    private static EventBus theInstance;
    public static EventBus Instance
    {
        get
        {
            if (theInstance == null)
                theInstance = new EventBus();
            return theInstance;
        }
    }
    // Action list below (all called once, not constantly) (relative to the player)
    // DoDamage called when dealing damage
    public event Action<Vector3, Damage, Hittable> OnDamage;
    public void DoDamage(Vector3 where, Damage dmg, Hittable target)
    {
        OnDamage?.Invoke(where, dmg, target);
    }
    // OnDamaged called when dealt damage
    public event Action<Vector3, Hittable> OnDamageTaken;
    public void DoDamageTaken(Vector3 where, Hittable target)
    {
        OnDamageTaken?.Invoke(where, target);
    }
    // OnKill called when killing
    public event Action<Vector3, Hittable> OnKill;
    public void DoKill(Vector3 where, Hittable target)
    {
        OnKill?.Invoke(where, target);
    }
    // OnCast called when casting
    public event Action<Vector3, SpellCaster> OnCast;
    public void DoCast(Vector3 where, SpellCaster owner)
    {
        OnCast?.Invoke(where, owner);
    }
    // OnMove called when moving
    public event Action<Vector3, SpellCaster> OnMove;
    public void DoMove(Vector3 where, SpellCaster owner)
    {
        OnMove?.Invoke(where, owner);
    }
    // OnStill called when stopped
    public event Action<Vector3, SpellCaster> OnStop;
    public void DoStop(Vector3 where, SpellCaster owner)
    {
        OnStop?.Invoke(where, owner);
    }
    // OnWave called when a wave ends
    public event Action<Vector3, SpellCaster> OnWave;
    public void DoWave(Vector3 where, SpellCaster owner)
    {
        OnWave?.Invoke(where, owner);
    }





}
