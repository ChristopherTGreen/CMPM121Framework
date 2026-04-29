using System.Collections.Generic;
using System;
using System.IO;
using Newtonsoft.Json.Linq;

//Template Method Pattern?
public abstract class JsonProcessingTemplate<Type>
{
    
    protected Type StoreData(string filename)
    {

        JToken jsonObject = JToken.Parse(filename);
        Type jsonData = jsonObject.ToObject<Type>();
        return jsonData;

    }

    /*
    protected string ReadJsonFile(string jsonfilename)
    {

        return File.ReadAllText(jsonfilename);

    }

    protected abstract Type ParseJsonFile(string data); //Children using this template will edit this method
    */
}
