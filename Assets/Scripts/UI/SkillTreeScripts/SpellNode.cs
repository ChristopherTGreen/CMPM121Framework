using UnityEngine.UI;
using UnityEngine;
using TMPro;

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

    //For line rendering
    private LineRenderer linerenderer;

    [Header("Important Note:")]
    [TextArea(3, 10)] 
    public string Notes = "Make sure the z-xis of the rect transform component is set to -1 on all nodes or else the connector lines will not show.";

    [Header("Select Spell")]
    [SerializeField] private AvaliableSpells nodespell; // user selects the spell that node will be in the inspector

    [Header("Line Rendering References")]
    [SerializeField] private GameObject thisNode;
    [SerializeField] private GameObject previous; // this will likely have to be an array of gameobjects

    [Header("Only select true if this node will be the 1st node of the tree!!")]
    [SerializeField] private bool startingNode = false;



    //Runs before Start()
    void Start()
    {

        //Line rendering handling
        if (previous != null) 
        {

            linerenderer = GetComponent<LineRenderer>();
            linerenderer.positionCount = 2; //2 pts: A start and an end

            linerenderer.widthMultiplier = 5.0f; //large for testing

        } else if (previous == null && startingNode == false)
        {

            throw new System.Exception("You forgot to assign an previous node somewhere!");

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


        PlaceNode(spell);

    }



    public void PlaceNode(SpellData spell)
    {

        nodetext = GetComponentInChildren<TextMeshProUGUI>();
        nodetext.text = "Spell " + spell.name + ":\n" + spell.description;

        GameManager.Instance.spellIconManager.PlaceSprite(spell.icon, this.GetComponent<Image>());
    }



    void Update()
    {
        
        if (previous != null && !startingNode){

            //error checking
            if (linerenderer == null)
            {
                throw new System.Exception("Not receiving linerenderer");
            } else if (thisNode.transform.position == null)
            {
                throw new System.Exception("Not getting initial position");
            } else if (previous.transform.position == null)
            {
                throw new System.Exception("Not getting the final position");
            }

            //drawing a line between nodes
            linerenderer.SetPosition(0, thisNode.transform.position);
            linerenderer.SetPosition(1, previous.transform.position);
        }

    }

}
