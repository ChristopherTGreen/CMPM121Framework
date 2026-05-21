// this script should handle the display of the collection buttons, description and icons 
using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;



public class RewardRelicDisplay : MonoBehaviour
{
    
    // Unity inspector stuff - assign in there
    public GameObject relicPrefab;
    public GameObject buttonPrefab;
    public Image rewardscreen;
    public List<RelicData> relicrewards = new List<RelicData>();

    public bool relicsDisplayedFlag = false;
    private bool relicsLoadedFlag = false; //TESTING
    
    public void RelicRewards()
    {
        
        if (!relicsDisplayedFlag){
            relicsDisplayedFlag = true;
            rewardscreen = GetComponent<Image>();

            // Get the generated relics list from the game manager
            // TESTING: For now, manually adding relics to the list for testing
            
            if (!relicsLoadedFlag)
            {
                
                relicsLoadedFlag = true;
                foreach (RelicData relic in GameManager.Instance.relics.Values)
                {
                    Debug.Log("RelicReward.cs_Start() >> Adding " + relic.name + " to the relicrewards list.");

                    relicrewards.Add(relic);
                }

            }

            // End of test segment

            //Displays all the relics
            DisplayRelicRewards(relicrewards);

        } else
        {
            return;
        }
    }

    public void DisplayRelicRewards(List<RelicData> randomrelics)
    {
        // get a list of three randomly selected relics
        int x_pos = 0;

        const int initialButtonPositionx = -200;
        const int buttonGapx = 200;

        // Instantiate the button and all the displays
        foreach (RelicData relic in randomrelics)
        {
            
            //creates a button using the button prefab in the engine and the class_selector's bg position

            //buttons
            GameObject selector = Instantiate(buttonPrefab, rewardscreen.transform);
            selector.transform.localPosition = new Vector3(initialButtonPositionx + x_pos, -65); //testing position - adjust x later

            selector.GetComponent<MenuSelectorController>().label.text = "Take";
            selector.GetComponent<MenuSelectorController>().spawner = null; //sets the spawner to null so the StartLevel() in the MenuSelectorController just returns instead of staating the level
            selector.GetComponent<Button>().onClick.RemoveAllListeners();
            selector.GetComponent<Button>().onClick.AddListener(() => TakeRelicHandler(relic));



            //Icon handling
            GameObject relicdisplay = Instantiate(relicPrefab, rewardscreen.transform);
            relicdisplay.transform.localPosition = new Vector3(initialButtonPositionx + x_pos, -10);

            GameManager.Instance.relicIconManager.PlaceSprite(relic.sprite, relicdisplay.GetComponentInChildren<Image>()); //should place icon



            //description handling
            Debug.Log("Getting relic.trigger.description: " + relic.trigger.description);
            Debug.Log("Getting relic.effect.description: " + relic.effect.description);

            relicdisplay.GetComponentInChildren<TextMeshProUGUI>().text = "Description: " + relic.trigger.description + ", " + relic.effect.description;
            relicdisplay.GetComponentInChildren<TextMeshProUGUI>().transform.localPosition = new Vector3(0, -30); // position is based off the parent relicdisplay.transform.localPosition


            x_pos += buttonGapx; // update at end - shifts all the displays over

        }

        // Add listeners to each button
    }

    public void TakeRelicHandler(RelicData relic)
    {
        //handles taking a relic and blocks off the other buttons from being clicked.
        Debug.Log("RewardRelicDisplay.cs_TakeRelicHandler() >> Took " + relic.name);

        // if we have a active relics list or dictionary in the game manager, all you need to do is add 'relic' to that list or dictionary
        // Reference the 'AssignClass' method in PlayerClassSelector.cs for how I handled the player's selected class
    }

}