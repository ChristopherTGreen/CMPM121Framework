using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

public class RelicInstant : RelicTrigger
{
    // check is the value being reached
    // Note: This can be an int, string, float, this is a pure equality check between the given value, 
    // meaning it naturally has an extra need for condition checks because it needs a reference
    // The reference might be what the current projectile type is, which is not an internally tracking variable
    // SO in short this is an external checker for any values outside, meaning no RPN
    public string check { get; set; }
    public RelicInstant(Action trigger, RelicEffect effect) : base(trigger, effect) { }

    
    protected override bool ConditionCheck()
    {
        if (amountToCheck == check) OnAction();
    }
}