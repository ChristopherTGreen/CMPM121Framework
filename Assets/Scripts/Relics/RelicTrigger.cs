using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class RelicTrigger
{
    // name of the relic event
    protected string eventName { get; set; } = null;
    // name of the property to be applied to
    protected string eventPropertyName { get; set; } = null;
    
    // amount applied
    protected string amountToApply { get; set; } = null;
    // Default is off
    public int time { get; set; } = -1;
    // general event for all triggers (probably useless)
    protected Action trigger { get; set; } = null;

    public RelicTrigger(Action trigger, RelicEffect effect)
    {
        
    }

    /*public void DoAction()
    {
        ConditionCheck();
    }*/
    public bool TestCheck(string amountToCheck)
    {
        return ConditionCheck(amountToCheck);
    }

    protected virtual bool ConditionCheck(string amountToCheck)
    {
        return true;
    }

    protected virtual void OnAction()
    {
        // base action (helpful for any events which must be executed at the end of the full operation)
    }
    // method to find dedicated trigger based on type given
}
