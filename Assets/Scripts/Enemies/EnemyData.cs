using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;

// Enemy
// Stores the information or stats of each enemy, each in a dictionary it references to
[Serializable]
public class EnemyData
{

    public string name { get ; set; }
    public int sprite { get ; set; } //Sprites are indexed - Jay
    public int hp { get ; set; }
    public int speed { get ; set; }
    public int damage { get ; set; }

    [JsonConstructor]
    public EnemyData(string initName, int initSprite, int initHp, int initSpeed, int initDamage)
    {

        name = initName;
        sprite = initSprite;
        hp = initHp;
        speed = initSpeed;
        damage = initDamage;
        
    }

}



// call this class to retrieve the dictionary with all the enemy data
public class StoredEnemyData
{
    
    private static EnemyParsing EnemyParse = new EnemyParsing();
    private static Dictionary<string, EnemyData> enemydictionary = new Dictionary<string, EnemyData>();

    // Programmer can now call Dictionary<string, EnemyData> enemy_dictionary = StoredEnemyData.EnemyDictionary();
    public static Dictionary<string, EnemyData> EnemyDictionary()
    {
        
        enemydictionary = EnemyParse.StoreData(Resources.Load<TextAsset>("enemies").text);
        return enemydictionary;

    }

}