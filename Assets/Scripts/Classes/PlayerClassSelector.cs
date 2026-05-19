using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerClassSelector : MonoBehaviour
{

    public GameObject buttonprefab;
    public Image class_selector;

    void Start()
    {
        class_selector = GetComponent<Image>();

        // spawn all the buttons and add listener. 
    }



    //might not need this
    void Update()
    {
    
    }



    // should get called in start to spawn all the buttons based off the class selection menu
    // this method gets stuck in a foreach loop
    public void DynamicButtonSpawner()
    {
        // Use the instantiate with the button prefab
        Dictionary<string, ClassData> class_dictionary = RetrieveClassData.ClassDictionary();
        int y_pos = 0;

        foreach (var playerclass in class_dictionary)
        {
            
            //creates a button using the button prefab in the engine and the class_selector's bg position
            GameObject selector = Instantiate(buttonprefab, class_selector.transform);

        }

    }



    // This method gets attached to the class button listener
    // it should take a parameter that is the class data.
    // The assign it as the player stats.
    // maybe make it it's own class?
    public void AssignClass(ClassData classData)
    {
        
    }

}