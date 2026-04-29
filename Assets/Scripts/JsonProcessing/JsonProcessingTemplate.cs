using System.Collections.Generic;
using System;
using System.IO;
using Newtonsoft.Json.Linq;

//Template Method Pattern
public abstract class JsonProcessingTemplate<Type>
{

    public Dictionary<string, Type> StoreData(string jsonfile)
    {
        
        // StoreData will return a dictionary with the json data stored in them
        Dictionary<string, Type> StoredData = new Dictionary<string, Type>(); //The 'Type' will be a type class that we define (Ex: EnemyData defined in EnemyData.cs)
        ParseData(jsonfile, StoredData); // This method will be overided depending on which json file we're parsing
        return StoredData; // Returned dictionary that has the jsondata stored in them

    }

    //Children will override this method
    protected abstract void ParseData(string jsonfile, Dictionary<string, Type> dictionary);

}
