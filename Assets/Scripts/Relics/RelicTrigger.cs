using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class RelicTrigger
{
    public string amountToCheck { get; set; } = null;

    // Connect the relic effect or condition to this to tell it to call its action
    public event Action OnTrigger;

    // Given trigger from eventbus
    public event Action triggerMain;
    public RelicTrigger(Action trigger, string amountToCheck)
    {
        triggerMain = trigger;
        this.amountToCheck = amountToCheck;
        triggerMain += Check;
    }

    /*public void DoAction()
    {
        ConditionCheck();
    }*/
    public void Check()
    {
        if (TriggerCheck(amountToCheck))
        {
            // 2. If true, shout it out! 
            OnTrigger?.Invoke();
            //return true;
        }
        //return false;
    }

    protected virtual bool TriggerCheck(string amountToCheck)
    {
        return true;
    }

    protected virtual void OnAction()
    {
        // base action (helpful for any events which must be executed at the end of the full operation)
    }
    // method to find dedicated trigger based on type given
}
