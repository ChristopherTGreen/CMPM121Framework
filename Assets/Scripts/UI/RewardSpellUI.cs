using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardSpell
{
    public GameObject spellReward;
    public Button Accept; // might need to get respective gameobject in scene
    public TextMeshProUGUI AcceptButtonText;
    public Button Drop;
    public PlayerController player; //for the player's spellcaster

    public bool RewardSpellGenerated = false;
    private Spell newRewardSpell;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public RewardSpell()
    {
        //spellReward.SetActive(false); // should be set false when game is over
    }

    public void SetSpellUI(GameObject spellObject)
    {
        spellReward = spellObject;
    }

    // No need for update loop
    /// <summary>
    ///  Display spell with take the generated random spell
    /// </summary>
    /// <param name="rewardSpell"></param>
    
    // this will get called in the reward screen manager at wave end state with a randomly generated spell
    public void DisplaySpell(Spell rewardSpell)
    {
        spellReward.GetComponent<SpellUI>().SetSpell(rewardSpell); //SetSpell is located in SpellUI.cs
        GameManager.Instance.sessionStats.currentSpell = rewardSpell;
    }

    public void AcceptButtonHandler()
    {

        if (!RewardSpellGenerated)
        {
            // Find player controller here instead of start() - I don't like it but it's the only place where if can even find the playercontroller
            player = GameManager.Instance.player.GetComponent<PlayerController>(); 

            //Debug.Log($"player null? {player == null}");

            RewardSpellGenerated = true; // flag makes sure this conditional only runs once per wave end state 
            newRewardSpell = new RandomModifier().CreateRandomSpell(player.spellcaster); // not player.spellcaster.spell because that would replace the player's currently active spell - this makes a new spell
            
            DisplaySpell(newRewardSpell); //Displays the icon and sprite of the new spell + manacost and damage text

            Accept.onClick.RemoveAllListeners();
            Accept.onClick.AddListener(() => AcceptSpell(newRewardSpell));
        }

        // if spell list not full, add
        // else highlight red for a bit?
        //); //When pressed, and if not full, takes in new spell
    }



    public void AcceptSpell(Spell spell)
    {
        Debug.Log("RewardSpellUI.cs_AcceptSpell(Spell) >> Accept button clicked");
        int size = GameManager.Instance.GetFilledSlotCount();
        if (size < 4){ //for testing, set to 2

            GameManager.Instance.StoreActiveSpell(spell); //adds the newly generated spell to the active spells list
            GameManager.Instance.spellUIcontainer.ShowActiveSpells(); //unhides the active spell icons in the bottom left based on number of active spells - Now sets the stats and icon

            Accept.onClick.RemoveAllListeners(); //removes the ability for the button to call Accept Spell
            AcceptButtonText = Accept.GetComponentInChildren<TextMeshProUGUI>();
            AcceptButtonText.text = "Spell Collected";

            Debug.Log("RewardSpellUI.cs_AcceptSpell(Spell) >> " + spell.name + "Collected");

        } 
        else if (size < 1)
        {
            throw new Exception("RewardSpellUI.cs_AcceptSpell(Spell) >> SOMETHING IS WRONG YOU SHOULDN'T HAVE ZERO ACTIVE SPELLS!!!");
        } 
        else
        {
            Debug.Log("RewardSpellUI.cs_AcceptSpell(Spell) >> Too Many Spells, please drop");

            //More than four spells
            //Enable drop buttons
            GameManager.Instance.spellUIcontainer.ShowDropButton();
            AcceptButtonText = Accept.GetComponentInChildren<TextMeshProUGUI>();
            AcceptButtonText.text = "Too Many Spells!";

        }

    }



    public void DropButtonHandler()
    {
        Drop.onClick.RemoveAllListeners();
        Drop.onClick.AddListener(() => DropSpell());
    }



    public static void DropSpell()
    {
        //GameManager.Instance.sessionStats.clearSpell
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