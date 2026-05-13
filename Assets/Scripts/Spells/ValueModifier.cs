using System;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;


// may need to make this an "interface" and not a "class" - chris
public class ValueModifier
{

    // Call Spell Container class...
    // strict values
    public List<ValueModifier<int>> amount = new List<ValueModifier<int>>();
    public List<ValueModifier<int>> heal = new List<ValueModifier<int>>();
    public List<ValueModifier<float>> speed = new List<ValueModifier<float>>();
    public List<ValueModifier<int>> number = new List<ValueModifier<int>>();
    public List<ValueModifier<int>> manaCost = new List<ValueModifier<int>>();
    public List<ValueModifier<float>> cooldown = new List<ValueModifier<float>>();
    public List<ValueModifier<float>> angle = new List<ValueModifier<float>>();
    public List<ValueModifier<float>> delay = new List<ValueModifier<float>>();
    public List<ValueModifier<float>> lifetime = new List<ValueModifier<float>>();


    // strings, may not need?
    public List<string> type;
    public List<string> trajectory;

        //var mod5 = ValueModifier<int>.GetValue(newList, potato);
        //ValueModifier<int> damageMod = new ValueModifier<int>();
        //Multiplier<MathOperationsInt, int> healthMod = new Multiplier<MathOperationsInt, int>(potato);
        //AddValue<int>("damage", healthMod);

    public void AddList(List<ValueModifier<int>> valueMod, string valueName)
    {
        if (valueName == "amount") amount.AddRange(valueMod);
        else if (valueName == "heal") heal.AddRange(valueMod);
        else if (valueName == "number") number.AddRange(valueMod);
        else if (valueName == "manaCost") manaCost.AddRange(valueMod);
        throw new Exception("Invalid value modifier int name for add list");
    }
    public void AddList(List<ValueModifier<float>> valueMod, string valueName)
    {
        if (valueName == "speed") speed.AddRange(valueMod);
        else if (valueName == "cooldown") cooldown.AddRange(valueMod);
        else if (valueName == "angle") angle.AddRange(valueMod);
        else if (valueName == "delay") delay.AddRange(valueMod);
        else if (valueName == "lifetime") lifetime.AddRange(valueMod);
        throw new Exception("Invalid value modifier float name for add list");
    }
    public void AddValue(ValueModifier<int> valueMod, string valueName)
    {
        if (valueName == "amount") amount.Add(valueMod);
        else if (valueName == "heal") heal.Add(valueMod);
        else if (valueName == "number") number.Add(valueMod);
        else if (valueName == "manaCost") manaCost.Add(valueMod);
        else throw new Exception("Invalid value modifier int name for add value");
    }
    public void AddValue(ValueModifier<float> valueMod, string valueName)
    {
        if (valueName == "speed") speed.Add(valueMod);
        else if (valueName == "cooldown") cooldown.Add(valueMod);
        else if (valueName == "angle") angle.Add(valueMod);
        else if (valueName == "delay") delay.Add(valueMod);
        else if (valueName == "lifetime") lifetime.Add(valueMod);
        else throw new Exception("Invalid value modifier float name for add value");
    }

    public static int GetValue(List<ValueModifier<int>> valueMod, int original)
    {
        return ValueModifier<int>.GetValue(valueMod, original);
    }
    public static float GetValue(List<ValueModifier<float>> valueMod, float original)
    {
        return ValueModifier<float>.GetValue(valueMod, original);
    }


}

// Child of the ValueModifier class above
public class ValueModifier<T> 
{

    // Editable method
    // Original should be base value
    public virtual T GetValue(T original)
    {
        return original;
    }

    // static version of GetValue, takes a list of value modifiers [Add(10), Mul(10)], original seems to be the base value
    public static T GetValue(List<ValueModifier<T>> mods, T original)
    {

        // Modifies specific value (base value of a spell) with current list of mods
        T result = original;

        foreach (ValueModifier<T> mod in mods)
        {
            // Calculating the addition and/or multiplication of the base value?
            result = mod.GetValue(result); // this GetValue is a edited version of ValueModifier<T>.GetValue. Edited below in Adder and Multiplier
        }

        return result;
    }
}


// Math operations interface
public interface MathOperations<T>
{
    T Add(T a, T b);
    T Mul(T a, T b);
    // and other operations
}


// The int child of the interface
public class MathOperationsInt : MathOperations<int>
{
    public int Add(int a, int b)
    {
        return a + b;
    }
    public int Mul(int a, int b)
    {
        return a * b;
    }
}


// The float child of the interface
// Jay added this after asking the professor
public class MathOperationsFloat : MathOperations<float>
{
    public float Add(float a, float b)
    {
        return a + b;
    }
    public float Mul(float a, float b)
    {
        return a * b;
    }
}


// Adder is a child of the ValueModifier<T> class and Ops is a child of the interface.
// new just means that new Ops().Add(original, add) is possible in the return statement below
// if Ops was filled in with MathOperationsInt, then the return statement is "return new MathOperationsInt().Add(original, add)"
public class Adder<Ops, T> : ValueModifier<T> where Ops : MathOperations<T>, new()
{

    public T add;

    // Constructor? I think
    public Adder(T add)
    {
        this.add = add;
    }

    // edited version of ValueModifier<T>'s GetValue
    public override T GetValue(T original)
    {
        return new Ops().Add(original, add);
    }

}


// Multiplier version
public class Multiplier<Ops, T> : ValueModifier<T> where Ops : MathOperations<T>, new()
{
    public T mul;
    public Multiplier(T mul)
    {
        this.mul = mul;
    }
    public override T GetValue(T original)
    {
        return new Ops().Mul(original, mul);
    }
}
