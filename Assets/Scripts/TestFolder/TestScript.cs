using UnityEngine;
using Newtonsoft.Json;


public class TestScript : MonoBehaviour
{
    //Put any references a class needs from the Unity inspector here 
    // [SerializeField] private Type Variablename;
    EnemyStoringTest TestingEnemy = new EnemyStoringTest();

    void Start()
    {
        
        //Call any class you want to test in here.

        //Testing Json reading and parsing
        TestingEnemy.EnemyDebugStatements(Resources.Load<TextAsset>("enemies").text); //figure out how to reference the json file

    }

    public class EnemyStoringTest
    {
        
        //Declarations - Have to do this or the script will scream at you :/
        public EnemyData enemy;
        public EnemyParsing enemyParse = new EnemyParsing();

        public void EnemyDebugStatements(string filename)
        {
            
            enemy = enemyParse.StoreData(filename);
            Debug.Log("Enemy Data: " + enemy);

        } 

    }
}
