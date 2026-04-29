using UnityEngine;
using Newtonsoft.Json;
using System.Collections.Generic;
using Unity.VisualScripting;


public class TestScript : MonoBehaviour
{
    //Put any references a class needs from the Unity inspector here 
    // [SerializeField] private Type Variablename;



    // Put any declarations needed for testing here
    EnemyStoringTest TestingEnemyStoring = new EnemyStoringTest();
    LevelStoringTest TestingLevelStoring = new LevelStoringTest();

    void Start()
    {
        
        //Call any class you want to test in here.

        //Testing Json reading, parsing and storing
        TestingEnemyStoring.EnemyDebugStatements(Resources.Load<TextAsset>("enemies").text); //figure out how to reference the json file
        TestingLevelStoring.LevelDebugStatements(Resources.Load<TextAsset>("levels").text);

    }

    public class EnemyStoringTest
    {
        
        //Declarations - Have to do this or the script will scream at you :/
        private EnemyParsing EnemyParse = new EnemyParsing();

        public void EnemyDebugStatements(string loadedjsonfile)
        {
            
            Dictionary<string, EnemyData> StoredEnemyData = EnemyParse.StoreData(loadedjsonfile);

            // Printing the returned dictionary from StoreData to Unity's console
            Debug.Log("Enemy Dictionary: " + JsonConvert.SerializeObject(StoredEnemyData, Formatting.Indented));

        } 

    }

    public class LevelStoringTest
    {
        
        private LevelParsing LevelParse = new LevelParsing();

        public void LevelDebugStatements(string loadedjsonfile)
        {
            
            Dictionary<string, LevelData> StoredLevelData = LevelParse.StoreData(loadedjsonfile);

            Debug.Log("Level Dictionary: " + JsonConvert.SerializeObject(StoredLevelData, Formatting.Indented));

        } 

    }

}


