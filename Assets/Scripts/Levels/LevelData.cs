using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class LevelData
{

    public string name { get ; set ;} = null;
    public int waves { get ; set ;} = -1;
    public List<SpawnData> spawns { get ; set ;} = new List<SpawnData>(); //should make a list of with all the different enemy spawn types

    /*[JsonConstructor]
    public LevelData(string initName, int initWaves, List<SpawnData> initSpawns)
    {
        name = initName;
        waves = initWaves;
        spawns = initSpawns;
    }*/

}



// call this class to retrieve the dictionary with all the level data
public class RetrieveLevelData
{
    
    private static LevelParsing LevelParse = new LevelParsing();
    private static Dictionary<string, LevelData> leveldictionary = new Dictionary<string, LevelData>();

    // Programmer can now call Dictionary<string, LevelData> level_dictionary = RetrieveLevelData.LevelDictionary();
    public static Dictionary<string, LevelData> LevelDictionary()
    {
        
        // Loading levels.json
        leveldictionary = LevelParse.StoreData(Resources.Load<TextAsset>("levels").text);
        return leveldictionary;

    }

}