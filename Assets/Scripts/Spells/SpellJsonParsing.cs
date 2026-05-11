using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;


public class SpellParsing : JsonProcessingTemplate<SpellData>
{
    
    protected override void ParseData(string jsonfile, Dictionary<string, SpellData> dictionary)
    {

        JObject jsonObject = JObject.Parse(jsonfile); //gets all of the objects from the Json file

        foreach (var spell in jsonObject.Properties())
        {

            SpellData spellData = spell.Value.ToObject<SpellData>();
            dictionary[spellData.name] = spellData;

        }

    }

}
