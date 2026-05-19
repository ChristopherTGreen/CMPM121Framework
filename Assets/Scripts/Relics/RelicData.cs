// Note: File created by Jay for assignment 4 - Not provided by the professor
// Professor also recommends splitting the relic triggers and effects separated

using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;



public class RelicData
{
    public string name { get; set; } = null;
    public int sprite { get; set; } = -1;
    public Trigger trigger { get; set; } = null;
    public Effect effect { get; set; } = null;

}



// Type Trigger - used in the RelicTriggersData class
public class Trigger
{
    
    public string description { get; set; } = null;
    public string type { get; set; } = null;
    public string amount { get; set; } = null;

}



public class Effect
{
    
    public string description { get; set; } = null;
    public string type { get; set; } = null;
    public string amount { get; set; } = null;
    public string until { get; set; } = null;

}



// rework this to return separated trigger and event data
public class RetrieveRelicData
{
    
    private static RelicParsing RelicParse = new RelicParsing();
    private static Dictionary<string, RelicData> relicdictionary = new Dictionary<string, RelicData>();

    // Programmer can now call Dictionary<string, LevelData> level_dictionary = RetrieveLevelData.LevelDictionary();
    public static Dictionary<string, RelicData> RelicsDictionary()
    {
        
        // Loading levels.json
        relicdictionary = RelicParse.StoreData(Resources.Load<TextAsset>("relics").text);
        return relicdictionary;

    }

}