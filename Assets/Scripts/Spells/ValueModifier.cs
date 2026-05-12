using System;
using System.Collections.Generic;
using System.Text;

//Jay's trying something else with the ValueModifier
/*
public class ValueModifier
{
     From Chris' attempt below
        public interface MathOperations<T>
    {
        T Add(T a, T b);
        T Mul(T a, T b);
        // and other operations
    }
    public class MathOperationsInt :
            MathOperations<int>
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
    

    
    //trying enums - structs, methods and classes didn't work as I thought
    public enum MathOperations
    {
        AddValue,
        ScalarValue
    }

    public MathOperations mathoperations;
    public float modifieramount;

    public float CalculateModification(float basevalue)
    {
        
        if (mathoperations == MathOperations.AddValue)
        {
    
            return modifieramount + basevalue;

        } 
        else
        {

            return modifieramount * basevalue;

        }

    }


    /* Having issues with methods
    public float AddValue(float basevalue, float modifierinput)
    {
        return basevalue + modifierinput;
    }

    public float ScalarValue(float basevalue, float modifierinput)
    {
        return basevalue * modifierinput;
    }
    

    // Modulus??? Probably not but for scalability... maybe

}
*/

//Chris' ValueModifier


// may need to make this an "interface" and not a "class" - chris
public class ValueModifier
{
    
    // don't know if we need anything in here, but we do need an interface - chris
    
}
public class ValueModifier<T> : ValueModifier
{
    public virtual T GetValue(T original)
    {
        return original;
    }
    public static T GetValue(List<ValueModifier<T>> mods, T original)
    {
        // Modifies specific value with current list of mods
        T result = original;
        foreach (ValueModifier<T> mod in mods)
        {
            result = mod.GetValue(result);
        }

        return result;
    }
}

public interface MathOperations<T>
{
    T Add(T a, T b);
    T Mul(T a, T b);
    // and other operations
}
public class MathOperationsInt :
        MathOperations<int>
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

public class ConstantAdderModifierGeneric<Ops, T> : ValueModifier<T> where Ops : MathOperations<T>, new()
{
    public T add;
    public ConstantAdderModifierGeneric(T add)
    {
        this.add = add;
    }
    public override T GetValue(T original)
    {
        return new Ops().Add(original, add);
    }
}
public class ConstantMultiplierModifierGeneric<Ops, T> :
        ValueModifier<T>
        where Ops : MathOperations<T>, new()
{
    public T mul;
    public ConstantMultiplierModifierGeneric(T mul)
    {
        this.mul = mul;
    }
    public override T GetValue(T original)
    {
        return new Ops().Mul(original, mul);
    }
}
