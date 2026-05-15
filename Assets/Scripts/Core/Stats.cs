using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[System.Serializable]
public class Stats : MonoBehaviour
{
    public int enemiesKilled { get; set; } = 0;
    public int totalDamageDealt { get; set; } = 0;
    

    public void resetStats()
    {
        enemiesKilled = 0;
        totalDamageDealt = 0;
    }

    public string getStats()
    {
        return "Enemies Killed: " + enemiesKilled + "\nTotal Damage Dealt: " + totalDamageDealt;

    }

    public string getSpellDescription()
    {

        string description = "Spell Description: " + GameManager.Instance.currentRewardSpell.description + "\n\n" + "Modifiers: ";

        foreach (string modname in GameManager.Instance.currentRewardSpell.stats.name)
        {
            Debug.Log("Modifier name: " + modname);
            description += modname + ", ";
        }

        // temporary hard coded description - this will later get the description of the randomly generated spell
        return description;
    }
}