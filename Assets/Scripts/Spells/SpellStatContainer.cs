using System;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;

public class SpellStatContainer
{
    
    // store a dictionary containing all current modifiers being used
    // Object would store the methods ValueAdd(int or float), ValueMul(int or float), string would be the modifier name
    private Dictionary<string, object> allCurrentMods = new Dictionary<string, object>();

    public List<ValueModifier<T>> GetList<T>(string valueName)
    {
        if (!allCurrentMods.ContainsKey(valueName))
        {
            throw new Exception("Invalid value modification; value does not exist");
        }

        return (List<ValueModifier<T>>)allCurrentMods[valueName];
    }



    // asuume valueMod is a list with respective type
    public void AddValue<T>(string valueName, object valueMod)
    {
        if (!allCurrentMods.ContainsKey(valueName))
        {
            List<ValueModifier<T>> newList = new List<ValueModifier<T>>();
            allCurrentMods[valueName] = newList;
        }
        //allCurrentMods[valueName]



        //var mod5 = ValueModifier<int>.GetValue(newList, potato);
        //ValueModifier<int> damageMod = new ValueModifier<int>();
        //Multiplier<MathOperationsInt, int> healthMod = new Multiplier<MathOperationsInt, int>(potato);
        //AddValue<int>("damage", healthMod);
    }
        
    

    // I know we want a dictionary but I'm trying a list first
    //public static List<ValueModifier> DamageModifier = new List<ValueModifier>();
}