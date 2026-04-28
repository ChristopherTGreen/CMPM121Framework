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
