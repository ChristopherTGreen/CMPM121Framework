using System;
using System.Collections.Generic;
using UnityEngine;

// Enemy
// Stores the information or stats of each enemy, each in a dictionary it references to
[Serializable]
public class EnemyData
{
    public string name;
    // Making sure, do we need sprite name or index (i'm going with index for memory reasons) (clarify me if I am wrong) - Chris
    public int sprite;
    public int hp;
    public int speed;
    public int damage;
    
    // surely there must be a better way to store this information with keeping encapsulation?


}
