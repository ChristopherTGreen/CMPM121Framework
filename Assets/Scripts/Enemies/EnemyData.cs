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
    // surely there must be a better way to store this information with keeping encapsulation?

}
