using System.Collections.Generic;
using System;
using System.IO;
using Newtonsoft.Json.Linq;

//Template Method Pattern?
public abstract class JsonProcessingTemplate<Type>
{

    private Dictionary<string, Type> StoreData(string jsonfile)
    {
        
        Dictionary<string, Type> StoredData = new Dictionary<string, Type>();
        ParseData(jsonfile, StoredData);
        return StoredData;
    }

    //Children will override this method
    protected abstract void ParseData(string jsonfile, Dictionary<string, Type> dictionary);

    /*
    protected string ReadJsonFile(string jsonfilename)
    {

        return File.ReadAllText(jsonfilename);

    }

    protected abstract Type ParseJsonFile(string data); //Children using this template will edit this method
    */
}
