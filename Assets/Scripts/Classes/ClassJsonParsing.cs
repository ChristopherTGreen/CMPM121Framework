using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;


public class ClassParsing : JsonProcessingTemplate<ClassData>
{

    protected override void ParseData(string jsonfile, Dictionary<string, ClassData> dictionary)
    {

        JObject jsonObject = JObject.Parse(jsonfile); //gets all of the objects from the Json file
        
        foreach (var classType in jsonObject.Properties())
        {

            ClassData classData = classType.Value.ToObject<ClassData>();
            dictionary[classData.name] = classData;

        }

    }

}
