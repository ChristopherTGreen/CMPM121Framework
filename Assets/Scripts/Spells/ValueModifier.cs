using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;


// may need to make this an "interface" and not a "class" - chris
public class ValueModifier
{

    // Modifier storage:
    // strict values
    // These lists track the value modifiers applied to the base spell.
    public List<ValueModifier<float>> amount = new List<ValueModifier<float>>();
    public List<ValueModifier<int>> heal = new List<ValueModifier<int>>();
    public List<ValueModifier<float>> speed = new List<ValueModifier<float>>();
    public List<ValueModifier<int>> number = new List<ValueModifier<int>>();
    public List<ValueModifier<float>> manaCost = new List<ValueModifier<float>>();
    public List<ValueModifier<float>> cooldown = new List<ValueModifier<float>>();
    public List<ValueModifier<float>> angle = new List<ValueModifier<float>>();
    public List<ValueModifier<float>> delay = new List<ValueModifier<float>>();
    public List<ValueModifier<float>> lifetime = new List<ValueModifier<float>>();


    // strings, may not need?
    public List<string> type = new List<string>();
    public List<string> trajectory = new List<string>();

        //var mod5 = ValueModifier<int>.GetValue(newList, potato);
        //ValueModifier<int> damageMod = new ValueModifier<int>();
        //Multiplier<MathOperationsInt, int> healthMod = new Multiplier<MathOperationsInt, int>(potato);
        //AddValue<int>("damage", healthMod);

    // Parameters: Takes a value modifier list that is a integer (such as "heal") and a string that is the name of the modifier type
    // Call this when you want to store a list of integer value modifiers to a storage list defined at the top of this class
    // If there are already stored value modifiers, then this method adds the new value modifiers to the existing list of value modifiers
    public void AddList(List<ValueModifier<int>> valueMod, string valueName)
    {
        if (valueName == "amount") amount.AddRange(valueMod);
        else if (valueName == "heal") heal.AddRange(valueMod);
        else if (valueName == "number") number.AddRange(valueMod);
        else if (valueName == "manaCost") manaCost.AddRange(valueMod);
        else throw new Exception("Invalid value modifier int name for add list");
    }

    // Same as notes above but for floats
    public void AddList(List<ValueModifier<float>> valueMod, string valueName)
    {
        if (valueName == "speed") speed.AddRange(valueMod);
        else if (valueName == "cooldown") cooldown.AddRange(valueMod);
        else if (valueName == "angle") angle.AddRange(valueMod);
        else if (valueName == "delay") delay.AddRange(valueMod);
        else if (valueName == "lifetime") lifetime.AddRange(valueMod);
        else throw new Exception("Invalid value modifier float name for add list");
    }
    public void AddList(List<string> valueMod, string valueName)
    {
        if (valueName == "type") type.AddRange(valueMod);
        else if (valueName == "trajectory") type.AddRange(valueMod);
        else throw new Exception("Invalid value modifier string name for add list");
    }

    // This method adds a ValueModifier int to a existing storage class defined at the top of this class.
    // Do not call this for adding a list of modifiers to an existing list of modifiers, if you want to do this, call the above two methods
    public void AddValue(ValueModifier<int> valueMod, string valueName)
    {
        //if (valueName == "amount") amount.Add(valueMod);
        //else if (valueName == "heal") heal.Add(valueMod);
        if (valueName == "heal") heal.Add(valueMod);
        else if (valueName == "number") number.Add(valueMod);
        //else if (valueName == "manaCost") manaCost.Add(valueMod);
        else throw new Exception("Invalid value modifier int name for add value");
    }

    // Same as method above but for floats
    public void AddValue(ValueModifier<float> valueMod, string valueName)
    {
        if (valueName == "speed") speed.Add(valueMod);
        else if (valueName == "cooldown") cooldown.Add(valueMod);
        else if (valueName == "angle") angle.Add(valueMod);
        else if (valueName == "delay") delay.Add(valueMod);
        else if (valueName == "lifetime") lifetime.Add(valueMod);

        else if (valueName == "amount") amount.Add(valueMod);
        else if (valueName == "manaCost") manaCost.Add(valueMod);

        else throw new Exception("Invalid value modifier float name: " + valueName + " for add value");
    }

    public void AddValue(string valueMod, string valueName)
    {
        if (valueName == "type") type.Add(valueMod);
        else if (valueName == "trajectory") type.Add(valueMod);
        else throw new Exception("Invalid value modifier int name: " + valueName + " for add value");
    }

    // Parameters: A list of integer value modifiers, original is the basevalue of a spell.
    public static int GetValue(List<ValueModifier<int>> valueMod, int original)
    {
        return ValueModifier<int>.GetValue(valueMod, original);
    }

    //
    public static float GetValue(List<ValueModifier<float>> valueMod, float original)
    {
        return ValueModifier<float>.GetValue(valueMod, original);
    }
    public static string GetValue(List<string> valueMod, string original)
    {
        if (valueMod == null || valueMod.Count == 0) return original;
        return valueMod[valueMod.Count - 1];
    }


    // Merge function for all possible lists currently
    public void MergeFrom(ValueModifier other)
    {
        if (other == null) return;

        // collects all public fields in value modifier
        FieldInfo[] fields = typeof(ValueModifier).GetFields(BindingFlags.Public | BindingFlags.Instance);

        // goes through each field
        foreach (FieldInfo field in fields)
        {
            // Cehecks if list)
            if (field.FieldType.IsGenericType && field.FieldType.GetGenericTypeDefinition() == typeof(List<>))
            {
                var sourceList = field.GetValue(other);
                if (sourceList == null) continue; // source list doesn't exist

                var targetList = field.GetValue(this);

                // does target list exist
                if (targetList == null)
                {
                    // Create the list if it doesn't exist yet 
                    targetList = Activator.CreateInstance(field.FieldType);
                    field.SetValue(this, targetList);
                }


                Debug.Log("Successful merge");

                MethodInfo addRangeMethod = targetList.GetType().GetMethod("AddRange");
                addRangeMethod.Invoke(targetList, new[] { sourceList });
            }
        }
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
