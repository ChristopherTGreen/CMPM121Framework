using UnityEngine;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;
using System.Linq;
using RPNEvaluator;

public class EnemySpawner : MonoBehaviour
{
    public Image level_selector;
    public GameObject button;
    public GameObject enemy;
    public SpawnPoint[] SpawnPoints;
    private Dictionary<string, int> variables = new Dictionary<string, int>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        variables["wave"] = 1; // wave tracker
        // variables for base (maybe move to for loop)
        variables["base_hp"] = 0;
        variables["base_speed"] = 0;
        variables["base_damage"] = 0;



        GameObject selector = Instantiate(button, level_selector.transform);
        selector.transform.localPosition = new Vector3(0, 130);
        selector.GetComponent<MenuSelectorController>().spawner = this;
        selector.GetComponent<MenuSelectorController>().SetLevel("Start");
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
        LevelData levelReference = level_selector.GetComponent<LevelData>();
        StartCoroutine(SpawnWave(levelReference));
    }

    public void NextWave(LevelData levelReference)
    {
        variables["wave"]++;
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
        for (int i = 0; i < enemyTypeCount; ++i)
        {
            SpawnEnemies();
        }
        yield return new WaitWhile(() => GameManager.Instance.enemy_count > 0);
        GameManager.Instance.state = GameManager.GameState.WAVEEND;
        NextWave(levelReference);
    }

    // saving SpawnZombie for reference
    IEnumerator SpawnZombie()
    {
        SpawnPoint spawn_point = SpawnPoints[Random.Range(0, SpawnPoints.Length)];
        Vector2 offset = Random.insideUnitCircle * 1.8f;
                
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
        EnemyData tempRef = enemyTypes[spawnReference.enemy];
        for (int i = 0; i < tempCount; i++)
        {
            SpawnEnemy(tempRef, spawnReference.location);
        }
        yield return new WaitForSeconds(float.Parse(spawnReference.delay));
    }
    // SpawnEnemy
    // Spawns the actual enemy with the process of locations - (I have no idea if its one specific location, random locations, etc) - chris
    private EnemyController SpawnEnemy(EnemyData enemyReference, int location)
    {
        SpawnPoint spawn_point = SpawnPoints[location];
        Vector2 offset = Random.insideUnitCircle * 1.8f; // constant here - chris

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
    // Sets the spawn values initially

    // SpawnModifyFlat
    // Called upon spawn, will change the given objects values by parameter specifications
    private void SpawnModifyFlat(SpawnData spawnReference, EnemyController en)
    {
        // might be better not to do this?
        variables["base_hp"] = 
        en.hp.SetMaxHP(RPNEvaluator.RPNEvaluator.Evaluate(, variables));

        en.speed = 
    }
}
