using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class RelicNode : MonoBehaviour
{
    
    public enum AvaliableRelics
    {
        GreenGem,
        JadeElephant,
        GoldenMask,
        CursedScroll,
        KingsCharge,
        LifeSteal,
        ArmletReplenish,
        ManaBubble,
        WarlocksTome,
        BloodAmulet
    }

    private RelicData relic; // Getting the spell from the game manager dictionary
    private TextMeshProUGUI nodetext;



    [Header("Select Relic")]
    [SerializeField] private AvaliableRelics noderelic; // user selects the spell that node will be in the inspector

    [Header("References")]
    [SerializeField] private GameObject previous; // this will likely have to be an array of gameobjects

    [Header("Only select true if this node will be the 1st node of the tree!!")]
    [SerializeField] private bool startingNode = false;



    //Runs before Start()
    void Start()
    {

        // if the node is not a starting node and the previous property is not assigned
        if (previous == null && startingNode == false)
        {

            throw new System.Exception("You forgot to assign an previous node somewhere!");

        }
        
        // Note, this script will throw a reference error if you have it forced set to active before the level difficulty selection
        // This script will run normally if you open the skill tree upon the completion of the first wave
        
        switch (noderelic)
        {

            case AvaliableRelics.GreenGem:
                relic = GameManager.Instance.relics["Green Gem"];
                break;
            case AvaliableRelics.JadeElephant:
                relic = GameManager.Instance.relics["Jade Elephant"];
                break;
            case AvaliableRelics.GoldenMask:
                relic = GameManager.Instance.relics["Golden Mask"];
                break;
            case AvaliableRelics.CursedScroll:
                relic = GameManager.Instance.relics["Cursed Scroll"];
                break;
            case AvaliableRelics.KingsCharge:
                relic = GameManager.Instance.relics["Kings Charge"];
                break;
            case AvaliableRelics.LifeSteal:
                relic = GameManager.Instance.relics["Life Steal"];
                break;
            case AvaliableRelics.ArmletReplenish:
                relic = GameManager.Instance.relics["Armlet Replenish"];
                break;
            case AvaliableRelics.ManaBubble:
                relic = GameManager.Instance.relics["Mana Bubble"];
                break;
            case AvaliableRelics.WarlocksTome:
                relic = GameManager.Instance.relics["Warlocks Tome"];
                break;
            case AvaliableRelics.BloodAmulet:
                relic = GameManager.Instance.relics["Blood Amulet"];
                break;
            default:
                throw new System.Exception($"Unhandled relic type: {noderelic}");

        }



        PlaceNode(relic);

    }



    public void PlaceNode(RelicData relic)
    {

        nodetext = GetComponentInChildren<TextMeshProUGUI>();
        nodetext.text = "Relic " + relic.name + ":\n" + relic.trigger.description + ", " + relic.effect.description;

        GameManager.Instance.relicIconManager.PlaceSprite(relic.sprite, this.GetComponent<Image>());
    }



}
