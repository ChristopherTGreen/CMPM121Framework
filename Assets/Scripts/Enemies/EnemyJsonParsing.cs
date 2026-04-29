using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Diagnostics;

public class EnemyParsing : JsonProcessingTemplate<EnemyData>
{

    // The dictionary parameter is provided in the StoreData method.
    // To give the json file, you need to use Resources.Load<TextAsset>("enemies").text (method from Newtonsoft.Json;) as a parameter.
    protected override void ParseData(string jsonfile, Dictionary<string, EnemyData> dictionary)
    {
        
        JToken jsonObject = JToken.Parse(jsonfile); //gets all of the objects from the Json file

        //Parses each object and "uploads" them to the dictionary with the enemy name as the key and the EnemyData type as the value
        foreach (var enemy in jsonObject)
        {
            EnemyData enemyData = enemy.ToObject<EnemyData>(); 
            dictionary[enemyData.name] = enemyData;
        }

    }

}
