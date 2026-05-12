using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

class RewardSpell : MonoBehaviour
{
    public GameObject spellReward;
    public SpellUI spellui;
    public PlayerController player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //spellReward.SetActive(false); // should be set false when game is over
    }

    // No need for update loop

    public void DisplaySpell(Spell rewardSpell)
    {
        spellui.SetSpell(rewardSpell);
    }

    public void RandomFunc()
    {
        var keys = new List<string>(GameManager.Instance.spells.Keys);
        string randomKey = keys[Random.Range(0, keys.Count)];
        Spell builtSpell = ;
        GameManager.Instance.spells[randomKey];
    }
}