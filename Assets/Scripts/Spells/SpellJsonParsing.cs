using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.Json;

public class SpellParsing : JsonProcessingTemplate<SpellData>
{
    
    protected override void ParseData(string jsonfile, Dictionary<string, SpellData> dictionary)
    {
        JToken jsonObject = JToken.Parse(jsonfile);
    }

}