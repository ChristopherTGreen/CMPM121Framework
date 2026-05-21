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

    private bool PlayerScaledFlag;

    public ClassData chosenClass;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        unit = GetComponent<Unit>();
        GameManager.Instance.player = gameObject;
        activeSpellIndex = 4;

        PlayerScaledFlag = false;

    }

    public void StartLevel()
    {

        // need to get the class from the PlayerClassSelector.cs
        chosenClass = GameManager.Instance.chosenClass;
        UnityEngine.Debug.Log("PlayerControler.cs_StartLevel() >> choseClass: " + chosenClass.name);

        // Parameters: 125 is the player's mana, 8 is the player's mana regen
        // These are hardcoded, later have the scaling for mana be here
        spellcaster = new SpellCaster(
            RPNEvaluator.RPNEvaluator.Evaluate(GameManager.Instance.classTypes[chosenClass.name].mana, GameManager.Instance.variables), 
            RPNEvaluator.RPNEvaluator.Evaluate(GameManager.Instance.classTypes[chosenClass.name].mana_regeneration, GameManager.Instance.variables), 
            Hittable.Team.PLAYER
        );
        
        StartCoroutine(spellcaster.ManaRegeneration());

        int start_health = RPNEvaluator.RPNEvaluator.Evaluate(GameManager.Instance.classTypes[chosenClass.name].health, GameManager.Instance.variables);

        hp = new Hittable(start_health, Hittable.Team.PLAYER, gameObject);
        hp.OnDeath += Die;
        hp.team = Hittable.Team.PLAYER;

        // tell UI elements what to show
        healthui.SetHealth(hp);
        manaui.SetSpellCaster(spellcaster);
        spellui.SetSpell(spellcaster.spell);

        UnityEngine.Debug.Log("Initial player health: " + hp.max_hp);
        UnityEngine.Debug.Log("Initial player max mana: " + spellcaster.max_mana);
        UnityEngine.Debug.Log("Initial player mana regen: " + spellcaster.mana_reg);
    }

    // Update is called once per frame
    void Update()
    {
        //When player hit's tab, toggle the spell used
        if (Keyboard.current[Key.Tab].wasPressedThisFrame)
        {
            SwitchSpell();
        }

        if (GameManager.Instance.state == GameManager.GameState.INWAVE)
        {
            if (!PlayerScaledFlag)
            {
                PlayerScaledFlag = true;
      
                PlayerClassScaling newData = new PlayerClassScaling(GameManager.Instance.classTypes[chosenClass.name]);

                UnityEngine.Debug.Log(">> Player Scaled");

                //updating player with new scaling
                spellcaster.SetMaxMana(newData.mana);
                spellcaster.mana_reg = newData.mana_regeneration;
                power = newData.spellpower;
                speed = newData.speed;
                
                hp.SetMaxHP(newData.health);
                hp.team = Hittable.Team.PLAYER;

                healthui.SetHealth(hp);
                manaui.SetSpellCaster(spellcaster);
                spellui.SetSpell(spellcaster.spell);

                UnityEngine.Debug.Log("New Health Count: " + RPNEvaluator.RPNEvaluator.Evaluate(GameManager.Instance.classTypes["player"].health, new Dictionary<string, int> { { "wave", GameManager.Instance.wave_count} }));
                UnityEngine.Debug.Log("New max health Scaling: " + hp.max_hp);
                UnityEngine.Debug.Log("New max mana Scaling: " + spellcaster.max_mana);
                UnityEngine.Debug.Log("New mana regen Scaling: " + spellcaster.mana_reg);

            }
        }
        else 
        {
            PlayerScaledFlag = false;
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
        System.Array.Clear(GameManager.Instance.activeSpells, 0, GameManager.Instance.activeSpells.Length);

        

        //Debug.Log("You Lost");
    }

    void SwitchSpell()
    {

        if (GameManager.Instance.activeSpells.Length > 0)
        {
            
            // Should cycle, 1 2 3 0
            activeSpellIndex = (activeSpellIndex + 1) % GameManager.Instance.activeSpells.Length;
            while (GameManager.Instance.activeSpells[activeSpellIndex] == null)
            {
                activeSpellIndex = (activeSpellIndex + 1) % GameManager.Instance.activeSpells.Length;
            }
            

            UnityEngine.Debug.Log("Active Spell index: " + activeSpellIndex);

            Spell activespell = GameManager.Instance.activeSpells[activeSpellIndex];
            spellcaster.CurrentActiveSpell(activespell);

            UnityEngine.Debug.Log("Equipped Spell: " + activespell.name + System.Array.IndexOf(GameManager.Instance.activeSpells, activespell));

        } 
        else
        {
            throw new System.Exception("PlayerController.cs_SwitchSpell() >> MASSIVE ERROR! YOU SHOULD NOT HAVE LESS THAN 1 SPELL! CHECK SpellCaster.cs");
        } 

    }

}
