using UnityEngine;
using TMPro;
using System.Collections.Generic;

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



    // Making this exist in the enemy controller first before putting it in here
    
    public static void MenuButtonsHandler()
    {
        
        

    }
    

}


