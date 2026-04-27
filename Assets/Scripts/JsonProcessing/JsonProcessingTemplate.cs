namespace JsonProcessing 
{

    //Template Method Pattern?
    public abstract class GeneralJsonParsing<Type>
    {
        
        public Type StoreData(string filename)
        {

            Type ParsedData = ParseJsonFile(ReadJsonFile(filename));
            return ParsedData;

        }

        protected string ReadJsonFile(string jsonfilename)
        {
            
            //error throwing/crash program if jsonfile is not provided
            if (!File.Exists(jsonfilename))
            {
                throw new Exception("Json file not provided. Please provide a Json file to parse.");
            }

            return File.ReadAllText(filename);

        }

        protected Type ParseJsonFile(string jsonfilename); //Classes using this template will edit this method

    }

}
