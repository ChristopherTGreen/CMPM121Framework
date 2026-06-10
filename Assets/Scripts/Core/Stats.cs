using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[System.Serializable]
public class Stats : MonoBehaviour
{
    public int enemiesKilled { get; set; } = 0;
    public int totalDamageDealt { get; set; } = 0;
    public Spell currentSpell { get ; set ; }
    public List<string> currentSpellModNames = new List<string>();
    

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
        if (currentSpell == null)
        {
            return "";
        }

        string description = "Spell Description: " + currentSpell.description + "\n\n" + "Modifiers: ";

        foreach (string modname in currentSpellModNames)
        {
            description += modname + ", ";
        }

        return description;
    }

    public void ClearSpell(int index)
    {
        if (GameManager.Instance.GetFilledSlotCount() < 4) return;
       
        GameManager.Instance.activeSpells[index] = null;

        GameManager.Instance.spellUIcontainer.spellUIs[index].GetComponent<SpellUI>().spell = null;
        GameManager.Instance.spellUIcontainer.spellUIs[index].SetActive(false);
        //currentSpellModNames.Clear();
    }

}