using System;
using System.Collections.Generic;
using System.Text;


// may need to make this an "interface" and not a "class" - chris
public class ValueModifier
{
    
    // Call Spell Container class...
    
}

// Child of the ValueModifier class above
public class ValueModifier<T> : ValueModifier
{

    // EDitable mathod
    // is Original the base value?
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
