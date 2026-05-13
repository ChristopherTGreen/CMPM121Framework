using System;
using System.Collections.Generic;
using System.Text;

public class ValueStatContainer
{

    // store a dictionary containing all current modifiers being used
    // Object would store the methods ValueAdd(int or float), ValueMul(int or float), string would be the modifier name
    /*private Dictionary<string, object> allCurrentStats = new Dictionary<string, object>
    {
        { "amount", amount }
    };*/
    // strict values
    public List<ValueModifier<int>> amount = new List<ValueModifier<int>>();
    public List<ValueModifier<int>> heal = new List<ValueModifier<int>>();
    public List<ValueModifier<float>> speed;
    public List<ValueModifier<int>> number = new List<ValueModifier<int>>();
    public List<ValueModifier<int>> manaCost = new List<ValueModifier<int>>();
    public List<ValueModifier<float>> cooldown = new List<ValueModifier<float>>();
    public List<ValueModifier<float>> angle = new List<ValueModifier<float>>();
    public List<ValueModifier<float>> delay = new List<ValueModifier<float>>();
    public List<ValueModifier<float>> lifetime = new List<ValueModifier<float>>();
    

    // strings, may not need?
    public List<string> type;
    public List<string> trajectory;



    public List<ValueModifier<T>> GetList<T>(string valueName)
    {
        if (!allCurrentStats.ContainsKey(valueName))
        {
            throw new Exception("Invalid value modification; value does not exist");
        }

        return (List<ValueModifier<T>>)allCurrentStats[valueName];
    }



    // asuume valueMod is a list with respective type
    public void AddValue<T>(string valueName, object valueMod)
    {
        if (!allCurrentStats.ContainsKey(valueName))
        {
            List<ValueModifier<T>> newList = new List<ValueModifier<T>>();
            allCurrentStats[valueName] = newList;
        }
        allCurrentStats[valueName] = valueMod;



        //var mod5 = ValueModifier<int>.GetValue(newList, potato);
        //ValueModifier<int> damageMod = new ValueModifier<int>();
        //Multiplier<MathOperationsInt, int> healthMod = new Multiplier<MathOperationsInt, int>(potato);
        //AddValue<int>("damage", healthMod);
    }

    // combine given lists
    



    // I know we want a dictionary but I'm trying a list first
    //public static List<ValueModifier> DamageModifier = new List<ValueModifier>();
}
