using System;
using System.Collections.Generic;
using UnityEngine;

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

    // should we put this somewhere else?
    // Action list below (all called once, not constantly) (relative to the player)
    // DoDamage called when dealing damage
    public event Action<Vector3, Damage, Hittable> OnDamage;
    public void DoDamage(Vector3 where, Damage dmg, Hittable target)
    {
        OnDamage?.Invoke(where, dmg, target);
    }
    // OnDamaged called when dealt damage (assumes subject is player)
    public event Action<Vector3, PlayerController> OnDamageTaken;
    public void DoDamageTaken(Vector3 where, PlayerController target)
    {
        OnDamageTaken?.Invoke(where, target);
    }
    // OnKill called when killing
    public event Action<Vector3, PlayerController> OnKill;
    public void DoKill(Vector3 where, PlayerController owner)
    {
        OnKill?.Invoke(where, owner);
    }
    // OnCast called when casting
    public event Action<Vector3, PlayerController> OnCast;
    public void DoCast(Vector3 where, PlayerController owner)
    {
        OnCast?.Invoke(where, owner);
    }
    // OnMove called when moving
    public event Action<Vector3, PlayerController> OnMove;
    public void DoMove(Vector3 where, PlayerController owner)
    {
        OnMove?.Invoke(where, owner);
    }
    // OnStill called when stopped
    public event Action<Vector3, PlayerController> OnStop;
    public void DoStop(Vector3 where, PlayerController owner)
    {
        OnStop?.Invoke(where, owner);
    }
    // OnWave called when a wave ends
    public event Action OnWave;
    public void DoWave()
    {
        OnWave?.Invoke();
    }


   

    private Dictionary<Action<EventContext>, Delegate> activeWrappers = new();
    // register an action
    public void Register(string eventName, Action<EventContext> listener)
    {
        switch (eventName)
        {
            case "dealt-damage":
                Action<Vector3, Damage, Hittable> handlerOnDamage = (handlerWhere, handlerDmg, handlerHittable) => listener(new EventContext { where = handlerWhere, damage = handlerDmg, hittable = handlerHittable });
                OnDamage += handlerOnDamage;
                break;
            case "take-damage":
                Action<Vector3, PlayerController> handlerTakeDamage = (handlerWhere, handlerPlayer) => listener(new EventContext { where = handlerWhere, player = handlerPlayer });
                OnDamageTaken += handlerTakeDamage;
                break;
            case "on-kill":
                Action<Vector3, PlayerController> handlerOnKill = (handlerWhere, handlerKiller) => listener(new EventContext { where = handlerWhere, player = handlerKiller });
                OnKill += handlerOnKill;
                break;
            case "on-cast":
                Action<Vector3, PlayerController> handlerOnCast = (handlerWhere, handlerCaster) => listener(new EventContext { where = handlerWhere, player = handlerCaster });
                OnCast += handlerOnCast;
                break;
            case "on-move":
                Action<Vector3, PlayerController> handlerOnMove = (handlerWhere, handlerMover) => listener(new EventContext { where = handlerWhere, player = handlerMover });
                OnMove += handlerOnMove;
                break;
            case "on-stop":
                Action<Vector3, PlayerController> handlerOnStop = (handlerWhere, handlerStopper) => listener(new EventContext { where = handlerWhere, player = handlerStopper });
                OnStop += handlerOnStop;
                break;
            case "on-wave":
                Action handlerOnWave = () => listener(new EventContext { });
                OnWave += handlerOnWave;
                break;
        }
    }
    // unregister an action
    public void Deregister(string eventName, Action<EventContext> listener)
    {
        if (activeWrappers.TryGetValue(listener, out Delegate wrapper))
        {
            switch (eventName)
            {
                case "dealt-damage":
                    OnDamage -= (Action<Vector3, Damage, Hittable>)wrapper;
                    break;
                case "take-damage":
                    OnDamageTaken -= (Action<Vector3, PlayerController>)wrapper;
                    break;
                case "on-kill":
                    OnKill -= (Action<Vector3, PlayerController>)wrapper;
                    break;
                case "on-cast":
                    OnCast -= (Action<Vector3, PlayerController>)wrapper;
                    break;
                case "on-move":
                    OnMove -= (Action<Vector3, PlayerController>)wrapper;
                    break;
                case "on-stop":
                    OnStop -= (Action<Vector3, PlayerController>)wrapper;
                    break;
                case "on-wave":
                    OnWave -= (Action)wrapper;
                    break;
            }
        }
        activeWrappers.Remove(listener);
    }
}



// contains all possible storage requirements, such as spell casters, vector 3 locations, Hittable, etc
public class EventContext
{
    public GameObject source; // subject in world who caused action
    public GameObject target;  // subject in world affected by action
    public Vector3 where;
    public string valueName; // given value to pass down (such as spell movement type, and flexability etc
    public float value; // given value, say damage, mana, etc

    // custom variables
    public PlayerController player; // I didn't like doing connections like this, we should probably change this at some point - chris
    public SpellCaster owner;
    public Hittable hittable;
    public Damage damage;
}


