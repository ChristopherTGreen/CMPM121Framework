using UnityEngine;
using Newtonsoft.Json;
using System.Collections.Generic;
using Unity.VisualScripting;


public class TestScript : MonoBehaviour
{
    //Put any references a class needs from the Unity inspector here 
    // [SerializeField] private Type Variablename;



    // Put any declarations needed for testing here



    void Start()
    {
        
        //Call any class you want to test in here.

        //Testing Json reading, parsing and storing
        Debug.Log("Enemies Dictionary: " + JsonConvert.SerializeObject(RetrieveEnemyData.EnemyDictionary(), Formatting.Indented));
        Debug.Log("Levels Dictionary: " + JsonConvert.SerializeObject(RetrieveLevelData.LevelDictionary(), Formatting.Indented));

    }



}


