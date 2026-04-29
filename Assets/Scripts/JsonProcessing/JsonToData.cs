using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

public class JsonToData
{

    public Dictionary<string, EnemyData> enemyTypes; // (possibly convert to lists based on what prof said) - chris
    public List<LevelData> levels;
    //protected override void ParseData(string jsonfile, Dictionary<string, LevelData> dictionary)
    public void LevelAssignment()
    {
        //ParseData(Resources.Load<TextAsset>("enemies").text);
    }

}