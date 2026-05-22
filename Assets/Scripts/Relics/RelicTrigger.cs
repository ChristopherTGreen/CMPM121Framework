using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class RelicTrigger
{
    public string amountToCheck { get; set; } = null;
    public string triggerBreak { get; set; } = null;

    // Connect the relic effect or condition to this to tell it to call its action
    public event Action<EventContext> OnTrigger;

    // Given name trigger for event bus
    public string triggerMain { get; set; } = null;
   
    public RelicTrigger(string eventName, string amountToCheck)
    {
        this.triggerMain = eventName;
        this.amountToCheck = amountToCheck;
    }

    public void Check(EventContext context)
    {
        if (TriggerCheck(amountToCheck, context))
        {
            // 2. If true, shout it out! 
            OnTrigger?.Invoke(context);
            //return true;
        }
        //return false;
    }

    protected virtual bool TriggerCheck(string amountToCheck, EventContext context)
    {
        return true;
    }
    
    public void AddObserver()
    {
        EventBus.Instance.Register(triggerMain, Check);
    }
    public void RemoveObserver() 
    {
        EventBus.Instance.Deregister(triggerMain, Check);

    }



}
