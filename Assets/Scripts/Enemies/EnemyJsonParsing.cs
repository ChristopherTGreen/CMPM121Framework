using Newtonsoft.Json;
using System.Collections.Generic;

public class EnemyParsing : JsonProcessingTemplate<EnemyData>
{
    
    protected override EnemyData ParseData(string jsonfile)
    {
        
    }

    private void AddEnemyData(Dictionary<string, EnemyData> dictionaryReference)
    {
        EnemyParsing enemy = new EnemyParsing();

        EnemyData newEnemy = enemy.StoreData("enemies.json");
        dictionaryReference.Add(newEnemy.name, newEnemy);
    }

}
