using System;
using System.Collections.Generic;
using System.Text;

public class SpellStatContainer
{
    /*
    // store a dictionary containing all current modifiers being used
    private Dictionary<string, object> allCurrentMods = new Dictionary<string, object>();


    public List<ValueModifier<T>> GetList<T>(string valueName)
    {
        if (!allCurrentMods.ContainsKey(valueName))
        {
            allCurrentMods[valueName] = new List<ValueModifier<T>>();
            // we need some way to check properties
        }
        else
        {
            throw new Exception("Invalid value modification; value does not exist");
        }

        return (List<ValueModifier<T>>)allCurrentMods[valueName];
    }
    */

    // I know we want a dictionary but I'm trying a list first
    public static List<ValueModifier> DamageModifier = new List<ValueModifier>();
}