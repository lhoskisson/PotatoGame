using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointBehavior : MonoBehaviour
{
    private GameObject carrotPrefab;
    private GameObject broccoliPrefab;
    private GameObject tomatoPrefab;
    private GameObject enemyMan;
    public GameObject spawnPointManager;

    // array size should be at least the size of its budget, since carrots only cost
    // 1 to the budget it's possible to have a wave of only carrots (though extreamely unlikely)
    public GameObject[] waveOne = new GameObject[100];

    public int spHealth = 500;
    public int waveOneBudget = 100;
    public bool waveOneIsSpawned;

    // Start is called before the first frame update
    void Start()
    {
        enemyMan = GameObject.Find("EnemyManager");
        spawnPointManager = GameObject.Find("SpawnPointManager");
        carrotPrefab = Resources.Load<GameObject>("Prefabs/Carrot");
        broccoliPrefab = Resources.Load<GameObject>("Prefabs/Broccoli");
        tomatoPrefab = Resources.Load<GameObject>("Prefabs/Tomato");
        createWave(waveOneBudget, waveOne);
    }

    // Update is called once per frame
    void Update()
    {
        // checkEnemyCount();
        if (!waveOneIsSpawned)
        {
            spawnWave(waveOne);
            waveOneIsSpawned = true;
        }
            
        if(spHealth <= 0)
        {
            initiateDisable();
        }
    }

    public void checkEnemyCount()
    {
        if(EnemyManager.carrotCount < 10)
        {
            enemyMan.GetComponent<EnemyManager>().spawnCarrot(gameObject.transform.position);
        }
        if(EnemyManager.broccoliCount < 5)
        {
            enemyMan.GetComponent<EnemyManager>().spawnBroccoli(gameObject.transform.position);
        }
        if(EnemyManager.tomatoCount < 3)
        {
            enemyMan.GetComponent<EnemyManager>().spawnTomato(gameObject.transform.position);
        }
    }

    public void createWave(int waveBudget, GameObject[] wave)
    {
        // forloop will loop until wave budget is less than 0 (the break will take it out).
        for(int i = 0; i < 1000; i++)
        {
            if (waveBudget < 0)
                break;
            wave[i] = pickRandomEnemy();
            if (wave[i].name == "Carrot")
                waveBudget -= CarrotBehavior.carrotCost;
            else if (wave[i].name == "Broccoli")
                waveBudget -= BroccoliBehavior.broccoliCost;
            else if (wave[i].name == "Tomato")
                waveBudget -= TomatoBehavior.tomatoCost;
            Debug.Log("WaveBudget = " + waveBudget);
            Debug.Log("Wave["+i+"] name = " + wave[i].name);
        }
    }

    public void spawnWave(GameObject[] wave)
    {
        for( int i = 0; i < wave.Length; i++)
        {
            if (wave[i] == null)
            {
                break;
            }
            if (wave[i].name == "Carrot")
                enemyMan.GetComponent<EnemyManager>().spawnCarrot(gameObject.transform.position);
            else if (wave[i].name == "Broccoli")
                enemyMan.GetComponent<EnemyManager>().spawnBroccoli(gameObject.transform.position);
            else if (wave[i].name == "Tomato")
                enemyMan.GetComponent<EnemyManager>().spawnTomato(gameObject.transform.position);
        }
    }

    // pickRandomEnemy returns a random enemy type
    public GameObject pickRandomEnemy()
    {
        GameObject randomEnemy;
        int enemySelector = Random.Range(1, 4); // Random.Range has an EXCLUSIVE max when using the int overload.
        Debug.Log("enemySelector = " + enemySelector);
        switch (enemySelector) 
        {
            case 1:
                randomEnemy = carrotPrefab;
                break;
            case 2:
                randomEnemy = broccoliPrefab;
                break;
            case 3:
                randomEnemy = tomatoPrefab;
                break;
            default: // required for the compiler to recognize randomEnemy as assigned.
                randomEnemy = carrotPrefab;
                break;
        }
        Debug.Log("random enemy = " + randomEnemy.name);
        return randomEnemy;
    }

    private void initiateDisable()
    {
        // setting loadedSpawnPoint in spawnPointManager to this spawn point
        spawnPointManager.GetComponent<SpawnPointManager>().setSpawnPoint(gameObject);
        // starting the disableSpawner method in the SpawnPointManager
        spawnPointManager.GetComponent<SpawnPointManager>().disableSpawner(gameObject);
    }
    
    // method that will be used to refill the spawners health in the SpawnPointManager
    public void refillHealth()
    {
        spHealth = 500;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            Destroy(collision.gameObject);
            spHealth -= 25;
        }
    }
}
