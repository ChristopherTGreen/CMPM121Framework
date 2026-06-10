using UnityEngine.UI;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class SpellNode : MonoBehaviour
{
    
    public enum AvaliableSpells
    {
        ArcaneBolt, //arcane bolt will be the default
        MagicMissile,
        ArcaneBlast,
        ArcaneSpray
    }



    private SpellData spell; // Getting the spell from the game manager dictionary
    private TextMeshProUGUI nodetext;
    private float lockedAlphaLvl = 0.7f; // same locked alpha lvl for all disabled nodes across all node scripts
    private Button iconbutton;
    [SerializeField] private Color collectedColor = new Color(0.35f, 1f, 0.35f, 1f);
    private Image nodeImage;


    [Header("Select Spell")]
    [SerializeField] private AvaliableSpells nodespell; // user selects the spell that node will be in the inspector

    [Header("References")]
    [SerializeField] private GameObject[] previous; // this will likely have to be an array of gameobjects

    [Header("Node Flags (For Debugging)")] // all of these flags need to be the same name across all node scripts
    [SerializeField] private bool nodeUnlockedFlag = false;
    public bool nodeCollectedFlag = false; 

    [Header("Only select true if this node will be the 1st node of the tree!!")]
    [SerializeField] private bool startingNode = false;


    
    void Start()
    {

        iconbutton = this.GetComponent<Button>();
        nodeImage = this.GetComponent<Image>();

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
        
        switch (nodespell)
        {

            case AvaliableSpells.ArcaneBolt:
                spell = GameManager.Instance.spells["Arcane Bolt"];
                break;
            case AvaliableSpells.MagicMissile:
                spell = GameManager.Instance.spells["Magic Missile"];
                break;
            case AvaliableSpells.ArcaneBlast:
                spell = GameManager.Instance.spells["Arcane Blast"];
                break;
            case AvaliableSpells.ArcaneSpray:
                spell = GameManager.Instance.spells["Arcane Spray"];
                break;

        }


        ButtonHandler();
        PlaceNode(spell);

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



    private void PlaceNode(SpellData spell)
    {

        nodetext = GetComponentInChildren<TextMeshProUGUI>();
        nodetext.text = "Spell " + spell.name + ":\n" + spell.description;

        GameManager.Instance.spellIconManager.PlaceSprite(spell.icon, this.GetComponent<Image>());
    }



    //Adds the listener to the button component
    private void ButtonHandler()
    {
        
        iconbutton.onClick.RemoveAllListeners();
        iconbutton.onClick.AddListener(() => CollectSpell());

    }



    //Listener to the button - needs to be public for Unity's onClick happy funtime thingy thing 
    public void CollectSpell()
    {
        if (!SkillTreeRewardManager.CanSelectNode())
        {
            Debug.Log("No skill tree selections remaining this round.");
            return;
        }

        Debug.Log("You have collected " + spell.name + "!");

        SkillTreeRewardManager.GrantSpell(nodespell);
        SkillTreeRewardManager.RegisterNodeSelection();

        SetCollectedVisual();
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
        if (nodeCollectedFlag)
        {
            return;
        }

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


    public AvaliableSpells GetSpellType()
    {
        return nodespell;
    }



    private void SetCollectedVisual()
    {
        nodeCollectedFlag = true;
        iconbutton.interactable = false;

        if (nodeImage != null)
        {
            nodeImage.color = collectedColor;
        }
    }
}
