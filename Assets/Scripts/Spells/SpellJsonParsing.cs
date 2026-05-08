using Newtonsoft.Json.Linq;
using System.Collections.Generic;


public class SpellParsing : JsonProcessingTemplate<SpellData>
{
    
    protected override void ParseData(string jsonfile, Dictionary<string, SpellData> dictionary)
    {

        JToken jsonObject = JToken.Parse(jsonfile); //gets all of the objects from the Json file

        foreach (var level in jsonObject)
        {
            SpellData spellData = level.ToObject<SpellData>();
            dictionary[spellData.name] = spellData;
        }

    }

}
