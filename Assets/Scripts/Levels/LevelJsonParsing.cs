using Newtonsoft.Json.Linq;
using System.Collections.Generic;


public class LevelParsing : JsonProcessingTemplate<LevelData>
{
    
    protected override void ParseData(string jsonfile, Dictionary<string, LevelData> dictionary)
    {

        JToken jsonObject = JToken.Parse(jsonfile); //gets all of the objects from the Json file

        foreach (var level in jsonObject)
        {
            LevelData levelData = level.ToObject<LevelData>();
            dictionary[levelData.name] = levelData;
        }

    }

}
