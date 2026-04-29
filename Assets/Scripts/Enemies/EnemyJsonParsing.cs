using Newtonsoft.Json;
using System.Collections.Generic;

public class EnemyParsing : JsonProcessingTemplate<EnemyData>
{
    
    protected override EnemyData ParseJsonFile(string data)
    {

        //Referencing the assignment instructions on the professor's recommendation.
        return JsonConvert.DeserializeObject<EnemyData>(data); //should deserialize the data from the json file and store it into the EnemyData class

    }

    private void AddEnemyData(Dictionary<string, EnemyData> dictionaryReference)
    {
        EnemyParsing enemy = new EnemyParsing();

        EnemyData newEnemy = enemy.StoreData("enemies.json");
        dictionaryReference.Add(newEnemy.name, newEnemy);
    }

}
