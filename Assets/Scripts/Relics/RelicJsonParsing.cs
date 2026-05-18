// Note: File created by Jay for assignment 4 - Not provided by the professor
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;


// Might need to rework this. I'm pretty sure we're going to have a parsing error since the Newtonsoft library 
// will throw a error if it can't find enough variables to store each property - the splitting of the triggers
// and effects makes this a bit difficult to make.


// parses the relic triggers data
public class RelicTriggersParsing : JsonProcessingTemplate<RelicTriggersData>
{

    protected override void ParseData(string jsonfile, Dictionary<string, RelicTriggersData> dictionary)
    {

        JObject jsonObject = JObject.Parse(jsonfile); //gets all of the objects from the Json file
        
        foreach (var relicType in jsonObject.Properties())
        {

            RelicTriggersData relicTriggersData = relicType.Value.ToObject<RelicTriggersData>();
            dictionary[relicTriggersData.name] = relicTriggersData;

        }

    }

}



// Parses the relic events data
public class RelicEventsParsing : JsonProcessingTemplate<RelicEventsData>
{

    protected override void ParseData(string jsonfile, Dictionary<string, RelicEventsData> dictionary)
    {

        JObject jsonObject = JObject.Parse(jsonfile); //gets all of the objects from the Json file
        
        foreach (var relicType in jsonObject.Properties())
        {

            RelicEventsData relicEventsData = relicType.Value.ToObject<RelicEventsData>();
            dictionary[relicEventsData.name] = relicEventsData;

        }

    }

}