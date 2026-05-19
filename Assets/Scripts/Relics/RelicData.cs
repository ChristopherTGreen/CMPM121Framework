// Note: File created by Jay for assignment 4 - Not provided by the professor
// Professor also recommends splitting the relic triggers and effects separated

using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;



public class RelicTriggersData
{
    public string name { get; set; } = null;
    public int sprite { get; set; } = -1;
    public Trigger trigger { get; set; } = null;

}



public class RelicEventsData
{
    
    public string name { get; set; } = null;
    public int sprite { get; set; } = -1;
    public Effect effect { get; set; } = null;

}



// Type Trigger - used in the RelicTriggersData class
public class Trigger
{
    
    public string description { get; set; } = null;
    public string type { get; set; } = null;
    public string amount { get; set; } = null;

}



// Type Effect - used in the RelicEventsData class
public class Effect
{
    
    public string description { get; set; } = null;
    public string type { get; set; } = null;
    public string amount { get; set; } = null;
    public string until { get; set; } = null;

}



public class RetrieveRelicTriggersData
{
    
    private static RelicTriggersParsing TriggersParse = new RelicTriggersParsing();
    private static Dictionary<string, RelicTriggersData> relictriggersdictionary = new Dictionary<string, RelicTriggersData>();

    // Programmer can now call Dictionary<string, LevelData> level_dictionary = RetrieveLevelData.LevelDictionary();
    public static Dictionary<string, RelicTriggersData> RelicTriggersDictionary()
    {
        
        // Loading levels.json
        relictriggersdictionary = TriggersParse.StoreData(Resources.Load<TextAsset>("relics").text);
        return relictriggersdictionary;

    }

}