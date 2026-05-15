using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class SpellUIContainer : MonoBehaviour
{
    public GameObject[] spellUIs;
    public PlayerController player;
    private Spell[] activeSpellList = new Spell[4];

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
        int numOfSpells = activeSpellList.Length;

        for (int i = 0; i < numOfSpells; ++i)
        {
            if (GameManager.Instance.activeSpells[i] == null) continue;
            // If a spell Ui is inactive, set it active and set the spell icon, manacost and damage and image
            if (!spellUIs[i].activeSelf)
            {
                spellUIs[i].SetActive(true); //unhide the active spell UI component
                spellUIs[i].GetComponent<SpellUI>().SetSpell(activeSpellList[i]); //Set the stats and icon of the new spell in the UI
            }
    
        }

        // also a way for the player to use that spell.
    }



    public void ShowDropButton()
    {
        activeSpellList = GameManager.Instance.activeSpells;
        int numOfSpells = activeSpellList.Length; 
        GameObject dropbuttonObject;
        Button dropbuttonButton;

        for (int i = 0; i < numOfSpells; ++i)
        {
            if (GameManager.Instance.activeSpells[i] == null) continue;
            //if (spellUIs[i].activeSelf && spellUIs[i].GetComponent<SpellUI>().spell != null)
            dropbuttonObject = spellUIs[i].GetComponent<SpellUI>().dropbutton; //Getting the drop button game object from SpellUI.cs
            dropbuttonObject.SetActive(true); //unhiding all of them

            int index = i;

            //Debug.Log("ShowDropButton() Iteration: " + i);
            //Debug.Log(">> " + numOfSpells);

            dropbuttonButton = spellUIs[i].GetComponentInChildren<Button>();
            dropbuttonButton.onClick.RemoveAllListeners();
        
            dropbuttonButton.onClick.AddListener(() => GameManager.Instance.sessionStats.ClearSpell(index));
    
        }
    }



    public void HideDropButtons()
    {
        int numOfSpells = GameManager.Instance.activeSpells.Length; 
        GameObject dropbuttonObject;

        for (int i = 0; i < numOfSpells; ++i)
        {
            if (GameManager.Instance.activeSpells[i] == null) continue;
            dropbuttonObject = spellUIs[i].GetComponent<SpellUI>().dropbutton; //Getting the drop button game object from SpellUI.cs
            dropbuttonObject.SetActive(false); //unhiding all of them
    
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
