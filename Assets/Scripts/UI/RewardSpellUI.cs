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
    public PlayerController player;

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
    
    public void DisplaySpell(Spell rewardSpell)
    {
        spellui.SetSpell(rewardSpell);
    }

    public void AcceptButtonHandler()
    {
        Accept.onClick.RemoveAllListeners();
        Accept.onClick.AddListener(() => AcceptSpell());

        // if spell list not full, add
        // else highlight red for a bit?
        //); //When pressed, and if not full, takes in new spell
    }



    public void AcceptSpell()
    {
        Debug.Log("Accept button clicked")
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