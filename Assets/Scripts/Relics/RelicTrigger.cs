using System;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class RelicTrigger : MonoBehaviour
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
    protected Action action { get; set; } = null;
    // effect given
    protected RelicEffect effect { get; set; } = null;
   


    public RelicTrigger(Action trigger, RelicEffect effect)
    {
        action = trigger;
        this.action += ConditionCheck; // if possible, put effect as parameter?
        this.effect = effect;
    }

    /*public void DoAction()
    {
        ConditionCheck();
    }*/

    protected virtual void ConditionCheck()
    {
        OnAction();
    }

    protected virtual void OnAction()
    {
        // base action (helpful for events, reset timers, etc)
    }

    public void OnDestroy() 
    {
        action -= ConditionCheck;
    }

    // method to find dedicated trigger based on type given
}
