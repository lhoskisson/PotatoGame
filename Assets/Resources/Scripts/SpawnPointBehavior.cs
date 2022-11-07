using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointBehavior : MonoBehaviour
{
    public GameObject carrotPrefab;
    public GameObject broccoliPrefab;
    public GameObject enemyMan;
    public GameObject spawnPointManager;

    public int spHealth = 500;

    // Start is called before the first frame update
    void Start()
    {
        enemyMan = GameObject.Find("EnemyManager");
        spawnPointManager = GameObject.Find("SpawnPointManager");
        carrotPrefab = Resources.Load<GameObject>("Prefabs/Carrot");
        broccoliPrefab = Resources.Load<GameObject>("Prefabs/Broccoli");
    }

    // Update is called once per frame
    void Update()
    {
        checkEnemyCount();

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
        spHealth = 1000;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            Destroy(collision.gameObject);
            // make this less, this high for testing purposes, should be 25 to keep projectile damage consistent
            spHealth -= 25;
        }
    }
}
