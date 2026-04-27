using Newtonsoft.Json.Linq;
using System.Dynamic;
using System.IO;

namespace JsonProcessing 
{
    //might need to move this enemy class to a different file
    public class EnemyBaseData
    {
        public string EnemyName { get ; set; }
        public int EnemySpriteIndex { get ; set; }
        public int EnemyBaseHealth { get ; set; }
        public int EnemyBaseSpeed { get ; set; }
        public int EnemyBaseDamage { get ; set; }
    }

    //also might need to move this to a different file?
    public class SpawnsBaseData
    {
        public string EnemyType { get ; set; }
        public string EnemyAmount { get ; set; }
        public string EnemyHp { get ; set; }
        public string SpawnDelay { get ; set; }
        public Array<int> EnemySpawnSequence { get ; set; }
        public string EnemySpawnLocation { get ; set; }
    }

    public class LevelsBaseData
    {
        public string LevelDifficulty { get ; set ;}
        public int AmountOfWaves { get ; set; }
        public List<SpawnsBaseData> EnemySpawns; //should make a list of with all the different enemy spawn types
    }



    //Template Method Pattern?
    public abstract class GeneralJsonParsing<Type>
    {
        //Definitely not doing this right. Will continue in the morning
        GetJsonFile();
        ReadJsonFile();
        ParseJsonFile();
        CloseJsonFile();
    };



    //for testing purposes
    void Start()
    {
        
    }

}
