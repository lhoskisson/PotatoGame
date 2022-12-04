using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class SpawnPointBehavior : MonoBehaviour
{
    // health bar variables
    public HealthBarBehavior healthBar;
    public float minHealth = 0f;
    public float maxHealth = 1000f;

    private GameObject carrotPrefab;
    private GameObject broccoliPrefab;
    private GameObject tomatoPrefab;
    private GameObject enemyMan;
    public GameObject spawnPointManager;

    // array size should be at least the size of its budget, since carrots only cost
    // 1 to the budget it's possible to have a wave of only carrots (though extreamely unlikely)
    public GameObject[] waveOne = new GameObject[100];
    public GameObject[] waveTwo = new GameObject[200];

    public int waveOneBudget = 100;
    public int waveTwoBudget = 200;
    public bool waveOneIsSpawned;
    public bool waveTwoIsSpawned;
    public float spawnDelay = 1f; // time between each enemy spawn
    public float spawnTimer = 0f; // keeps track of spawnDelay
    public int wavei; // counter for the index of waves

    // varibles dealing with disabling the spawner
    public bool isDisabled;
    private float time = 0.0f;
    public float timeDelay = 10.0f;
    public bool isFirstRun = true;

    // Start is called before the first frame update
    void Start()
    {
        enemyMan = GameObject.Find("EnemyManager");
        spawnPointManager = GameObject.Find("SpawnPointManager");
        carrotPrefab = Resources.Load<GameObject>("Prefabs/Carrot");
        broccoliPrefab = Resources.Load<GameObject>("Prefabs/Broccoli");
        tomatoPrefab = Resources.Load<GameObject>("Prefabs/Tomato");

        // initializing healthBar on enemy
        minHealth = maxHealth;
        healthBar.SetHealth(minHealth, maxHealth);
        healthBar.offset = new Vector3(0f, 1.5f, 0f);

        createWave(waveOneBudget, waveOne);
        createWave(waveTwoBudget, waveTwo);
    }

    // Update is called once per frame
    void Update()
    {
        // checkEnemyCount();
        if (!waveOneIsSpawned && !isDisabled)
        {
            spawnWave(waveOne);
        }
        else if(!waveTwoIsSpawned && !isDisabled)
        {
            spawnWave(waveTwo);
        }
            
        if(minHealth <= 0)
        {
            Disable();
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
        }
    }

    public void spawnWave(GameObject[] wave)
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnDelay)
        {
            if (wave[wavei].name == "Carrot")
                enemyMan.GetComponent<EnemyManager>().spawnCarrot(gameObject.transform.position);
            else if (wave[wavei].name == "Broccoli")
                enemyMan.GetComponent<EnemyManager>().spawnBroccoli(gameObject.transform.position);
            else if (wave[wavei].name == "Tomato")
                enemyMan.GetComponent<EnemyManager>().spawnTomato(gameObject.transform.position);
            spawnTimer = 0f;
            wavei++;
        }
        if (wave[wavei] == null && !waveOneIsSpawned)
        {
            waveOneIsSpawned = true;
            wavei = 0;
        }
        else if (wave[wavei] == null && !waveTwoIsSpawned)
        {
            waveTwoIsSpawned = true;
            wavei = 0;
        }
        /* This loop will dump a wave all at once, might be useful for a boss wave. 
         * some variables will need to be updated to work properly.
        for (int i = 0; i < wave.Length; i++)
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
        }*/
    }

    // pickRandomEnemy returns a random enemy type
    public GameObject pickRandomEnemy()
    {
        GameObject randomEnemy;
        int enemySelector = Random.Range(1, 11); // Random.Range has an EXCLUSIVE max when using the int overload.
        if (enemySelector >= 1 && enemySelector <= 5) // 50% chance of a carrot
            randomEnemy = carrotPrefab;
        else if (enemySelector >= 6 && enemySelector <= 8) // 30% chance of a broccoli
            randomEnemy = broccoliPrefab;
        else // 20% chance of a tomato
            randomEnemy = tomatoPrefab;

        return randomEnemy;
    }

    private void Disable()
    {
        
        if (isFirstRun) // trying to avoid running unnecessary code each frame with this if
        {
            isDisabled = true;
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,0f);
            gameObject.GetComponent<Collider2D>().enabled = false;
            gameObject.GetComponentInChildren<Canvas>().enabled = false;
            isFirstRun = false;
        }
        time += Time.deltaTime;
        if (time >= timeDelay)
        {
            isDisabled = false;
            time = 0f;
            refillHealth();
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            gameObject.GetComponent<Collider2D>().enabled = true;
            gameObject.GetComponentInChildren<Canvas>().enabled = true;
            isFirstRun = true;
        }
        
    }
    
    // method that will be used to refill the spawners health in the SpawnPointManager
    public void refillHealth()
    {
        minHealth = maxHealth;
        healthBar.SetHealth(minHealth, maxHealth);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            minHealth -= collision.gameObject.GetComponent<ProjectileScript>().damage;
            healthBar.SetHealth(minHealth, maxHealth);
            Destroy(collision.gameObject);
        }
    }
}
