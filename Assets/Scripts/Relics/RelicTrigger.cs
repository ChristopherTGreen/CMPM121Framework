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
    // amount to check
    protected string amountToCheck { get; set; } = null;
    // amount applied
    protected string amountToApply { get; set; } = null;
    // general event for all triggers (probably useless)
    protected Action trigger { get; set; } = null;
    // effect given
    public Delegate operation { get; set; } = null;
   


    public RelicTrigger(Action trigger, RelicEffect effect)
    {
        this.trigger = trigger;
        this.trigger += ConditionCheck; // if possible, put effect as parameter?
    }

    /*public void DoAction()
    {
        ConditionCheck();
    }*/
    public void TestCheck()
    {
        ConditionCheck();
    }

    protected virtual void ConditionCheck()
    {
        OnAction();
    }

    protected virtual void OnAction()
    {
        // base action (helpful for any events which must be executed at the end of the full operation)
    }

    public void OnDestroy() 
    {
        trigger -= ConditionCheck;
    }



    // method to find dedicated trigger based on type given
}
