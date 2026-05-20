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
        DynamicButtonSpawner();

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

        const int initialButtonPosition = 70;
        const int buttonGap = 40;

        foreach (ClassData playerclass in class_dictionary.Values)
        {
            
            //creates a button using the button prefab in the engine and the class_selector's bg position
            GameObject selector = Instantiate(buttonprefab, class_selector.transform);
            selector.transform.localPosition = new Vector3(0, initialButtonPosition - y_pos);
            y_pos += buttonGap;

            selector.GetComponent<MenuSelectorController>().label.text = "Class: " + playerclass.name;
            selector.GetComponent<MenuSelectorController>().spawner = null; //sets the spawner to null so the StartLevel() in the MenuSelectorController just returns instead of staating the level
            selector.GetComponent<Button>().onClick.AddListener(() => AssignClass(playerclass));

        }

    }



    // This method gets attached to the class button listener
    // it should take a parameter that is the class data.
    // The assign it as the player stats.
    // maybe make it it's own class?
    public void AssignClass(ClassData classData)
    {
        Debug.Log("Assign Class Button Clicked");

        HideClassSelectionUI();

        //send the classData name to the playercontroller somehow.
        // Gamemanager
        GameManager.Instance.chosenClass = classData;
        Debug.Log("PlayerClassSelector.cs_AssignClass(ClassData) >> Chosen Class: " + GameManager.Instance.chosenClass.name);

    }



    // Hide the player class selection UI
    public void HideClassSelectionUI()
    {
        class_selector.gameObject.SetActive(false);
    }

}