using UnityEngine;
using TMPro;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine.PlayerLoop;

public class MenuSelectorController : MonoBehaviour
{
    public TextMeshProUGUI label;
    public string level;
    public EnemySpawner spawner;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetLevel(string text)
    {
        level = text;
        label.text = text;
    }

    public void StartLevel()
    {
        spawner.StartLevel(level);
    }



    // Making this exist in the enemy spawner first before putting it in here
    // need to get the Unity inspector references in the EnemySpawner class
    public static void DynamicMenuButtonSpawner(EnemySpawner EnemySpawnerClassReferences)
    {
        
        Dictionary<string, LevelData> level_dictionary = RetrieveLevelData.LevelDictionary();
        int y_pos = 0; // Initial button was too high on the UI so this lowers it

        const int initialButtonPosition = 190;
        const int buttonGap = 40;

        foreach (var difficulty in level_dictionary)
        {

            /* Note From Jay:
            Okay so from my understanding: button is a prefab referenced in the Unity inspector under the 
            "EnemySpawner" script. The EnemySpawner script for some reason is attached to Main Camera (Why?!?).

            level_selector references a UI image for the buttons. This reference is also in the EnemySpawner

            *I think* selector.GetComponent<MenuSelectorController>().spawner = EnemySpawnerClassReferences; 
            links the enemy spawner to the button in some way. 

            I took what the professor originally had in the Start() method in the EnemySpawner, stuck it
            in a for loop and stuck it in a method in the MenuSelectorController. 
            */

            GameObject selector = Instantiate(EnemySpawnerClassReferences.button, EnemySpawnerClassReferences.level_selector.transform);

            selector.transform.localPosition = new Vector3(0, initialButtonPosition - y_pos); //Button position
            y_pos += buttonGap; //spacing between buttons 
            // possibly get rid of these constants for ease of adjustment

            selector.GetComponent<MenuSelectorController>().spawner = EnemySpawnerClassReferences;
            selector.GetComponent<MenuSelectorController>().SetLevel(difficulty.Key); //Sets the text on the button (selector)

        }

    }
    
}


