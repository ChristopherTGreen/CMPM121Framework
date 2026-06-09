
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class ModifierNode : MonoBehaviour
{
    
    public enum AvaliableModifiers
    {
        DamageAmp = 4,
        SpeedAmp = 5,
        PierceAmp = 6,
        HealAmp = 7,
        Bounce = 8,
        Doubler = 9,
        Splitter = 10,
        Broken = 11,
        Massive = 12,
        Chaos = 13,
        Homing = 14,
        Bubble = 3 //will have to use a spell's  
    }

    private SpellData spellmod; // Getting the spell from the game manager dictionary
    private TextMeshProUGUI nodetext;
    private float lockedAlphaLvl = 0.7f; // same locked alpha lvl for all disabled nodes across all node scripts
    private Button iconbutton;




    [Header("Select Modifier")]
    [SerializeField] private AvaliableModifiers nodemod; // user selects the spell that node will be in the inspector

    [Header("References")]
    [SerializeField] private GameObject[] previous; // this will likely have to be an array of gameobjects

    [Header("Node Flags (For Debugging)")] // all of these flags need to be the same name across all node scripts
    [SerializeField] private bool nodeUnlockedFlag = false;
    public bool nodeCollectedFlag = false; 

    [Header("Only select true if this node will be the 1st node of the tree!!")]
    [SerializeField] private bool startingNode = false;


    
    //Runs before Start()
    void Start()
    {

        iconbutton = this.GetComponent<Button>();

        // can't find button component? Throw error - button component is needed 
        if (iconbutton == null)
        {

            throw new System.Exception("No button component attached to this node, see the hierarchy for highlighted node");

        }

        // if the node is not a starting node and the previous property is not assigned
        if (previous == null && startingNode == false)
        {

            throw new System.Exception("You forgot to assign an previous node somewhere!");

        }

        // if the node is the starting node, do not make it transparent, else do make it transparent (locked)
        if (!startingNode && previous != null)
        {

            LockNode(); // icon transparency and eventually the disabling of the button

        }
        
        // Note, this script will throw a reference error if you have it forced set to active before the level difficulty selection
        // This script will run normally if you open the skill tree upon the completion of the first wave
        
        switch (nodemod)
        {

            case AvaliableModifiers.DamageAmp:
                spellmod = GameManager.Instance.spells["damage-amplified"];
                break;
            case AvaliableModifiers.SpeedAmp:
                spellmod = GameManager.Instance.spells["speed-amplified"];
                break;
            case AvaliableModifiers.PierceAmp:
                spellmod = GameManager.Instance.spells["pierce-amplified"];
                break;
            case AvaliableModifiers.HealAmp:
                spellmod = GameManager.Instance.spells["heal-amplified"];
                break;
            case AvaliableModifiers.Bounce:
                spellmod = GameManager.Instance.spells["bounce"];
                break;
            case AvaliableModifiers.Doubler:
                spellmod = GameManager.Instance.spells["doubled"];
                break;
            case AvaliableModifiers.Splitter:
                spellmod = GameManager.Instance.spells["split"];
                break;
            case AvaliableModifiers.Broken:
                spellmod = GameManager.Instance.spells["Broken"];
                break;
            case AvaliableModifiers.Massive:
                spellmod = GameManager.Instance.spells["Massive"];
                break;
            case AvaliableModifiers.Chaos:
                spellmod = GameManager.Instance.spells["chaotic"];
                break;
            case AvaliableModifiers.Homing:
                spellmod = GameManager.Instance.spells["homing"];
                break;
            case AvaliableModifiers.Bubble:
                spellmod = GameManager.Instance.spells["bubble"];
                break;
            default:
                throw new System.Exception($"Unhandled modifier type: {nodemod}");
                
        }


        ButtonHandler();
        PlaceNode(spellmod);

    }



    // checking for previous everytime - may cause performance issues as each node will be checking every frame
    // for it's previous to be collected
    // State machine for node states? Temporarilty doing a flag right now
    void Update()
    {
        
        if (!startingNode && !nodeUnlockedFlag)
        {
        
            if (CheckPreviousCollected(previous))
            {
                nodeUnlockedFlag = true;
                UnlockNode();
            }

        }

    }



    public void PlaceNode(SpellData spellmod)
    {

        nodetext = GetComponentInChildren<TextMeshProUGUI>();
        nodetext.text = "Spell Modifier: " + spellmod.name + ":\n" + spellmod.description;

        // (int) nodemod gets the int of the modifier name
        GameManager.Instance.spellIconManager.PlaceSprite((int)nodemod, this.GetComponent<Image>());
    }



    //Adds the listener to the button component
    private void ButtonHandler()
    {
        
        iconbutton.onClick.RemoveAllListeners();
        iconbutton.onClick.AddListener(() => CollectMod());

    }



    //Listener to the button - needs to be public for Unity's onClick happy funtime thingy thing 
    public void CollectMod()
    {
        
        Debug.Log("You have collected " + spellmod.name + "!");

        //put actual collection here. Switch statement?




        

        //then disable the button so the player can't click the node again
        iconbutton.interactable = false;
        nodeCollectedFlag = true; // indicate the node is collected for the previous check in Update()

    }



    private void LockNode()
    {

        //disable button feature
        iconbutton.interactable = false; //disables button component

        //reduce transparency
        Color alphaAdjust = this.GetComponent<Image>().color;
        alphaAdjust.a = lockedAlphaLvl;
        this.GetComponent<Image>().color = alphaAdjust;

    }



    // when player gets all of the previous nodes, then run this function
    private void UnlockNode()
    {
        
        //enable button feature
        iconbutton.interactable = true;

        //disable transparency
        Color alphaAdjust = this.GetComponent<Image>().color;
        alphaAdjust.a = 1.0f; //100% transparency
        this.GetComponent<Image>().color = alphaAdjust;

    }



    //returns true if all of the previous nodes have been collected
    //returns false if not all of the previous nodes have been collected
    private bool CheckPreviousCollected(GameObject[] objArray)
    {
        
        foreach(GameObject prev in objArray)
        {
            
            // if the node collected flag of any of the previous components are false (not collected) return false
            if (prev.GetComponent<SpellNode>() != null)
            {
                
                if (!prev.GetComponent<SpellNode>().nodeCollectedFlag)
                {
                    return false;
                }

            } else if (prev.GetComponent<RelicNode>() != null)
            {
                
                if (!prev.GetComponent<RelicNode>().nodeCollectedFlag)
                {
                    return false;
                }

            } else
            {
                
                if (!prev.GetComponent<ModifierNode>().nodeCollectedFlag)
                {
                    return false;
                }

            }

        }

        return true; //fpreach loop finished, therefore true

    }




}


