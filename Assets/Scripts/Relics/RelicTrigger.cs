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

    // Connect the relic effect or condition to this to tell it to call its action
    public event Action<EventContext> OnTrigger;

    // Given name trigger for event bus
    public string triggerMain;
    public RelicTrigger(string eventName, string amountToCheck)
    {
        this.triggerMain = eventName;
        this.amountToCheck = amountToCheck;
        EventBus.Instance.Register(eventName, Check);
    }

    /*public void DoAction()
    {
        ConditionCheck();
    }*/
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

    protected virtual void OnAction()
    {
        // base action (helpful for any events which must be executed at the end of the full operation)
    }
    // method to find dedicated trigger based on type given

    public void Unequip()
    {
        // used to disconnect observers
       
    }
}
