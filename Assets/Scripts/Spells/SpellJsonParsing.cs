using Newtonsoft.Json.Linq;
using System.Collections.Generic;


public class SpellParsing : JsonProcessingTemplate<SpellData>
{
    
    protected override void ParseData(string jsonfile, Dictionary<string, SpellData> dictionary)
    {

        JObject jsonObject = JObject.Parse(jsonfile); //gets all of the objects from the Json file

        foreach (var spell in jsonObject.Properties())
        {

            string SpellName = spell.Name; // .Name takes the string outside of the curly brackets (The spell name)
            SpellData spellData = spell.Value.ToObject<SpellData>(); //.Value gets everything in the curly brackets
            dictionary[SpellName] = spellData;

        }

    }

}
