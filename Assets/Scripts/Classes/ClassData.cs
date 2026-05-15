using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;



public class ClassData
{

    public string name { get; set; } = null;
    public int sprite { get; set; } = 0;//Sprites are indexed - Jay
    public string health { get; set; } = null;
    public string mana { get; set; } = null;
    public string mana_regeneration { get; set; } = null;
    public string spellpower { get; set; } = null;
    public string speed { get; set; } = null;

}



// call this class to retrieve the dictionary with all the class data
public class RetrieveClassData
{

    private static ClassParsing ClassParse = new ClassParsing();
    private static Dictionary<string, ClassData> classdictionary = new Dictionary<string, ClassData>();

    // Programmer can now call Dictionary<string, ClassData> enemy_dictionary = RetrieveClassData.ClassDictionary();
    public static Dictionary<string, ClassData> ClassDictionary()
    {

        classdictionary = ClassParse.StoreData(Resources.Load<TextAsset>("classes").text);
        return classdictionary;

    }

}
