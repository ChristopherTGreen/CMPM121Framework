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
    public List<RelicData> relicrewardpool = new List<RelicData>();
    public List<RelicData> threeRelicRewards = new List<RelicData>();

    public bool relicsDisplayedFlag = false;
    private bool relicsLoadedFlag = false; //TESTING
    public bool relicSelectedFlag = false;

    public Dictionary<string, RelicData> tempActiveRelics;
    
    public void RelicRewards()
    {
        
        if (!relicsDisplayedFlag){
            relicsDisplayedFlag = true;
            rewardscreen = GetComponent<Image>();

            // Get the generated relics list from the game manager
            // TESTING: For now, manually adding relics to the list for testing
            
            // random 3 relic rewards
            if (!relicsLoadedFlag)
            {
                
                LoadAllRelicsToList(); // load all the relics from the json file to a list

            }

            // End of test segment

            ThreeRandomRelics(); //load three random relics 

            //Displays all the relics
            DisplayRelicRewards(threeRelicRewards);

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
            selector.GetComponent<Button>().onClick.AddListener(() => TakeRelicHandler(relic, selector.GetComponent<MenuSelectorController>()   ));



            //Icon handling
            GameObject relicdisplay = Instantiate(relicPrefab, rewardscreen.transform);
            relicdisplay.transform.localPosition = new Vector3(initialButtonPositionx + x_pos, -10);

            GameManager.Instance.relicIconManager.PlaceSprite(relic.sprite, relicdisplay.GetComponentInChildren<Image>()); //should place icon



            //description handling
            Debug.Log("Getting relic.trigger.description: " + relic.trigger.description);
            Debug.Log("Getting relic.effect.description: " + relic.effect.description);

            relicdisplay.GetComponentInChildren<TextMeshProUGUI>().text = "Description: " + relic.trigger.description + ", " + relic.effect.description;
            relicdisplay.GetComponentInChildren<TextMeshProUGUI>().transform.localPosition = new Vector3(0, -30); // position is based off the parent relicdisplay.transform.localPosition



            //add a tag to the display button and icon + description so the clear method can clear them without clearing the other stuff
            selector.tag = "RelicDisplay";
            relicdisplay.tag = "RelicDisplay";


            x_pos += buttonGapx; // update at end - shifts all the displays over

        }

        // Add listeners to each button
    }



    public void TakeRelicHandler(RelicData relic, MenuSelectorController buttonlabel)
    {
        //handles taking a relic and blocks off the other buttons from being clicked.

        // if we have a active relics list or dictionary in the game manager, all you need to do is add 'relic' to that list or dictionary
        // Reference the 'AssignClass' method in PlayerClassSelector.cs for how I handled the player's selected class

        if (!relicSelectedFlag)
        {
            
            GameManager.Instance.tempActiveRelics.Add(relic.name, relic);

            Debug.Log("RewardRelicDisplay.cs_TakeRelicHandler() >> Took " + relic.name);

            buttonlabel.label.text = "Relic Selected";

            RemoveRelicFromRewardPool(relic); // removes from the reward pool so it doesn't get randomly rolled later

            relicSelectedFlag = true;
        }
        else
        {

            Debug.Log("RewardRelicDisplay.cs_TakeRelicHandler() >> You already took a relic!");
            return;
        }
        
    }


    
    public void ClearRewardRelicDisplays(GameObject rewardScreen)
    {

        foreach (Transform childobj in rewardScreen.transform)
        {

            if (childobj.gameObject.CompareTag("RelicDisplay"))
            {
                
                Destroy(childobj.gameObject);

            }

        }

    }



    public void LoadAllRelicsToList()
    {

        relicsLoadedFlag = true;

        foreach (RelicData relic in GameManager.Instance.relics.Values)
        {
            Debug.Log("RelicReward.cs_Start() >> Adding " + relic.name + " to the relicrewards list.");

            relicrewardpool.Add(relic);
        }

    }



    public void ThreeRandomRelics()
    {

        threeRelicRewards.Clear(); //clear the three random relic rewards 
        int unusedRelicCount = relicrewardpool.Count;

        Debug.Log("Unused relic count: " + unusedRelicCount);


        if (unusedRelicCount == 1) // 1 relic only
        {

            System.Random rnd = new System.Random();
            int Index = rnd.Next(0, unusedRelicCount);
            
            // if the relic is not already in the rewards list, add the relic to the rewards list
            Debug.Log("Random Relic Reward: " + relicrewardpool[Index].name);
            threeRelicRewards.Add(relicrewardpool[Index]);
        
        }
        else if (unusedRelicCount == 2)
        {
            for (int i = 0; i < 2; i++) // 0, 1 - 2 relics
                {

                    System.Random rnd = new System.Random();
                    int Index = rnd.Next(0, unusedRelicCount);
                    
                    if (threeRelicRewards.Contains(relicrewardpool[Index]))
                    {
                        // if the relic is already in the rewards list, reroll the index
                        Debug.Log("Duplicate Relic Reward: " + relicrewardpool[Index].name);
                        i -= 1;
                        continue;
                    } 
                    else
                    {
                        // if the relic is not already in the rewards list, add the relic to the rewards list
                        Debug.Log("Random Relic Reward: " + relicrewardpool[Index].name);
                        threeRelicRewards.Add(relicrewardpool[Index]);
                    }

                }
        } else if (unusedRelicCount == 0)
        {
            return; // do nothing
        }
        else if (unusedRelicCount >= 3)
        {
            for (int i = 0; i < 3; i++) // 0, 1, 2 - 3 relics
            {

                System.Random rnd = new System.Random();
                int Index = rnd.Next(0, unusedRelicCount);
                
                if (threeRelicRewards.Contains(relicrewardpool[Index]))
                {
                    // if the relic is already in the rewards list, reroll the index
                    Debug.Log("Duplicate Relic Reward: " + relicrewardpool[Index].name);
                    i -= 1;
                    continue;
                } 
                else
                {
                    // if the relic is not already in the rewards list, add the relic to the rewards list
                    Debug.Log("Random Relic Reward: " + relicrewardpool[Index].name);
                    threeRelicRewards.Add(relicrewardpool[Index]);
                }

            }

        }

    }



    public void RemoveRelicFromRewardPool(RelicData relic)
    {
        
        Debug.Log("Removed relic form reward pool: " + relic.name);
        relicrewardpool.Remove(relic); // should remove the equipped relic from the pool

    }
    

}