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
    // OnDamaged called when dealt damage
    public event Action<Vector3, Hittable> OnDamageTaken;
    public void DoDamageTaken(Vector3 where, Hittable target)
    {
        OnDamageTaken?.Invoke(where, target);
    }
    // OnKill called when killing
    public event Action<Vector3, GameObject> OnKill;
    public void DoKill(Vector3 where, GameObject owner)
    {
        OnKill?.Invoke(where, owner);
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
    public event Action OnWave;
    public void DoWave(Vector3 where, SpellCaster owner)
    {
        OnWave?.Invoke();
    }




    // helper methods to help map events
    public void RegisterEmpty(string eventName, Action listener)
    {
        //if (eventName == "move") OnPlayerMoved += listener;
    }

    public void RegisterFloat(string eventName, Action listener)
    {
        //if (eventName == "move") OnPlayerMoved += listener;
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
                Action<Vector3, Hittable> handlerTakeDamage = (handlerWhere, handlerHittable) => listener(new EventContext { where = handlerWhere, hittable = handlerHittable });
                OnDamageTaken += handlerTakeDamage;
                break;
            case "on-kill":
                Action<Vector3, GameObject> handlerOnKill = (handlerWhere, handlerKill) => listener(new EventContext { where = handlerWhere, source = handlerKill });
                OnKill += handlerOnKill;
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
                    OnDamageTaken -= (Action<Vector3, Hittable>)wrapper;
                    break;
                case "on-kill":
                    OnKill -= (Action<Vector3, GameObject>)wrapper;
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
    public SpellCaster owner;
    public Hittable hittable;
    public Damage damage;
}

// helps with creating connections dynamically without needing to worry about anything else
public struct EventConnection
{
    public Action<Action<EventContext>> Subscribe;
    public Action<Action<EventContext>> Unsubscribe;

    public EventConnection(Action<Action<EventContext>> subscribe, Action<Action<EventContext>> unsubscribe)
    {
        Subscribe = subscribe;
        Unsubscribe = unsubscribe;
    }
}

