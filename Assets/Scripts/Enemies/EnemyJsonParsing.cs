using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Newtonsoft.Json.Linq;

public class EnemyParsing : JsonProcessingTemplate<EnemyData>
{
    
    protected override EnemyData ParseJsonFile(string data)
    {

        //Referencing the assignment instructions on the professor's recommendation.
        return JsonConvert.DeserializeObject<EnemyData>(data); //should deserialize the data from the json file and store it into the EnemyData class

    }

}
