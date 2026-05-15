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

    [JsonConstructor]
    public ClassData(string initName, int initSprite, string initHealth, string initMana, string initManaRegen, string initSpellPower, string initSpeed)
    {

        name = initName;
        sprite = initSprite;
        health = initHealth;
        mana = initMana;
        mana_regeneration = initManaRegen;
        spellpower = initSpellPower;
        speed = initSpeed;

    }

}



// call this class to retrieve the dictionary with all the enemy data
public class RetrieveClassData
{

    private static ClassParsing ClassParse = new ClassParsing();
    private static Dictionary<string, ClassData> classdictionary = new Dictionary<string, ClassData>();

    // Programmer can now call Dictionary<string, EnemyData> enemy_dictionary = RetrieveEnemyData.EnemyDictionary();
    public static Dictionary<string, ClassData> EnemyDictionary()
    {

        classdictionary = ClassParse.StoreData(Resources.Load<TextAsset>("classes").text);
        return classdictionary;

    }

}
