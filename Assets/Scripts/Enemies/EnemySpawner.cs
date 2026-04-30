using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RPNEvaluator;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    public Image level_selector;
    public GameObject button;
    public GameObject enemy;
    public SpawnPoint[] SpawnPoints;
    public Dictionary<string, int> variables { get; private set; } = new Dictionary<string, int> { {"Wave", 1} };
    public static LevelData levelReference;
    RewardScreenManager RewardScreenManagerClass;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameManager.Instance.wave_count = 1;
        variables["wave"] = GameManager.Instance.wave_count; // there might be a better way to improve this - chris

        MenuSelectorController.DynamicMenuButtonSpawner(this);
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartLevel(string levelname)
    {
        level_selector.gameObject.SetActive(false);
        // this is not nice: we should not have to be required to tell the player directly that the level is starting
        GameManager.Instance.player.GetComponent<PlayerController>().StartLevel();

        // find the level name (I think that is what this does) - chris
        //LevelData levelReference = LevelSelector(levelname);
        levelReference = GameManager.Instance.levels[levelname];
        StartCoroutine(SpawnWave(levelReference));
    }
    // NextWave
    // Calls for the next wave, with relevant level
    public void NextWave(LevelData levelReference)
    {
        GameManager.Instance.wave_count++;
        variables["wave"] = GameManager.Instance.wave_count;
        StartCoroutine(SpawnWave(levelReference));
    }


    IEnumerator SpawnWave(LevelData levelReference)
    {
        GameManager.Instance.state = GameManager.GameState.COUNTDOWN;
        GameManager.Instance.countdown = 3;
        for (int i = 3; i > 0; i--)
        {
            yield return new WaitForSeconds(1);
            GameManager.Instance.countdown--;
        }
        GameManager.Instance.state = GameManager.GameState.INWAVE;
        int enemyTypeCount = levelReference.spawns.Count;
        // This for loop should create every enemy type with enemy type number amount of programs running, but all halting individually depending on delays
        for (int i = 0; i < enemyTypeCount; ++i)
        {
            StartCoroutine(SpawnEnemies(levelReference.spawns[i]));
        }
        yield return new WaitWhile(() => GameManager.Instance.enemy_count > 0);
        GameManager.Instance.state = GameManager.GameState.WAVEEND;
        // Should end the round when waves are finished?
        if (GameManager.Instance.wave_count < levelReference.waves)
        {
            // theres a temporary delay?
            yield return new WaitForSeconds(1);
            RewardScreenManagerClass.NextWaveButtonHandler(this);
            yield return new WaitWhile(() => RewardScreenManager.RewardScreenActive() == true);
            NextWave(levelReference);
        }
        else GameManager.Instance.state = GameManager.GameState.GAMEOVER; // there might be a better state for this, pregame?
    }

    // saving SpawnZombie for reference
    IEnumerator SpawnZombie()
    {
        SpawnPoint spawn_point = SpawnPoints[UnityEngine.Random.Range(0, SpawnPoints.Length)]; // UnityEngine.Random is an issue, when UnityEngine and System are being used, doesn't know which to reference
        Vector2 offset = UnityEngine.Random.insideUnitCircle * 1.8f;
                
        Vector3 initial_position = spawn_point.transform.position + new Vector3(offset.x, offset.y, 0);
        GameObject new_enemy = Instantiate(enemy, initial_position, Quaternion.identity);

        new_enemy.GetComponent<SpriteRenderer>().sprite = GameManager.Instance.enemySpriteManager.Get(0);
        EnemyController en = new_enemy.GetComponent<EnemyController>();
        en.hp = new Hittable(50, Hittable.Team.MONSTERS, new_enemy);
        en.speed = 10;
        GameManager.Instance.AddEnemy(new_enemy);
        yield return new WaitForSeconds(0.5f);
    }
    // SpawnEnemies
    // The process of spawning multiple given enemies and applying SpawnData
    IEnumerator SpawnEnemies(SpawnData spawnReference)
    {
        int tempCount = RPNEvaluator.RPNEvaluator.Evaluate(spawnReference.count, variables);
        int tempSequenceIndex = 0; // will iterate through sequence 
        int sequenceCount = spawnReference.sequence.Count; // 1 is default, since we check with mod or %
        int currBuildCount = spawnReference.sequence[tempSequenceIndex]; // stores the current build count from sequence
        SpawnPoint spawnLocation = SpawnSelector(spawnReference.location);
        EnemyData tempEnemyReference = GameManager.Instance.enemyTypes[spawnReference.enemy];

        // separate this into its own function?
        while (tempCount >= 0)
        {
            for (int i = 0; i < currBuildCount; i++)
            {
                SpawnModifyFlat(spawnReference, SpawnEnemy(tempEnemyReference, spawnLocation)); // location will need to be handled separately with a locator?
            }
            tempSequenceIndex = (tempSequenceIndex + 1) % sequenceCount;
            tempCount -= currBuildCount;
            currBuildCount = spawnReference.sequence[tempSequenceIndex];
            if (spawnReference.delay != null) yield return new WaitForSeconds(RPNEvaluator.RPNEvaluator.Evaluatef(spawnReference.delay, variables));
        }
        
    }
    // SpawnEnemy
    // Spawns the actual enemy with the process of locations - (I have no idea if its one specific location, random locations, etc) - chris
    private EnemyController SpawnEnemy(EnemyData enemyReference, SpawnPoint spawnLocation)
    {
        SpawnPoint spawn_point = spawnLocation;
        Vector2 offset = UnityEngine.Random.insideUnitCircle * 1.8f; // constant here - chris

        Vector3 initial_position = spawn_point.transform.position + new Vector3(offset.x, offset.y, 0);
        GameObject new_enemy = Instantiate(enemy, initial_position, Quaternion.identity);

        new_enemy.GetComponent<SpriteRenderer>().sprite = GameManager.Instance.enemySpriteManager.Get(enemyReference.sprite);
        EnemyController en = new_enemy.GetComponent<EnemyController>();
        // builder (lets try to have default values somehow?, and quicker way to initialize)
        // we might need to make a function similar to that of hittable
        en.hp = new Hittable(enemyReference.hp, Hittable.Team.MONSTERS, new_enemy);
        en.speed = enemyReference.speed;
        en.damage = enemyReference.damage;

        GameManager.Instance.AddEnemy(new_enemy);
        return en;
    }

    // SpawnValues
    // Sets the spawn values initially?

    // SpawnModifyFlat
    // Called upon spawn, will change the given objects values by parameter specifications
    private void SpawnModifyFlat(SpawnData spawnReference, EnemyController en)
    {
        // might be better not to do this?
        en.hp.SetMaxHP(RPNEvaluator.RPNEvaluator.Evaluate(spawnReference.hp, new Dictionary<string, int>(variables) { ["base"] = en.hp.max_hp}));

        //en.speed = spawnReference.speed;
        //en.damage = spawnReference.damage;
    }


    // Possibly move to a separate file or dedicated .cs - Chris
    // SpawnSelector
    // Checks the given name and identifies index for the spawn
    private SpawnPoint SpawnSelector(string spawnLocation)
    {
        if (spawnLocation == "random")
        {
            return SpawnPoints[UnityEngine.Random.Range(0, SpawnPoints.Length)];
        }
        string[] spawnToken = RPNEvaluator.RPNEvaluator.TokenizeString(spawnLocation);
        string spawnName = spawnToken[1];
        
        for (int i = 0; i < SpawnPoints.Length; i++)
        {
            SpawnPoint.SpawnName spawn = SpawnPoints[i].kind;
            if (spawn.ToString().ToLower() == spawnName) return SpawnPoints[i];
        }



        throw new Exception("Spawn point name invalid, the JSON file name does not exist in the SpawnPoint.cs");
    }

    // LevelSelector
    // Finds the given level and returns levelData to it (move this to an outside function)  - chris
    // THIS IS OLD, but keep it just in case we move to lists - chris
    /*private LevelData LevelSelector(string levelName)
    {
        Dictionary<string, LevelData> levels = GameManager.Instance.levels;
        int levelSize = levels.Count;
        for (int i = 0; i < levelSize; i++)
        {
            LevelData currentLevel = levels[i];
            if (currentLevel.name == levelName) return currentLevel;
        }

        throw new Exception("Level name invalid, the level name in the JSON file does not exist as a class LevelData");
    }*/
    
}
