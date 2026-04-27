namespace JsonDataClasses{

    public class EnemyBaseData
    {
        public string EnemyName { get ; set; }
        public int EnemySpriteIndex { get ; set; }
        public int EnemyBaseHealth { get ; set; }
        public int EnemyBaseSpeed { get ; set; }
        public int EnemyBaseDamage { get ; set; }
    }

    public class SpawnsBaseData
    {
        public string EnemyType { get ; set; }
        public string EnemyAmount { get ; set; }
        public string EnemyHp { get ; set; }
        public string SpawnDelay { get ; set; }
        public List<int> EnemySpawnSequence { get ; set; }
        public string EnemySpawnLocation { get ; set; }
    }

    public class LevelsBaseData
    {
        public string LevelDifficulty { get ; set ;}
        public int AmountOfWaves { get ; set; }
        public List<SpawnsBaseData> EnemySpawns { get ; set ;} = new List<SpawnsBaseData>(); //should make a list of with all the different enemy spawn types
    }

}