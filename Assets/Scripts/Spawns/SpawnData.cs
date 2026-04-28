using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;

public class SpawnData
{

    public string enemy { get ; set; }
    public string count { get ; set; }
    public string hp { get ; set; }
    public string delay { get ; set; }
    public List<int> sequence { get ; set; }
    public string location { get ; set; }

    [JsonConstructor]
    public SpawnData(string initEnemy, string initCount, string initHp, string initDelay, List<int> initSequence, string initLocation)
    {
        
        enemy = initEnemy;
        count = initCount;
        hp = initHp;
        delay = initDelay;
        sequence = initSequence;
        location = initLocation;

    }

}