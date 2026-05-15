using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class RewardSpell : MonoBehaviour
{
    public GameObject spellReward;
    public SpellUI spellui;
    public Button Accept; // might need to get respective gameobject in scene
    public Button Drop;
    public PlayerController player; //for the player's spellcaster

    public bool RewardSpellGenerated = false;
    public static Spell newRewardSpell;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //spellReward.SetActive(false); // should be set false when game is over
    }

    // No need for update loop
    /// <summary>
    ///  Display spell with take the generated random spell
    /// </summary>
    /// <param name="rewardSpell"></param>
    
    // this will get called in the reward screen manager at wave end state with a randomly generated spell
    public void DisplaySpell(Spell rewardSpell)
    {
        spellui = GameManager.Instance.spellUI;

        Debug.Log($"new spell in DisplaySpell null? {rewardSpell == null}");
        Debug.Log($"spellui class in DisplaySpell null? {spellui == null}");

        GameManager.Instance.spellIconManager.PlaceSprite(rewardSpell.GetIcon(), spellui.icon.GetComponent<Image>());
        
        //spellui.SetSpell(rewardSpell); //SetSpell is located in SpellUI.cs
    }

    public void AcceptButtonHandler()
    {

        if (!RewardSpellGenerated)
        {
            // Find player controller here instead of start() - I don't like it but it's the only place where if can even find the playercontroller
            player = GameManager.Instance.player.GetComponent<PlayerController>(); 

            Debug.Log($"player null? {player == null}");

            RewardSpellGenerated = true; // flag makes sure this conditional only runs once per wave end state 
            newRewardSpell = new RandomModifier().CreateRandomSpell(player.spellcaster); // not player.spellcaster.spell because that would replace the player's currently active spell - this makes a new spell

            Debug.Log($"new spell null? {newRewardSpell == null}");

            DisplaySpell(newRewardSpell); //Displays the icon and sprite of the new spell + manacost and damage text
        }
        

        Accept.onClick.RemoveAllListeners();
        Accept.onClick.AddListener(() => AcceptSpell(newRewardSpell));

        // if spell list not full, add
        // else highlight red for a bit?
        //); //When pressed, and if not full, takes in new spell
    }



    public void AcceptSpell(Spell spell)
    {
        Debug.Log("RewardSpellUI.cs_AcceptSpell(Spell) >> Accept button clicked");

        GameManager.Instance.activeSpells[spell.name] = spell;
        Debug.Log("RewardSpellUI.cs_AcceptSpell(Spell) >> Stored " + spell.name + "as a active spell after player accepted the spell.");
    }



    public void DropButtonHandler()
    {
        Drop.onClick.RemoveAllListeners();
        Drop.onClick.AddListener(() => DropSpell());
    }



    public void DropSpell()
    {
        Debug.Log("Drop button clicked");
    }


    /*
    public void RandomFunc()
    {
        var keys = new List<string>(GameManager.Instance.spells.Keys);
        string randomKey = keys[Random.Range(0, keys.Count)];
        Spell builtSpell = ;
        GameManager.Instance.spells[randomKey];
    }
    */
}