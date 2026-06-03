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

    [Header("References")]
    [SerializeField] private AvaliableSpells nodespell; // user selects the spell that node will be in the inspector
    public GameObject previous;



    //Runs before Start()
    void Start()
    {
        
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
        nodetext.text = spell.description;

        GameManager.Instance.spellIconManager.PlaceSprite(spell.icon, this.GetComponent<Image>());
    }

}
