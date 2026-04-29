using UnityEngine;
using Newtonsoft.Json;
using System.Collections.Generic;
using Unity.VisualScripting;


public class TestScript : MonoBehaviour
{
    //Put any references a class needs from the Unity inspector here 
    // [SerializeField] private Type Variablename;



    // Put any declarations needed for testing here
    EnemyStoringTest TestingEnemy = new EnemyStoringTest();

    void Start()
    {
        
        //Call any class you want to test in here.

        //Testing Json reading, parsing and storing
        TestingEnemy.EnemyDebugStatements(Resources.Load<TextAsset>("enemies").text); //figure out how to reference the json file

    }

    public class EnemyStoringTest
    {
        
        //Declarations - Have to do this or the script will scream at you :/
        public EnemyParsing EnemyParse = new EnemyParsing();

        public void EnemyDebugStatements(string loadedjsonfile)
        {
            
            Dictionary<string, EnemyData> StoredEnemyData = EnemyParse.StoreData(loadedjsonfile);

            // Printing the returned dictionary from StoreData to Unity's console
            Debug.Log("Dictionary: " + JsonConvert.SerializeObject(StoredEnemyData));

        } 

    }
}
