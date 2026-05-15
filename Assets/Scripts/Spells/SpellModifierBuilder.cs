using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

public class SpellModifierBuilder
{
    private ValueModifier valueMod = new ValueModifier();

    // Modifier
    public SpellModifierBuilder WithDamageAdder(string amount) { valueMod.AddValue(AddInt(amount), "amount"); return this; }

    //Changed MulInt to MulFloat since multipliers should be floats. This was causing a Key Dictionary error with the RPN Eval
    public SpellModifierBuilder WithDamageMultiplier(string amount) { valueMod.AddValue(MulFloat(amount), "amount"); return this; }
    public SpellModifierBuilder WithSpeedAdder(string speed) { valueMod.AddValue(AddFloat(speed), "speed"); return this; }
    public SpellModifierBuilder WithSpeedMultiplier(string speed) { valueMod.AddValue(MulFloat(speed), "speed"); return this; }
    public SpellModifierBuilder WithManaAdder(string mana) { valueMod.AddValue(AddFloat(mana), "mana"); return this; }
    public SpellModifierBuilder WithManaMultiplier(string mana) { valueMod.AddValue(MulFloat(mana), "mana"); return this; }
    public SpellModifierBuilder WithCooldownAdder(string cooldown) { valueMod.AddValue(MulFloat(cooldown), "cooldown"); return this; }
    public SpellModifierBuilder WithCooldownMultiplier(string cooldown) { valueMod.AddValue(MulFloat(cooldown), "cooldown"); return this; }

    public ValueModifier Build()
    {
        return valueMod;
    }





    // Acts as an interface and class call
    public SpellModifierBuilder(ValueModifier existingValueModifier)
    {
        this.valueMod = existingValueModifier;
    }



    // returns what type of modifier should be made (mul or add)
    public static string modify(string valueName)
    {
        string suffix = valueName.Split('_')[^1];

        if (suffix == "adder") return "adder";
        if (suffix == "multiplier") return "multiplier";

        // valueMod.AddValue(AddInt(valueExpression, valueName), valueName)

        //ValueModifier<int> damageMod = new ValueModifier<int>();
        //Multiplier<MathOperationsInt, int> healthMod = new Multiplier<MathOperationsInt, int>(potato);
        return null;
    }

    public Adder<MathOperationsInt, int> AddInt(string valueExpression)
    {
        return new Adder<MathOperationsInt, int>(RPNEvaluator.RPNEvaluator.Evaluate(valueExpression, GameManager.Instance.variables));
    }
    public Adder<MathOperationsFloat, float> AddFloat(string valueExpression)
    {
        return new Adder<MathOperationsFloat, float>(RPNEvaluator.RPNEvaluator.Evaluatef(valueExpression, GameManager.Instance.variables));
    }

    public Multiplier<MathOperationsInt, int> MulInt(string valueExpression)
    {
        return new Multiplier<MathOperationsInt, int>(RPNEvaluator.RPNEvaluator.Evaluate(valueExpression, GameManager.Instance.variables));
    }
    public Multiplier<MathOperationsFloat, float> MulFloat(string valueExpression)
    {
        return new Multiplier<MathOperationsFloat, float>(RPNEvaluator.RPNEvaluator.Evaluatef(valueExpression, GameManager.Instance.variables));
    }


    public ValueModifier SpellModifierQuickBuilder(SpellData data)
    {
        ProcessFloat(data.damage_adder, "amount", "adder");
        ProcessFloat(data.damage_multiplier, "amount", "multiplier");
        ProcessFloat(data.mana_adder, "manaCost", "adder");
        ProcessFloat(data.mana_multiplier, "manaCost", "multiplier");

        ProcessFloat(data.speed_adder, "speed", "adder");
        ProcessFloat(data.speed_multiplier, "speed", "multiplier");
        ProcessFloat(data.cooldown_adder, "cooldown", "adder");
        ProcessFloat(data.cooldown_multiplier, "cooldown", "multiplier");

        valueMod.AddValue(data.projectile_trajectory, "projectile_trajectory");

        valueMod.AddValue(data.name, "name");

        if (data.heal != null) valueMod.AddValue(AddFloat(data.heal), "heal");
        if (data.N != null) valueMod.AddValue(AddInt(data.N), "number");
        if (data.repeat != null) valueMod.AddValue(AddInt(data.repeat), "repeat");
        if (data.pierce != null) valueMod.AddValue(AddInt(data.pierce), "pierce");
        if (data.bounce != null) valueMod.AddValue(AddInt(data.bounce), "bounce");
        if (data.delay != null) valueMod.AddValue(AddFloat(data.delay), "delay");
        if (data.angle != null) valueMod.AddValue(AddInt(data.angle), "angle");

        return valueMod;
    }

    private void ProcessInt(string rpn, string statName, string type)
    {
        if (string.IsNullOrEmpty(rpn)) return;

        if (type == "adder")
            valueMod.AddValue(AddInt(rpn), statName);
        else
            valueMod.AddValue(MulInt(rpn), statName);
    }

    private void ProcessFloat(string rpn, string statName, string type)
    {
        if (string.IsNullOrEmpty(rpn)) return;

        if (type == "adder")
            valueMod.AddValue(AddFloat(rpn), statName);
        else
            valueMod.AddValue(MulFloat(rpn), statName);
    }
}
