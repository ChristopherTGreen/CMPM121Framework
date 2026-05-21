using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class RelicTrigger
{

    public RelicTrigger()
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
