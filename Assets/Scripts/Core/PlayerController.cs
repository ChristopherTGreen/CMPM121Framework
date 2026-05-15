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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        unit = GetComponent<Unit>();
        GameManager.Instance.player = gameObject;
        activeSpellIndex = 4;

        PlayerScaledFlag = false;

        UnityEngine.Debug.Log("Initial player health: " + GameManager.Instance.classTypes["player"].health);
        UnityEngine.Debug.Log("Initial player mana: " + GameManager.Instance.classTypes["player"].mana);
        UnityEngine.Debug.Log("Initial player mana regen: " + GameManager.Instance.classTypes["player"].mana_regeneration);
    }

    public void StartLevel()
    {
        // Parameters: 125 is the player's mana, 8 is the player's mana regen
        // These are hardcoded, later have the scaling for mana be here
        spellcaster = new SpellCaster(125, 8, Hittable.Team.PLAYER);
        StartCoroutine(spellcaster.ManaRegeneration());

        int start_health = RPNEvaluator.RPNEvaluator.Evaluate(GameManager.Instance.classTypes["player"].health, GameManager.Instance.variables);
        hp = new Hittable(start_health, Hittable.Team.PLAYER, gameObject);
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

        if (GameManager.Instance.state == GameManager.GameState.INWAVE)
        {
            if (!PlayerScaledFlag)
            {
                PlayerScaledFlag = true;
      
                PlayerClassScaling newData = new PlayerClassScaling(GameManager.Instance.classTypes["player"]);

                UnityEngine.Debug.Log("Player Scaled");

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

                UnityEngine.Debug.Log("Health Count: " + RPNEvaluator.RPNEvaluator.Evaluate(GameManager.Instance.classTypes["player"].health, new Dictionary<string, int> { { "wave", GameManager.Instance.wave_count} }));
                UnityEngine.Debug.Log("health Scaling: " + hp);
                UnityEngine.Debug.Log("mana Scaling: " + spellcaster.max_mana);

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
