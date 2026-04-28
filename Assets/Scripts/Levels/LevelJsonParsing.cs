using Newtonsoft.Json;
using System.Collections.Generic;

public class LevelParsing : JsonProcessingTemplate<LevelData>
{
    
    protected override LevelData ParseJsonFile(string data)
    {

        //need to deserialize the array in levels.json for spawn.

        //need to also deserialize the name and waves of levels.json

        //then return it.

        //Referencing the assignment instructions on the professor's recommendation.
        return JsonConvert.DeserializeObject<LevelData>(data); //should deserialize the data from the json file and store it into the LevelData class

        //likely need to add something else to parse the spawns in level.json

    }

}