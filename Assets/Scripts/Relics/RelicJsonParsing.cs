// Note: File created by Jay for assignment 4 - Not provided by the professor
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;


// Might need to rework this. I'm pretty sure we're going to have a parsing error since the Newtonsoft library 
// will throw a error if it can't find enough variables to store each property - the splitting of the triggers
// and effects makes this a bit difficult to make.


// parses the relic triggers data
public class RelicParsing : JsonProcessingTemplate<RelicData>
{

    protected override void ParseData(string jsonfile, Dictionary<string, RelicData> dictionary)
    {

        JToken jsonObject = JToken.Parse(jsonfile); //    gets all of the objects from the Json file
        
        foreach (var relicType in jsonObject)
        {

            RelicData relicData = relicType.ToObject<RelicData>();
            dictionary[relicData.name] = relicData;

        }

    }

}