using System.Collections.Generic;
using Newtonsoft.Json;

public class LevelData
{

    public string name { get ; set ;}
    public int waves { get ; set; }
    public List<SpawnData> spawns { get ; set ;} = new List<SpawnData>(); //should make a list of with all the different enemy spawn types

    [JsonConstructor]
    public LevelData(string initName, int initWaves, List<SpawnData> initSpawns)
    {
        name = initName;
        waves = initWaves;
        spawns = initSpawns;
    }

}