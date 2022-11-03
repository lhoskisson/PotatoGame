using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointBehavior : MonoBehaviour
{
    public GameObject carrotPrefab;
    public GameObject broccoliPrefab;
    public GameObject EnemyMan;

    public int spHealth = 1000;
    private float time = 0.0f;
    public float timeDelay = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        EnemyMan = GameObject.Find("EnemyManager");
        carrotPrefab = Resources.Load<GameObject>("Prefabs/Carrot");
        broccoliPrefab = Resources.Load<GameObject>("Prefabs/Broccoli");
    }

    // Update is called once per frame
    void Update()
    {
        checkEnemyCount();

        if(spHealth <= 0)
        {
            Debug.Log("spawning disabled?");
            spawnDelay();
        }
    }

    public void checkEnemyCount()
    {
        if(EnemyManager.carrotCount < 20)
        {
            EnemyMan.GetComponent<EnemyManager>().spawnCarrot(gameObject.transform.position);
        }
        if(EnemyManager.broccoliCount < 10)
        {
            EnemyMan.GetComponent<EnemyManager>().spawnBroccoli(gameObject.transform.position);
        }
    }

    private void spawnDelay()
    {
        // IMPORTANT!!
        // this is cool and all, but when the spawn point is set to inactive it's update is no longer called.
        // this means it needs to be reactivated somewhere else which also means that somewhere else will need to 
        // be counting the timer for this to work. SpawnPointManager?

        // gameObject.SetActive(false);
        time += Time.deltaTime;
        if (time >= timeDelay)
        {
            time = 0f;
            Debug.Log("setting active");
            // gameObject.SetActive(true);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            Destroy(collision.gameObject);
            // make this less, this high for testing purposes
            spHealth -= 100;
        }
    }
}
