using Newtonsoft.Json.Linq;

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
}
