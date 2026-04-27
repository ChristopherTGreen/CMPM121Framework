using System.Collections.Generic;

public class LevelData
{

    public string name { get ; set ;}
    public int waves { get ; set; }
    public List<SpawnData> spawns { get ; set ;} = new List<SpawnData>(); //should make a list of with all the different enemy spawn types

}