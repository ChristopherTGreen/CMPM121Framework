using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class GainHp : RelicEffect
{
    protected override void ApplyEffect(EventContext context)
    {
        //Debug.Log("Apply hP");
        if (string.IsNullOrEmpty(amount))
        {
            Debug.LogError("Amount null or never provided: " + amount); //if amount is null the rpn will fail.
            return; 
        }
        
        int hpGain = RPNEvaluator.RPNEvaluator.Evaluate(amount, GameManager.Instance.variables);
        //Debug.Log(hpGain);
        //Debug.Log(context.hittable.hp);
        if (context.hittable != null) context.hittable.SetCurrentHP(hpGain);
        else if (context.player != null) context.player.hp.SetCurrentHP(hpGain);
        else
        {
            throw new Exception("Apply Effect: Gain Hp was given invalid context");
        }
        //Debug.Log(context.hittable.hp);
    }
}