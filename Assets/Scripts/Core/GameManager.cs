using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

public class GameManager 
{
    public enum GameState
    {
        PREGAME,
        INWAVE,
        WAVEEND,
        COUNTDOWN,
        GAMEOVER
    }
    public GameState state;

    public int countdown;
    private static GameManager theInstance;
    public static GameManager Instance {  get
        {
            if (theInstance == null)
                theInstance = new GameManager();
            return theInstance;
        }
    }

    public GameObject player;
    
    public ProjectileManager projectileManager;
    public SpellIconManager spellIconManager;
    public EnemySpriteManager enemySpriteManager;
    public PlayerSpriteManager playerSpriteManager;
    public RelicIconManager relicIconManager;

    public Stats sessionStats = new Stats();

    private List<GameObject> enemies;
    // Private dictionary for enemy types for storage in a singleton 
    public Dictionary<string, EnemyData> enemyTypes; // (possibly convert to lists based on what prof said) - chris
    public Dictionary<string, LevelData> levels;
    public Dictionary<string, SpellData> spells;

    public List<Spell> activeSpells = new List<Spell>(); //stores the player's current active spells
    public Spell currentRewardSpell;
    public SpellUIContainer spellUIcontainer;

    public Dictionary<string, int> variables => new Dictionary<string, int>
    {
        { "power", GameManager.Instance.player.GetComponent<PlayerController>().power},
        { "wave", GameManager.Instance.wave_count }, 
    };
    public int enemy_count { get { return enemies.Count; } }
    public int enemy_spawns_left = 0;
    public int wave_count = 0;

    public void AddEnemy(GameObject enemy)
    {
        enemies.Add(enemy);
    }
    public void RemoveEnemy(GameObject enemy)
    {
        enemies.Remove(enemy);
    }
    public void ClearEnemy()
    {
        enemies.Clear();
    }

    public GameObject GetClosestEnemy(Vector3 point)
    {
        if (enemies == null || enemies.Count == 0) return null;
        if (enemies.Count == 1) return enemies[0];
        return enemies.Aggregate((a,b) => (a.transform.position - point).sqrMagnitude < (b.transform.position - point).sqrMagnitude ? a : b);
    }

    // Stores the active spell that the spellcaster makes.
    public void StoreActiveSpell(Spell spell)
    {
        activeSpells.Add(spell);
        Debug.Log("GameManager.cs_StoreActiveSpell(Spell) >> sucessfully stored a spell " + spell.name);
    }

    private GameManager()
    {
        enemies = new List<GameObject>();
        levels = RetrieveLevelData.LevelDictionary();
        enemyTypes = RetrieveEnemyData.EnemyDictionary();
        spells = RetrieveSpellData.SpellDictionary();
        Debug.Log(string.Join(", ", spells));
    }
}
