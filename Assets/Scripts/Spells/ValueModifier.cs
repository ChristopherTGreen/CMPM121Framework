using System;
using System.Collections.Generic;
using System.Text;

// potentially, we should separate these into their own classes - chris
public class ValueModifier<T>
{
    public virtual T GetValue(T original)
    {
        return original;
    }
}
public class ConstantAdderModifierGeneric<Ops, T> :
        ValueModifier<T>
        where Ops : MathOperations<T>, new()
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

public interface MathOperations<T>
{
    T Add(T a, T b);
    // and other operations
}
public class MathOperationsInt :
        MathOperations<int>
{
    public int Add(int a, int b)
    {
        return a + b;
    }
}