using Newtonsoft.Json;
using System.Collections.Generic;

public class SpawnParsing : JsonProcessingTemplate<SpawnData>
{
    
    protected override SpawnData ParseJsonFile(string data)
    {

        //Referencing the assignment instructions on the professor's recommendation.
        return JsonConvert.DeserializeObject<SpawnData>(data); //should deserialize the data from the json file and store it into the SpawnData class

    }

}