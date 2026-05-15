using UnityEngine;
using UnityEngine.InputSystem;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;

public class PlayerController : MonoBehaviour
{
    public Hittable hp;
    public HealthBar healthui;
    public ManaBar manaui;

    public SpellCaster spellcaster;
    public SpellUI spellui;

    public int speed;
    public int power;

    public Unit unit;

    public int activeSpellIndex;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        unit = GetComponent<Unit>();
        GameManager.Instance.player = gameObject;
        activeSpellIndex = 4;
    }

    public void StartLevel()
    {
        // Parameters: 125 is the player's mana, 8 is the player's mana regen
        // These are hardcoded, later have the scaling for mana be here
        spellcaster = new SpellCaster(125, 8, Hittable.Team.PLAYER);
        StartCoroutine(spellcaster.ManaRegeneration());

        hp = new Hittable(100, Hittable.Team.PLAYER, gameObject);
        hp.OnDeath += Die;
        hp.team = Hittable.Team.PLAYER;

        // tell UI elements what to show
        healthui.SetHealth(hp);
        manaui.SetSpellCaster(spellcaster);
        spellui.SetSpell(spellcaster.spell);
    }

    // Update is called once per frame
    void Update()
    {
        //When player hit's tab, toggle the spell used
        if (Keyboard.current[Key.Tab].wasPressedThisFrame)
        {
            SwitchSpell();
        }
    }

    void OnAttack(InputValue value)
    {
        if (GameManager.Instance.state == GameManager.GameState.PREGAME || GameManager.Instance.state == GameManager.GameState.GAMEOVER) return;
        Vector2 mouseScreen = Mouse.current.position.value;
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(mouseScreen);
        mouseWorld.z = 0;
        StartCoroutine(spellcaster.Cast(transform.position, mouseWorld));
    }

    void OnMove(InputValue value)
    {
        if (GameManager.Instance.state == GameManager.GameState.PREGAME || GameManager.Instance.state == GameManager.GameState.GAMEOVER) return;
        unit.movement = value.Get<Vector2>()*speed;
    }

    void Die()
    {
        GameManager.Instance.state = GameManager.GameState.GAMEOVER;
        
        //Clearing active spells that the player has on death
        GameManager.Instance.activeSpells.Clear();
        GameManager.Instance.sessionStats.ClearModNamesList();

        //Debug.Log("You Lost");
    }

    void SwitchSpell()
    {

        if (GameManager.Instance.activeSpells.Count > 0)
        {
            
            // Should cycle, 1 2 3 0
            activeSpellIndex = (activeSpellIndex + 1) % GameManager.Instance.activeSpells.Count;

            UnityEngine.Debug.Log("Active Spell index: " + activeSpellIndex);

            Spell activespell = GameManager.Instance.activeSpells[activeSpellIndex];
            spellcaster.CurrentActiveSpell(activespell);

            UnityEngine.Debug.Log("Equipped Spell: " + activespell.name + GameManager.Instance.activeSpells.IndexOf(activespell));

        } 
        else
        {
            throw new System.Exception("PlayerController.cs_SwitchSpell() >> MASSIVE ERROR! YOU SHOULD NOT HAVE LESS THAN 1 SPELL! CHECK SpellCaster.cs");
        } 

    }

}
