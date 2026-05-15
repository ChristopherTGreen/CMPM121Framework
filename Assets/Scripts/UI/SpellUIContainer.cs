using System.Collections.Generic;
using UnityEngine;

public class SpellUIContainer : MonoBehaviour
{
    public GameObject[] spellUIs;
    public PlayerController player;
    private List<Spell> activeSpellList;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        activeSpellList = GameManager.Instance.activeSpells;

        // we only have one spell (right now)
        spellUIs[0].SetActive(true); // first spell is visible at start

        for(int i = 1; i< spellUIs.Length; ++i)
        {
            spellUIs[i].SetActive(false); //hide the rest
        }

        GameManager.Instance.spellUIcontainer = this;
    }
    
    public void ShowActiveSpells()
    {
        // Get active spells
        activeSpellList = GameManager.Instance.activeSpells;
        int numOfSpells = activeSpellList.Count;

        for (int i = 0; i < numOfSpells; ++i)
        {
            spellUIs[i].SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
