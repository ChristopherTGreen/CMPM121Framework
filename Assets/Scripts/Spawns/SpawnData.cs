using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;

public class SpawnData
{

    public string enemy { get ; set; } = null;
    public string count { get ; set; } = null;
    public string hp { get ; set; } = null;
    public string delay { get ; set; } = "2";
    public List<int> sequence { get ; set; } = new List<int> { 1 };
    public string location { get; set; } = null;

    /*[JsonConstructor]
    public SpawnData(string initEnemy, string initCount, string initHp, string initDelay, List<int> initSequence, string initLocation)
    {
        
        enemy = initEnemy;
        count = initCount;
        hp = initHp;
        delay = initDelay;
        sequence = initSequence;
        location = initLocation;

    }*/

}